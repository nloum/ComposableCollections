using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericNumbers
{
    public interface INumberBound<out T>
    {
        T Value { get; }
        bool IsStrict { get; }
        bool IsLower { get; }
        bool IsUpper { get; }
        bool IsRelative { get; }
        INumberBound<T> ChangeStrictness(bool newIsStrict);
    }
}
