// (c) Nick Polyak 2018 - http://awebpros.com/
// License: Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0.html)
//
// short overview of copyright rules:
// 1. you can use this framework in any commercial or non-commercial 
//    product as long as you retain this copyright message
// 2. Do not blame the author of this software if something goes wrong. 
// 
// Also, please, mention this software in any documentation for the 
// products that use it.
using NP.Utilities.BasicInterfaces;
using System;

namespace NP.Concepts.Behaviors
{
    internal class DisposableBehaviorContainer<T> : IDisposable, ISuspendable
    {
        public IStatelessBehavior<T> TheBehavior { get; }
        public T TheObjectTheBehaviorIsAttachedTo { get; }

        public DisposableBehaviorContainer
        (
            IStatelessBehavior<T> behavior,
            T objectTheBehaviorIsAttachedTo
        )
        {
            TheBehavior = behavior;
            TheObjectTheBehaviorIsAttachedTo = objectTheBehaviorIsAttachedTo;

            TheBehavior.Attach(TheObjectTheBehaviorIsAttachedTo);
        }

        public void Suspend()
        {
            TheBehavior?.Detach(TheObjectTheBehaviorIsAttachedTo, false);
        }

        public void Dispose()
        {
            TheBehavior?.Detach(TheObjectTheBehaviorIsAttachedTo, true);
        }

        public void ResetBehavior(bool resetItems = true)
        {
            Reset(resetItems);
        }

        public void Reset(bool resetItems = true)
        {
            TheBehavior?.Reset(TheObjectTheBehaviorIsAttachedTo, resetItems);
        }
    }
}
