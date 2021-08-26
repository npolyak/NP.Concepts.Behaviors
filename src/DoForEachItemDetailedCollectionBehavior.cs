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
    public class DoForEachItemDetailedCollectionBehavior<TCollItem> :
        IForEachItemDetailedCollectionBehavior<TCollItem>
    {
        Action<IEnumerable<TCollItem>, TCollItem, int> UnsetItemDelegate { get; }
        Action<IEnumerable<TCollItem>, TCollItem, int> SetItemDelegate { get; }


        void IForEachItemDetailedCollectionBehavior<TCollItem>.OnItemAdded
        (
            IEnumerable<TCollItem> collection,
            TCollItem item,
            int newIdx)
        {
            SetItemDelegate?.Invoke(collection, item, newIdx);
        }

        void IForEachItemDetailedCollectionBehavior<TCollItem>.OnItemRemoved
        (
            IEnumerable<TCollItem> collection, 
            TCollItem item, 
            int oldIdx)
        {
            UnsetItemDelegate?.Invoke(collection, item, oldIdx);
        }

        public DoForEachItemDetailedCollectionBehavior
        (
            Action<IEnumerable<TCollItem>, TCollItem, int> OnAdd,
            Action<IEnumerable<TCollItem>, TCollItem, int> OnRemove = null)
        {
            SetItemDelegate = OnAdd;
            UnsetItemDelegate = OnRemove;
        }
    }

    public static class DoForEachDetailedBehaviorUtils
    {
        public static BehaviorsDisposable<IEnumerable<TCollItem>> AddDetailedBehavior<TCollItem>
        (
            this IEnumerable<TCollItem> collection,
            Action<IEnumerable<TCollItem>, TCollItem, int> onAdd,
            Action<IEnumerable<TCollItem>, TCollItem, int> onRemove
        )
        {
            return collection
                .AddBehavior(new DoForEachItemDetailedCollectionBehavior<TCollItem>(onAdd, onRemove));
        }

        public static BehaviorsDisposable<IEnumerable<TCollItem>> AddDetailedBehavior<TCollItem>
        (
            this BehaviorsDisposable<IEnumerable<TCollItem>> previousBehavior,
            Action<IEnumerable<TCollItem>, TCollItem, int> onAdd,
            Action<IEnumerable<TCollItem>, TCollItem, int> onRemove
        )
        {
            return previousBehavior
                .AddBehavior(new DoForEachItemDetailedCollectionBehavior<TCollItem>(onAdd, onRemove));
        }
    }
}