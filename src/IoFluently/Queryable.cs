using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Castle.DynamicProxy.Contributors;
using TreeLinq;

namespace IoFluently {
    /// <summary>
    /// IQueryable implementation that converts expressions like AbsolutePaths.Where(path => path.Contains("test")) into
    /// efficient calls to the .NET file system APIs.
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class Queryable<T> : IOrderedQueryable<T>
    {
        public Queryable(IQueryContext queryContext)
        {
            Initialize(new QueryProvider(queryContext), null);
        }
 
        public Queryable(IQueryProvider provider)
        {
            Initialize(provider, null);
        }
 
        internal Queryable(IQueryProvider provider, Expression expression)
        {
            Initialize(provider, expression);
        }
 
        private void Initialize(IQueryProvider provider, Expression expression)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (expression != null && !typeof(IQueryable<T>).
                IsAssignableFrom(expression.Type))
                throw new ArgumentException(
                    String.Format("Not assignable from {0}", expression.Type), "expression");
 
            Provider = provider;
            Expression = expression ?? Expression.Constant(this);
        }
 
        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }
 
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<System.Collections.IEnumerable>(Expression)).GetEnumerator();
        }
 
        public Type ElementType
        {
            get { return typeof(T); }
        }
 
        public Expression Expression { get; private set; }
        public IQueryProvider Provider { get; private set; }
    }
    
    public interface IQueryContext
    {
        object Execute(Expression expression, bool isEnumerable);
    }

    public class QueryContext : IQueryContext
    {
        public object Execute(Expression expression, bool isEnumerable)
        {
            if (expression is MethodCallExpression methodCallExpression)
            {
                var wheres = typeof(System.Linq.Queryable).GetMethods().Where(method => method.Name == "Where");
                var whereMethod = wheres.Single(method =>
                {
                    var lambdaParameter = method.GetParameters()[1];
                    var funcType = lambdaParameter.ParameterType.GetGenericArguments()[0];
                    return funcType.GetGenericArguments().Length == 2;
                });
                if (methodCallExpression.Method.GetGenericMethodDefinition() == whereMethod)
                {
                    var expr = methodCallExpression.Arguments[1];
                    if (expr is UnaryExpression unaryExpression)
                    {
                        var lambdaExpression = unaryExpression.Operand as LambdaExpression;
                        if (lambdaExpression.Body is BinaryExpression methodBinaryExpression)
                        {
                            if (methodBinaryExpression.Method == typeof(AbsolutePath).GetMethod("op_Equality"))
                            {
                                var absolutePath = (AbsolutePath)GetConstant(methodBinaryExpression.Right);

                                var isParent = false;
                                var parentExpr = methodBinaryExpression.Left;
                                var parentOf = lambdaExpression.Parameters[0];

                                if (IsParentExpression(parentExpr, parentOf))
                                {
                                    return Directory.GetFileSystemEntries(absolutePath.ToString()).Select(fse => 
                                        absolutePath.IoService.ParseAbsolutePath(fse));
                                }
                            }
                        }
                        else if (lambdaExpression.Body is MethodCallExpression methodCallExpression2)
                        {
                            var containsMethod = typeof(System.Linq.Enumerable).GetMethods()
                                .Where(method => method.Name == "Contains")
                                .Single(method => method.GetParameters().Length == 2);

                            if (methodCallExpression2.Method.GetGenericMethodDefinition() == containsMethod)
                            {
                                if (methodCallExpression2.Arguments[0] is MethodCallExpression methodCallExpression3)
                                {
                                    var ancestorsMethod = typeof(IoExtensions).GetMethods()
                                        .Where(method => method.Name == "Ancestors")
                                        .Single(method => method.GetParameters().Length == 1);

                                    var ancestor = (AbsolutePath)GetConstant(methodCallExpression2.Arguments[1]);
                                    
                                    if (methodCallExpression3.Method == ancestorsMethod)
                                    {
                                        var root = ancestor.ToString();
                                        return root.TraverseTree<string>(
                                            path =>
                                            {
                                                if (Directory.Exists(path))
                                                {
                                                    var result = Directory.GetFileSystemEntries(path);
                                                    return result;
                                                }
                                                
                                                return Enumerable.Empty<string>();
                                            }, (string node, string name, out string child) =>
                                            {
                                                child = name;
                                                return File.Exists(child) || Directory.Exists(child);
                                            })
                                            .Where(x => x.Type != TreeTraversalType.ExitBranch && !x.Path.IsRoot)
                                            .Select(x =>
                                            {
                                                return ancestor.IoService.ParseAbsolutePath(x.Value);
                                            });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            throw new NotImplementedException();
        }
        
        private static bool IsParentExpression(Expression parentExpr, ParameterExpression parentOf)
        {
            if (parentExpr is MethodCallExpression mce)
            {
                if (mce.Arguments[0] == parentOf)
                {
                    if (mce.Method == typeof(IoExtensions).GetMethod("Parent"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        private static bool IsAncestorExpression(Expression parentExpr, ParameterExpression parentOf)
        {
            if (parentExpr is MethodCallExpression mce)
            {
                if (mce.Object == parentOf)
                {
                    if (mce.Method == typeof(IoExtensions).GetMethod("Parent"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static object GetConstant(Expression expr)
        {
            if (expr is MemberExpression memberExpression)
            {
                if (memberExpression.Expression is ConstantExpression constantExpression)
                {
                    var constantWrapper = constantExpression.Value.GetType();
                    var property = constantWrapper.GetFields().First();
                    return property.GetValue(constantExpression.Value);
                }
            }

            return null;
        }
    }
    
    public class QueryProvider : IQueryProvider
    {
        private readonly IQueryContext queryContext;
 
        public QueryProvider(IQueryContext queryContext)
        {
            this.queryContext = queryContext;
        }
 
        public virtual IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return               
                    (IQueryable)Activator.CreateInstance(typeof(Queryable<>).
                        MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
 
        public virtual IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new Queryable<T>(this, expression);
        }
 
        object IQueryProvider.Execute(Expression expression)
        {
            return queryContext.Execute(expression, false);
        }
 
        T IQueryProvider.Execute<T>(Expression expression)
        {
            return (T)queryContext.Execute(expression, 
                (typeof(T).Name == "IEnumerable`1"));
        }
    }
    
    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);

            if (ienum == null)
                return seqType;

            return ienum.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null | seqType == typeof(string))
                return null;

            if ((seqType.IsArray))
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

            if ((seqType.IsGenericType))
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);

                    if ((ienum.IsAssignableFrom(seqType)))
                        return ienum;
                }
            }

            Type[] ifaces = seqType.GetInterfaces();

            if (ifaces != null & ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);

                    if ((ienum != null))
                        return ienum;
                }
            }

            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
                return FindIEnumerable(seqType.BaseType);

            return null;
        }
    }
}