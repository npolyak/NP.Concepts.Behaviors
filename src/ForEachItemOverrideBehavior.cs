using System;
using System.Collections.Generic;

namespace NP.Concepts.Behaviors
{
    public abstract class ForEachItemOverrideBehavior<T> : IDisposable
    {
        IDisposable _behavior;

        public ForEachItemOverrideBehavior(IEnumerable<T>? items)
        {
            _behavior = items?.AddBehavior(OnItemAdded, OnItemRemoved);
        }

        protected abstract void OnItemAdded(T item);

        protected abstract void OnItemRemoved(T item);

        public virtual void Dispose()
        {
            _behavior?.Dispose();
            _behavior = null;
        }
    }
}
