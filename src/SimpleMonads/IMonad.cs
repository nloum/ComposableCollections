using System;

namespace SimpleMonads
{
    public interface IMonad<out TElement>
    {
        TMonad2 Bind<TMonad2, TElement2>(Func<TElement, TMonad2> f)
            where TMonad2 : IMonad<TElement2>;
    }
}