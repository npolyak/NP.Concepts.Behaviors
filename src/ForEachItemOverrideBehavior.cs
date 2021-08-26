// (c) Nick Polyak 2021 - http://awebpros.com/
// License: MIT License (https://opensource.org/licenses/MIT)
//
// short overview of copyright rules:
// 1. you can use this framework in any commercial or non-commercial 
//    product as long as you retain this copyright message
// 2. Do not blame the author of this software if something goes wrong. 
// 
// Also, please, mention this software in any documentation for the 
// products that use it.

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
