using System.Collections.Generic;
using System.Linq;
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

        public class EitherAnimalRepository
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

            public IEnumerable<AnyAnimal> GetAnimals()
            {
                return GetDogs().Select(x => (AnyAnimal)x);
            }
        }

        public class MaybeAnimalRepository
        {
            public IMaybe<Dog> GetDogs()
            {
                return new Dog()
                {
                    Name = "Oscar",
                    IsGoodDog = true
                }.ToMaybe();
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
                .AddType(new ObjectTypeExtension<EitherAnimalRepository>(x => x.Name("Query")))
                .Create();

            return Verify(schema.ToString());
        }
        
        [TestMethod]
        public Task AddMaybeObjectTypeShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddType<DogType>()
                .AddMaybe<DogType>()
                // Add empty query
                .AddType(new ObjectType(descriptor => descriptor.Name("Query" )))
                .AddType(new ObjectTypeExtension<MaybeAnimalRepository>(x => x.Name("Query")))
                .Create();

            return Verify(schema.ToString());
        }
        
        [TestMethod]
        public Task AddMaybeShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddType<DogType>()
                .AddMaybe<ObjectType<Dog>>()
                // Add empty query
                .AddType(new ObjectType(descriptor => descriptor.Name("Query" )))
                .AddType(new ObjectTypeExtension<MaybeAnimalRepository>(x => x.Name("Query")))
                .Create();

            return Verify(schema.ToString());
        }
    }
}