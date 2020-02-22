using System.Collections.Generic;
using System.Text;

namespace SimpleMonads
{
    public interface IEither<out T1, out T2>
    {
        IMaybe<T1> Item1 { get; }
        IMaybe<T2> Item2 { get; }
    }

    public interface IEither<out T1, out T2, out T3>
    {
        IMaybe<T1> Item1 { get; }
        IMaybe<T2> Item2 { get; }
        IMaybe<T3> Item3 { get; }
    }

    public interface IEither<out T1, out T2, out T3, out T4>
    {
        IMaybe<T1> Item1 { get; }
        IMaybe<T2> Item2 { get; }
        IMaybe<T3> Item3 { get; }
        IMaybe<T4> Item4 { get; }
    }
}