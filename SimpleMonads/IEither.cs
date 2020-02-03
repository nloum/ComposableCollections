using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMonads
{
    public interface IEither<out T1, out T2>
    {
        IMaybe<T1> Item1 { get; }
        IMaybe<T2> Item2 { get; }
    }
}
