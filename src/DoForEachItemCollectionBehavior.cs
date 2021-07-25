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
using System;
using System.Collections;
using System.Collections.Generic;

namespace NP.Concepts.Behaviors
{
    public class DoForEachItemCollectionBehavior<TCollItem> :
        IForEachItemCollectionBehavior<TCollItem>
    {
        Action<TCollItem> UnsetItemDelegate { get; }
        Action<TCollItem> SetItemDelegate { get; }

        void ICollectionItemBehavior<TCollItem>.OnItemAdded(TCollItem item)
        {
            SetItemDelegate?.Invoke(item);
        }

        void ICollectionItemBehavior<TCollItem>.OnItemRemoved(TCollItem item)
        {
            UnsetItemDelegate?.Invoke(item);
        }

        public DoForEachItemCollectionBehavior
        (
            Action<TCollItem> OnAdd, 
            Action<TCollItem> OnRemove = null)
        {
            SetItemDelegate = OnAdd;
            UnsetItemDelegate = OnRemove;
        }
    }

    public static class DoForEachBehaviorUtils
    {
        private static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehaviorImpl<TCollItem>
        (
            this IEnumerable<TCollItem> collection, 
            IStatelessBehavior<IEnumerable<TCollItem>> behavior,
            BehaviorsDisposable<IEnumerable<TCollItem>> previousBehavior = null)
        {
            if (collection == null)
                return null;

            DisposableBehaviorContainer<IEnumerable<TCollItem>> behaviorContainer =
                new DisposableBehaviorContainer<IEnumerable<TCollItem>>(behavior, collection);

            BehaviorsDisposable<IEnumerable<TCollItem>> behaviorsDisposable =
                new BehaviorsDisposable<IEnumerable<TCollItem>>(behaviorContainer, previousBehavior);

            return behaviorsDisposable;
        }

        public static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehavior<TCollItem>
        (
            this IEnumerable<TCollItem> collection,
            IStatelessBehavior<IEnumerable<TCollItem>> behavior
        )
        {
            return AddBehaviorImpl(collection, behavior);
        }

        public static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehavior<TCollItem>
        (
            this BehaviorsDisposable<IEnumerable<TCollItem>> previousBehavior,
            IStatelessBehavior<IEnumerable<TCollItem>> behavior
        )
        {
            IEnumerable<TCollItem> collection = previousBehavior.TheObjectTheBehaviorsAreAttachedTo;
            return AddBehaviorImpl(collection, behavior, previousBehavior);
        }

        private static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehaviorImpl<TCollItem>
        (
            this IEnumerable<TCollItem> collection,
            DoForEachItemCollectionBehavior<TCollItem> behavior,
            BehaviorsDisposable<IEnumerable<TCollItem>> previousBehavior = null
        )
        {
            if (collection == null)
                return null;

            DisposableBehaviorContainer<IEnumerable<TCollItem>> behaviorContainer =
                new DisposableBehaviorContainer<IEnumerable<TCollItem>>(behavior, collection);

            BehaviorsDisposable<IEnumerable<TCollItem>> behaviorsDisposable =
                new BehaviorsDisposable<IEnumerable<TCollItem>>(behaviorContainer, previousBehavior);

            return behaviorsDisposable;
        }

        private static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehaviorImpl<TCollItem>
        (
            this IEnumerable<TCollItem> collection,
            Action<TCollItem> onAdd,
            Action<TCollItem> onRemove = null,
            BehaviorsDisposable<IEnumerable<TCollItem>> previousBehavior = null
        )
        {
            DoForEachItemCollectionBehavior<TCollItem> behavior =
                new DoForEachItemCollectionBehavior<TCollItem>(onAdd, onRemove);

            return collection?.AddBehaviorImpl<TCollItem>(behavior, previousBehavior);
        }

        public static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehavior<TCollItem>
        (
            this IEnumerable<TCollItem> collection,
            DoForEachItemCollectionBehavior<TCollItem> behavior
        )
        {
            return collection.AddBehaviorImpl(behavior);
        }

        public static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehavior<TCollItem>
        (
            this IEnumerable<TCollItem> collection, 
            Action<TCollItem> onAdd,
            Action<TCollItem> onRemove = null
        )
        {
            return collection.AddBehaviorImpl<TCollItem>(onAdd, onRemove);
        }

        public static BehaviorsDisposable<IEnumerable<TCollItem>> AddBehavior<TCollItem>
        (
            this BehaviorsDisposable<IEnumerable<TCollItem>> previousBehaviors,
            Action<TCollItem> onAdd,
            Action<TCollItem> onRemove = null
        )
        {
            IEnumerable<TCollItem> collection = previousBehaviors.TheObjectTheBehaviorsAreAttachedTo;

            return collection.AddBehaviorImpl<TCollItem>(onAdd, onRemove, previousBehaviors);
        }
    }
}
