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

using System.Collections.Generic;

namespace NP.Concepts.Behaviors
{
    public interface IStatelessBehavior<in T>
    {
        void Attach(T obj, bool setItems = true);

        void Detach(T obj, bool unsetItems = true);
    }

    public interface ICollectionItemBehavior<in T>
    {
        protected void OnItemAdded(T item);
        protected void OnItemRemoved(T item);
    }

    public interface ICollectionStatelessBehavior<in T> : IStatelessBehavior<IEnumerable<T>>, ICollectionItemBehavior<T>
    {

    }

    public static class StatelessBehaviorUtils
    {
        public static void Reset<T>(this IStatelessBehavior<T> behavior, T obj, bool resetItems = true)
        {
            behavior.Detach(obj, resetItems);
            behavior.Attach(obj, resetItems);
        }
    }
}
