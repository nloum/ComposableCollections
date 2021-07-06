using System;

namespace UtilityDisposables
{
    /// <summary>
    /// A quick and easy way to create an IDisposable by specifying the actions that should occur when
    /// the resulting IDisposable is disposed.
    /// </summary>
    public class AnonymousDisposable : IDisposable
    {
        private readonly Action[] _actions;

        public AnonymousDisposable(params Action[] actions)
        {
            _actions = actions;
            if (actions == null)
                throw new NullReferenceException("actions");
            for (var i = 0; i < actions.Length; i++)
            {
                if (actions[i] == null)
                    throw new NullReferenceException(string.Format("actions[{0}]", i));
            }
        }

        public void Dispose()
        {
            foreach (var action in _actions)
            {
                action();
            }
        }
    }
}
