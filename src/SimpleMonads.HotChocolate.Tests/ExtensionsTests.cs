using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMonads.HotChocolate;
using VerifyMSTest;

namespace SimpleMonads.GraphQL.Tests
{
    [TestClass]
    public class ExtensionsTests : VerifyBase
    {
        public interface IAnimal
        {
            string Name { get; set; }
        }

        public class Cat : IAnimal
        {
            public string Name { get; set; }
            public bool IsBadCat { get; set; }
        }

        public class Dog : IAnimal
        {
            public string Name { get; set; }
            public bool IsGoodDog { get; set; }
        }

        public class AnyAnimal : SubTypesOf<IAnimal>.Either<Cat, Dog>
        {
            
        }

        public class DogType : ObjectType<Dog>
        {
            protected override void Configure(IObjectTypeDescriptor<Dog> descriptor)
            {
                descriptor.Name("Dog");
            }
        }

        public class AnimalRepository
        {
            public IEnumerable<Dog> GetDogs()
            {
                return new[]
                {
                    new Dog()
                    {
                        Name = "Oscar",
                        IsGoodDog = true
                    }
                };
            }
        }

        [TestMethod]
        public Task AddEitherShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddType<DogType>()
                .AddType<ObjectType<Cat>>()
                .AddEither<AnyAnimal>()
                // Add empty query
                .AddType(new ObjectType(descriptor => descriptor.Name("Query" )))
                .AddType(new ObjectTypeExtension<AnimalRepository>(x => x.Name("Query")))
                .Create();

            return Verify(schema.ToString());
        }
    }
}