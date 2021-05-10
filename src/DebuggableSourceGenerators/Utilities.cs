using System.Threading.Tasks;

namespace DebuggableSourceGenerators
{
    public static class Utilities
    {
        public static T WaitForResult<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}