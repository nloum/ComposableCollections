using System;
using Microsoft.Extensions.Primitives;
using Disposable = System.Reactive.Disposables.Disposable;

namespace IoFluently
{
	public class EmptyChangeToken : IChangeToken {
		public IDisposable RegisterChangeCallback( Action<object> callback, object state ) {
			return Disposable.Empty;
		}

		public bool HasChanged => false;
		public bool ActiveChangeCallbacks => false;
	}
}