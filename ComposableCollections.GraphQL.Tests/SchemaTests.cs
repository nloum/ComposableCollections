using System.Threading.Tasks;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using FluentAssertions;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using LiveLinq.Dictionary;
using Microsoft.Extensions.DependencyInjection;
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
        public async Task ComposableDictionaryAtRootShouldWork()
        {
            var state = new ComposableDictionary<string, Person>();
            
            var schema = new SchemaBuilder()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddTopLevelComposableDictionary<ComposableDictionary<string, Person>>(new ComposableDictionaryObjectTypeParameters()
                {
                    CollectionName = "people",
                    ValueNameSingular = "one",
                    ValueNamePlural = "all",
                    KeyNameSingular = "name",
                    KeyNamePlural = "names",
                }, rc => state)
                .Create();
            
            await Verify(schema.ToString());

            var executor = schema.MakeExecutable();

            var mutationResult = (await executor.ExecuteAsync(QueryRequestBuilder.Create(@"mutation {
    people {
        tryAddOne(key: ""Joe"", value: { name: ""Joe"" })
    }
}
"))).ToJson();
            await Verify(mutationResult).UseFileName(nameof(ComposableDictionaryAtRootShouldWork) + "_Mutation_Result");

            var queryResult = (await executor.ExecuteAsync(QueryRequestBuilder.Create(@"query {
    people {
        all {
            name
        }
    }
}"))).ToJson();
            await Verify(queryResult).UseFileName(nameof(ComposableDictionaryAtRootShouldWork) + "_Query_Result");
        }
        
        [TestMethod]
        public Task QueryableComposableDictionaryAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddFiltering()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddTopLevelComposableDictionary<SimpleQueryableComposableDictionary<string, Person>>(new ComposableDictionaryObjectTypeParameters()
                {
                    CollectionName = "people",
                    ValueNameSingular = "person",
                    ValueNamePlural = "people",
                    KeyNameSingular = "name",
                    KeyNamePlural = "names",
                })
                .Create();

            return Verify(schema.ToString());
        }
        
        [TestMethod]
        public Task DictionaryWithBuiltInKeyAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddTopLevelComposableDictionary<IDictionaryWithBuiltInKey<string, Person>>(new ComposableDictionaryObjectTypeParameters()
                {
                    CollectionName = "people",
                    ValueNameSingular = "person",
                    ValueNamePlural = "people",
                    KeyNameSingular = "name",
                    KeyNamePlural = "names",
                })
                .Create();

            return Verify(schema.ToString());
        }
        
        [TestMethod]
        public Task QueryableDictionaryWithBuiltInKeyAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddFiltering()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddTopLevelComposableDictionary<IQueryableDictionaryWithBuiltInKey<string, Person>>(new ComposableDictionaryObjectTypeParameters()
                {
                    CollectionName = "people",
                    ValueNameSingular = "person",
                    ValueNamePlural = "people",
                    KeyNameSingular = "name",
                    KeyNamePlural = "names",
                })
                .Create();

            return Verify(schema.ToString());
        }

        [TestMethod]
        public Task ObservableComposableDictionaryAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Subscription")))
                .AddTopLevelComposableDictionary<ObservableDictionary<string, Person>>(new ComposableDictionaryObjectTypeParameters()
                {
                    CollectionName = "people",
                    ValueNameSingular = "person",
                    ValueNamePlural = "people",
                    KeyNameSingular = "name",
                    KeyNamePlural = "names",
                })
                .Create();

            return Verify(schema.ToString());
        }
        
        [TestMethod]
        public Task ObservableQueryableComposableDictionaryAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddFiltering()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
                .AddType(new ObjectType(descriptor => descriptor.Name("Subscription")))
                .AddTopLevelComposableDictionary<IObservableQueryableDictionary<string, Person>>(new ComposableDictionaryObjectTypeParameters()
                {
                    CollectionName = "people",
                    ValueNameSingular = "person",
                    ValueNamePlural = "people",
                    KeyNameSingular = "name",
                    KeyNamePlural = "names",
                })
                .Create();

            return Verify(schema.ToString());
        }
        
        [TestMethod]
        public Task ReadOnlyComposableDictionaryAtRootShouldWork()
        {
            var schema = new SchemaBuilder()
                .AddFiltering()
                .AddType<ObjectType<Person>>()
                .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
                .AddTopLevelComposableDictionary<IComposableReadOnlyDictionary<string, Person>>(new ComposableDictionaryObjectTypeParameters()
                {
                    CollectionName = "people",
                    ValueNameSingular = "person",
                    ValueNamePlural = "people",
                    KeyNameSingular = "name",
                    KeyNamePlural = "names",
                })
                .Create();

            return Verify(schema.ToString());
        }
        
        // [TestMethod]
        // public Task ObservableDictionaryWithBuiltInKeyAtRootShouldWork()
        // {
        //     var schema = new SchemaBuilder()
        //         .AddType<ObjectType<Person>>()
        //         .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
        //         .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
        //         .AddType(new ObjectType(descriptor => descriptor.Name("Subscription")))
        //         .AddTopLevelComposableDictionary<IObservableDictionaryWithBuiltInKey<string, Person>>("people")
        //         .Create();
        //
        //     return Verify(schema.ToString());
        // }
        
        // [TestMethod]
        // public Task ObservableQueryableDictionaryWithBuiltInKeyAtRootShouldWork()
        // {
        //     var schema = new SchemaBuilder()
        //         .AddFiltering()
        //         .AddType<ObjectType<Person>>()
        //         .AddType(new ObjectType(descriptor => descriptor.Name("Query")))
        //         .AddType(new ObjectType(descriptor => descriptor.Name("Mutation")))
        //         .AddType(new ObjectType(descriptor => descriptor.Name("Subscription")))
        //         .AddTopLevelComposableDictionary<IQueryableObservableDictionaryWithBuiltInKey<string, Person>>("people")
        //         .Create();
        //
        //     return Verify(schema.ToString());
        // }
    }
}