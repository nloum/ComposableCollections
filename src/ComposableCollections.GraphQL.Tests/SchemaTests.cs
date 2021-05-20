using System.Threading.Tasks;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyMSTest;

namespace ComposableCollections.GraphQL.Tests
{
    [TestClass]
    public class SchemaTests : VerifyBase
    {
        public class Person
        {
            public string Name { get; set; }
        }
        
        [TestMethod]
        public Task ComposableDictionaryAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddTopLevelComposableDictionary<ComposableDictionary<string, Person>>("people")
                .Create();

            return Verify(schema.ToString());
        }
        
        [TestMethod]
        public Task QueryableComposableDictionaryAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddFiltering()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddTopLevelComposableDictionary<SimpleQueryableComposableDictionary<string, Person>>("people")
                .Create();

            return Verify(schema.ToString());
        }
    }
}