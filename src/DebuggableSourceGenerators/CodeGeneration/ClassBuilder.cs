namespace DebuggableSourceGenerators.CodeGeneration
{
    public class ClassBuilder
    {
        public Type? BaseClass { get; set; }
        public List<Type> Interfaces { get; } = new List<Type>();
        public string Name { get; set; }
        public List<Constructor> Constructors { get; } = new List<Constructor>();
        public List<Property> Properties { get; } = new List<Property>();
        public List<Method> Methods { get; } = new List<Method>();
        public List<Type> StaticMethodImplementationSources { get; } = new List<Type>();
        public bool IncludeConstructorForAllProperties { get; set; }

        public Type Build()
        {
            BaseClass ??= typeof(object);

            var assemblyName = new AssemblyName("DynamicAssemblyExample");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()),
                AssemblyBuilderAccess.Run);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            var typeBuilder = moduleBuilder.DefineType(Name, TypeAttributes.Public, BaseClass, Interfaces.ToArray());

            var baseTypes = Interfaces.ToList();
            if (BaseClass != null)
            {
                baseTypes.Insert(0, BaseClass);
            }

            var propertiesThatNeedToBeOverriden = new HashSet<PropertyInfo>();

            foreach (var baseType in baseTypes)
            {
                var propertiesThatNeedToBeOverridenForThisBaseType = baseType.GetProperties().Where(property =>
                    (property.GetMethod?.IsAbstract == true || property.SetMethod?.IsAbstract == true ||
                     baseType.IsInterface));
                foreach (var property in propertiesThatNeedToBeOverridenForThisBaseType)
                {
                    propertiesThatNeedToBeOverriden.Add(property);
                }
            }

            foreach (var property in Properties)
            {
                foreach (var baseType in baseTypes)
                {
                    property.PropertyToOverride = baseType.GetProperty(property.Name);

                    if (property.PropertyToOverride?.PropertyType != property.Type)
                    {
                        property.PropertyToOverride = null;
                    }

                    if (property.PropertyToOverride != null)
                    {
                        if (propertiesThatNeedToBeOverriden.Contains(property.PropertyToOverride))
                        {
                            propertiesThatNeedToBeOverriden.Remove(property.PropertyToOverride);
                        }

                        break;
                    }
                }
            }

            foreach (var propertyThatNeedsToBeOverriden in propertiesThatNeedToBeOverriden)
            {
                var property = new Property()
                {
                    Name = propertyThatNeedsToBeOverriden.Name,
                    Type = propertyThatNeedsToBeOverriden.PropertyType,
                    PropertyToOverride = propertyThatNeedsToBeOverriden,
                };

                Properties.Add(property);
            }

            foreach (var property in Properties)
            {
                property.Field = typeBuilder.DefineField("_" + property.Name.Camelize(), property.Type,
                    FieldAttributes.Private);
            }

            foreach (var x in Methods)
            {
                var methodInfo = x.MethodToOverride ?? x.StaticImplementation;
                if (x.ImplementationMode == MethodImplementationMode.UnchangingReturnValueFromConstructorParameter)
                {
                    x.Field = typeBuilder.DefineField("_" + x.Name.Camelize(), methodInfo.ReturnType,
                        FieldAttributes.Private);
                    continue;
                }

                if (x.ImplementationMode == MethodImplementationMode.DelegateFromConstructorParameter)
                {
                    x.Field = typeBuilder.DefineField("_" + x.Name.Camelize(), GetDelegateType(methodInfo),
                        FieldAttributes.Private);
                    continue;
                }

                throw new InvalidOperationException(
                    $"There is supposed to be a constructor to setup the {x.Name} method but the implementation mode {x.ImplementationMode} is invalid for this");
            }

            if (IncludeConstructorForAllProperties)
            {
                var constructor = new Constructor();
                constructor.PropertiesToInitialize.AddRange(Properties);
                Constructors.Add(constructor);
            }

            foreach (var constructor in Constructors)
            {
                var parameterTypes = new List<Type>();

                foreach (var property in constructor.PropertiesToInitialize)
                {
                    parameterTypes.Add(property.Type);
                }

                foreach (var method in constructor.MethodsToInitialize)
                {
                    parameterTypes.Add(method.Field.FieldType);
                }

                if (constructor.BaseConstructor != null)
                {
                    foreach (var parameter in constructor.BaseConstructor.GetParameters())
                    {
                        parameterTypes.Add(parameter.ParameterType);
                    }
                }

                constructor.Builder = typeBuilder.DefineConstructor(MethodAttributes.Public,
                    CallingConventions.Standard, parameterTypes.ToArray());

                ILGenerator ctor1IL = constructor.Builder.GetILGenerator();
                // For a constructor, argument zero is a reference to the new
                // instance. Push it on the stack before calling the base
                // class constructor. Specify the default constructor of the
                // base class (System.Object) by passing an empty array of
                // types (Type.EmptyTypes) to GetConstructor.
                ctor1IL.Emit(OpCodes.Ldarg_0);
                if (constructor.BaseConstructor != null)
                {
                    var baseConstructorParameters = constructor.BaseConstructor.GetParameters();
                    for (var i = 0; i < baseConstructorParameters.Length; i++)
                    {
                        var argIndex = constructor.PropertiesToInitialize.Count + i + 1;
                        if (argIndex == 1)
                        {
                            ctor1IL.Emit(OpCodes.Ldarg_1);
                        }
                        else if (argIndex == 2)
                        {
                            ctor1IL.Emit(OpCodes.Ldarg_2);
                        }
                        else if (argIndex == 3)
                        {
                            ctor1IL.Emit(OpCodes.Ldarg_3);
                        }
                        else
                        {
                            ctor1IL.Emit(OpCodes.Ldarg_S, argIndex);
                        }
                    }

                    ctor1IL.Emit(OpCodes.Call, constructor.BaseConstructor);
                }
                else
                {
                    ctor1IL.Emit(OpCodes.Call, BaseClass.GetConstructor(Type.EmptyTypes));
                }

                for (var i = 0; i < constructor.PropertiesToInitialize.Count; i++)
                {
                    var property = constructor.PropertiesToInitialize[i];
                    // Push the instance on the stack before pushing the argument
                    // that is to be assigned to the private field m_number.
                    ctor1IL.Emit(OpCodes.Ldarg_0);

                    var argIndex = i + 1;
                    if (argIndex == 1)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_1);
                    }
                    else if (argIndex == 2)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_2);
                    }
                    else if (argIndex == 3)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_3);
                    }
                    else
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_S, argIndex);
                    }

                    ctor1IL.Emit(OpCodes.Stfld, property.Field);
                }

                for (var i = 0; i < constructor.MethodsToInitialize.Count; i++)
                {
                    var method = constructor.MethodsToInitialize[i];
                    // Push the instance on the stack before pushing the argument
                    // that is to be assigned to the private field m_number.
                    ctor1IL.Emit(OpCodes.Ldarg_0);

                    var argIndex = constructor.PropertiesToInitialize.Count + i + 1;
                    if (argIndex == 1)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_1);
                    }
                    else if (argIndex == 2)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_2);
                    }
                    else if (argIndex == 3)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_3);
                    }
                    else
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_S, argIndex);
                    }

                    ctor1IL.Emit(OpCodes.Stfld, method.Field);
                }

                ctor1IL.Emit(OpCodes.Ret);
            }

            foreach (var property in Properties)
            {
                // Define a property named Number that gets and sets the private
                // field.
                //
                // The last argument of DefineProperty is null, because the
                // property has no parameters. (If you don't specify null, you must
                // specify an array of Type objects. For a parameterless property,
                // use the built-in array with no elements: Type.EmptyTypes)
                property.Builder = typeBuilder.DefineProperty(
                    property.Name,
                    PropertyAttributes.HasDefault,
                    property.Type,
                    null);

                MethodInfo propertyGetterOverride = null;
                MethodInfo propertySetterOverride = null;

                foreach (var baseType in baseTypes)
                {
                    var propertyToOverride = baseType.GetProperty(property.Name);
                    if (propertyToOverride == null)
                    {
                        continue;
                    }

                    if (propertyToOverride.PropertyType == property.Type)
                    {
                        if (propertyToOverride.GetMethod != null)
                        {
                            propertyGetterOverride = propertyToOverride.GetMethod;
                        }

                        if (propertyToOverride.SetMethod != null)
                        {
                            propertySetterOverride = propertyToOverride.SetMethod;
                        }

                        break;
                    }
                }

                // The property "set" and property "get" methods require a special
                // set of attributes.
                MethodAttributes getSetAttr = MethodAttributes.Public |
                                              MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                // Define the "get" accessor method for Number. The method returns
                // an integer and has no arguments. (Note that null could be
                // used instead of Types.EmptyTypes)
                MethodBuilder propertyGetter = typeBuilder.DefineMethod(
                    "get_" + property.Name,
                    propertyGetterOverride == null ? getSetAttr : getSetAttr | MethodAttributes.Virtual,
                    property.Type,
                    Type.EmptyTypes);
                if (propertyGetterOverride != null)
                {
                    typeBuilder.DefineMethodOverride(propertyGetter, propertyGetterOverride);
                }

                ILGenerator propertyGetterIl = propertyGetter.GetILGenerator();
                // For an instance property, argument zero is the instance. Load the
                // instance, then load the private field and return, leaving the
                // field value on the stack.
                propertyGetterIl.Emit(OpCodes.Ldarg_0);
                propertyGetterIl.Emit(OpCodes.Ldfld, property.Field);
                propertyGetterIl.Emit(OpCodes.Ret);

                // Define the "set" accessor method for Number, which has no return
                // type and takes one argument of type int (Int32).
                MethodBuilder propertySetter = typeBuilder.DefineMethod(
                    "set_" + property.Name,
                    propertySetterOverride == null ? getSetAttr : getSetAttr | MethodAttributes.Virtual,
                    null,
                    new Type[] {property.Type});
                if (propertySetterOverride != null)
                {
                    typeBuilder.DefineMethodOverride(propertySetter, propertySetterOverride);
                }

                ILGenerator propertySetterIl = propertySetter.GetILGenerator();
                // Load the instance and then the numeric argument, then store the
                // argument in the field.
                propertySetterIl.Emit(OpCodes.Ldarg_0);
                propertySetterIl.Emit(OpCodes.Ldarg_1);
                propertySetterIl.Emit(OpCodes.Stfld, property.Field);
                propertySetterIl.Emit(OpCodes.Ret);

                // Last, map the "get" and "set" accessor methods to the
                // PropertyBuilder. The property is now complete.
                property.Builder.SetGetMethod(propertyGetter);
                property.Builder.SetSetMethod(propertySetter);
            }

            var methodsThatNeedToBeOverriden = new HashSet<MethodInfo>();

            foreach (var baseType in baseTypes)
            {
                var methodsThatNeedToBeOverridenForThisBaseType = baseType.GetMethods().Where(method =>
                    (method.IsAbstract || baseType.IsInterface) &&
                    !(method.Name.StartsWith("get_") || method.Name.StartsWith("set_")));
                foreach (var method in methodsThatNeedToBeOverridenForThisBaseType)
                {
                    methodsThatNeedToBeOverriden.Add(method);
                }
            }

            foreach (var method in Methods)
            {
                method.Name ??= method.StaticImplementation.Name;

                foreach (var baseType in baseTypes)
                {
                    method.MethodToOverride = baseType.GetMethod(method.Name,
                        method.StaticImplementation.GetParameters().Select(x => x.ParameterType).Skip(1).ToArray());

                    if (method.MethodToOverride != null)
                    {
                        if (methodsThatNeedToBeOverriden.Contains(method.MethodToOverride))
                        {
                            methodsThatNeedToBeOverriden.Remove(method.MethodToOverride);
                        }

                        break;
                    }
                }
            }

            foreach (var methodThatNeedsToBeOverriden in methodsThatNeedToBeOverriden)
            {
                var method = new Method()
                {
                    MethodToOverride = methodThatNeedsToBeOverriden,
                };

                foreach (var staticMethodImplementationSource in StaticMethodImplementationSources)
                {
                    foreach (var possibleImplementation in staticMethodImplementationSource.GetMethods()
                        .Where(x => x.IsStatic))
                    {
                        if (possibleImplementation.Name != methodThatNeedsToBeOverriden.Name)
                        {
                            continue;
                        }

                        if (possibleImplementation.GetParameters().Length - 1 !=
                            methodThatNeedsToBeOverriden.GetParameters().Length)
                        {
                            continue;
                        }

                        if (!baseTypes.Any(baseType =>
                            possibleImplementation.GetParameters()[0].ParameterType.IsAssignableFrom(baseType)))
                        {
                            continue;
                        }

                        var parameters = possibleImplementation.GetParameters().Skip(1)
                            .Zip(methodThatNeedsToBeOverriden.GetParameters());
                        var match = true;
                        foreach (var paramPair in parameters)
                        {
                            if (paramPair.First.ParameterType != paramPair.Second.ParameterType)
                            {
                                match = false;
                            }
                        }

                        if (match)
                        {
                            method.StaticImplementation = possibleImplementation;
                            method.ImplementationMode = MethodImplementationMode.StaticImplementation;
                            break;
                        }
                    }

                    if (method.StaticImplementation != null)
                    {
                        break;
                    }
                }

                Methods.Add(method);
            }

            foreach (var method in Methods)
            {
                if (method.ImplementationMode == MethodImplementationMode.StaticImplementation)
                {
                    method.Name ??= method.StaticImplementation.Name;

                    method.Builder =
                        typeBuilder.DefineMethod(
                            method.Name,
                            MethodAttributes.Public
                            | MethodAttributes.Virtual
                            //| MethodAttributes.Final
                            | MethodAttributes.HideBySig
                            | MethodAttributes.NewSlot,
                            method.StaticImplementation.ReturnType,
                            method.StaticImplementation.GetParameters().Skip(1).Select(p => p.ParameterType).ToArray());
                    var methodIl = method.Builder.GetILGenerator();
                    //methodIl.Emit(OpCodes.Nop);
                    methodIl.Emit(OpCodes.Ldarg_0);

                    var extensionMethodParameters =
                        method.StaticImplementation.GetParameters().Skip(1).ToImmutableList();
                    for (var i = 0; i < extensionMethodParameters.Count; i++)
                    {
                        if (i == 0)
                        {
                            methodIl.Emit(OpCodes.Ldarg_1);
                        }
                        else
                        {
                            methodIl.Emit(OpCodes.Ldarg_S, i + 1);
                        }
                    }

                    if (method.StaticImplementation.ReturnType != null &&
                        method.StaticImplementation.ReturnType != typeof(void))
                    {
                        var localVariable = methodIl.DeclareLocal(method.StaticImplementation.ReturnType);
                        methodIl.EmitCall(OpCodes.Call, method.StaticImplementation,
                            method.StaticImplementation.GetParameters().Select(p => p.ParameterType).ToArray());
                        methodIl.Emit(OpCodes.Stloc_0, localVariable);
                        methodIl.Emit(OpCodes.Ldloc_0);
                    }
                    else
                    {
                        methodIl.EmitCall(OpCodes.Call, method.StaticImplementation,
                            method.StaticImplementation.GetParameters().Select(p => p.ParameterType).ToArray());
                    }

                    methodIl.Emit(OpCodes.Ret);

                    if (method.MethodToOverride != null)
                    {
                        typeBuilder.DefineMethodOverride(method.Builder, method.MethodToOverride);
                    }
                }
                else if (method.ImplementationMode == MethodImplementationMode.DelegateFromConstructorParameter)
                {
                    throw new NotImplementedException();
                }
                else if (method.ImplementationMode ==
                         MethodImplementationMode.UnchangingReturnValueFromConstructorParameter)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            // Finish the type.
            Type t = typeBuilder.CreateType();

            return t;
        }

        private Type GetDelegateType(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            var parameterTypes = parameters.Select(x => x.ParameterType).ToArray();
            if (methodInfo.ReturnType == null || methodInfo.ReturnType == typeof(void))
            {
                if (parameters.Length == 0)
                {
                    return typeof(Action);
                }

                var actionType = Type.GetType($"System.Action`{parameters.Length}")
                    .MakeGenericType(parameterTypes);
                return actionType;
            }
            else
            {
                if (parameters.Length == 0)
                {
                    return typeof(Func<>).MakeGenericType(methodInfo.ReturnType);
                }

                var funcType = Type.GetType($"System.Func`{parameters.Length + 1}")
                    .MakeGenericType(parameterTypes.Concat(new[] {methodInfo.ReturnType}).ToArray());
                return funcType;
            }
        }
    }

    public class Method
    {
        public string Name { get; set; }
        public MethodBuilder Builder { get; set; }
        public MethodInfo MethodToOverride { get; set; }
        public Type ReturnType { get; set; }
        public List<Type> Parameters { get; } = new List<Type>();
        internal FieldBuilder Field { get; set; }
        public MethodImplementationMode ImplementationMode { get; set; }
    }

    public class MethodImplementationMode : Either<StaticMethodImplementation, DelegateFromConstructorParameter,
        UnchangingReturnValueFromConstructorParameter>
    {
        public MethodImplementationMode(StaticMethodImplementation item1) : base(item1)
        {
        }

        public MethodImplementationMode(DelegateFromConstructorParameter item2) : base(item2)
        {
        }

        public MethodImplementationMode(UnchangingReturnValueFromConstructorParameter item3) : base(item3)
        {
        }
    }

    public record DelegateFromConstructorParameter { }
    public record UnchangingReturnValueFromConstructorParameter { }

    public class StaticMethodImplementation
    {
        public Dictionary<string, StaticMethodParameterImplementation> ParameterSettings { get; } =
            new Dictionary<string, StaticMethodParameterImplementation>();
        public MethodInfo StaticMethod { get; set; }
    }

    public class StaticMethodParameterImplementation : Either<StaticMethodParameterPullFromConstructor, StaticMethodParameterIncludeInMethodImplementation, StaticMethodParameterConstant>
    {
        public StaticMethodParameterImplementation(StaticMethodParameterPullFromConstructor item1) : base(item1)
        {
        }

        public StaticMethodParameterImplementation(StaticMethodParameterIncludeInMethodImplementation item2) : base(item2)
        {
        }

        public StaticMethodParameterImplementation(StaticMethodParameterConstant item3) : base(item3)
        {
        }
    }

    public record StaticMethodParameterPullFromConstructor() { }
    public record StaticMethodParameterIncludeInMethodImplementation() { }
    public record StaticMethodParameterConstant(object Value) { }

    public class Constructor
    {
        public List<Property> PropertiesToInitialize { get; } = new List<Property>();
        public List<Method> MethodsToInitialize { get; } = new List<Method>();
        public ConstructorBuilder Builder { get; set; }
        public ConstructorInfo BaseConstructor { get; set; }
    }

    public class Property
    {
        public string Name { get; init; }
        public Type Type { get; init; }
        internal FieldBuilder Field { get; set; }
        public PropertyBuilder Builder { get; set; }
        public PropertyInfo PropertyToOverride { get; set; }
    }

    public interface IMarkerInterface1
    {
        public string Joe { set; }
        public int Hello(int a, int b);
    }
    
    public interface IMarkerInterface2
    {
        public string Blob { get; }
        public string Hello();
        public void Hello(int a);
    }
    
    public static class FunExtensions {
        public static string Hello(IMarkerInterface2 markerInterface1)
        {
            return markerInterface1.Blob;
        }
        
        public static int Hello(IMarkerInterface1 markerInterface1, int a, int b)
        {
            return a + b;
        }
        
        public static void Hello(IMarkerInterface1 markerInterface1, int a)
        {
            Console.WriteLine(a);
        }
    }

    public class Query
    {
        public static IQueryable<T> GetItems<T>(DbContext context) where T : class
        {
            return context.Set<T>();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var classBuilder = new ClassBuilder()
            {
                Name = "MyClass",
                IncludeConstructorForAllProperties = true,
                StaticMethodImplementationSources = { typeof(FunExtensions) },
                Properties =
                {
                    new Property()
                    {
                        Name = "id",
                        Type = typeof(Guid),
                    },
                    new Property()
                    {
                        Name = "title",
                        Type = typeof(string),
                    },
                    new Property()
                    {
                        Name = "isbn",
                        Type = typeof(string),
                    },
                },
                // Add a default constructor for EntityFrameworkCore
                Constructors = { new Constructor() }
            };
            
            var clazz = classBuilder.Build();

            //var dbContextType = typeof(MyDbContext<>).MakeGenericType(clazz);
            var dbContextType = new ClassBuilder()
            {
                Name = "CustomDbContext",
                BaseClass = typeof(DbContext),
                IncludeConstructorForAllProperties = false,
                Properties =
                {
                    new Property()
                    {
                        Name = "Books",
                        Type = typeof(DbSet<>).MakeGenericType(clazz),
                    }
                },
                Constructors =
                {
                    new Constructor()
                    {
                        BaseConstructor = typeof(DbContext).GetConstructor(new[] {typeof(DbContextOptions)})
                    }
                }
            }.Build();
            var options = new DbContextOptionsBuilder()
                .UseNpgsql("Host=localhost;Username=changeme;Password=changeme;Database=changeme")
                .Options;
            //var dbContext = (DbContext)Activator.CreateInstance(dbContextType, options);

            var queryType = new ClassBuilder()
            {
                Name = "Query",
                Methods =
                {
                    new Method()
                    {
                        Name = "GetBooks",
                        
                    }
                }
            }
            
            var services = new ServiceCollection();
            services.AddSingleton(options);
            services.AddSingleton(dbContextType);
            var serviceProvider = services.BuildServiceProvider();
            
            var item = Activator.CreateInstance(clazz, Guid.NewGuid(), "Hello world!", "ISBN1");
            
            var schemaBuilder = new SchemaBuilder()
                .AddServices(serviceProvider)
                .AddQueryType(dbContextType);
            
            var schema = schemaBuilder.Create();
            var executableSchema = schema.MakeExecutable();
            var result = executableSchema.ExecuteAsync(new QueryRequestBuilder().SetQuery("query { book { id isbn } }").Create());
            result.Wait();
            Console.WriteLine(result.Result.ToJson());

            // var obj = (IMarkerInterface2)Activator.CreateInstance(clazz, "hi", "a", "b");
            // var x = obj.Blob;
            // Console.WriteLine(obj.Hello());
            // Console.WriteLine(((IMarkerInterface1)obj).Hello(5, 6));
            // obj.Hello(4);
            // Console.WriteLine(x);
        }
    }

    public interface IMyDbContext
    {
        void Add(object obj);
        IQueryable<object> Items { get; }
    }
    
    public class MyDbContext<TEntity> : DbContext, IMyDbContext where TEntity : class
    {
        private readonly string _tableName;

        public MyDbContext(string tableName) : base(GetOptions())
        {
            _tableName = tableName;
        }

        private static DbContextOptions GetOptions()
        {
            var options = new DbContextOptionsBuilder()
                .UseNpgsql("Host=localhost;Username=changeme;Password=changeme;Database=changeme")
                .Options;
            return options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TEntity>().ToTable(_tableName);
        }

        public void Add(object obj)
        {
            Entities.Add((TEntity) obj);
            SaveChanges();
        }

        public IQueryable<object> Items => Entities;

        public DbSet<TEntity> Entities { get; set; }
    }
}