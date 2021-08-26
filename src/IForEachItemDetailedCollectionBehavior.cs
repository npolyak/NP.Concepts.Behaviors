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

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NP.Concepts.Behaviors
{
    public interface IForEachItemDetailedCollectionBehavior<TCollItem> :
        IStatelessBehavior<IEnumerable<TCollItem>>
    {
        protected void OnItemAdded
        (
            IEnumerable<TCollItem> collection,
            TCollItem item,
            int oldIdx);

        protected void OnItemRemoved
        (
            IEnumerable<TCollItem> collection,
            TCollItem item,
            int newIdx);

        private void UnsetItems
        (
            IEnumerable<TCollItem> collection,
            IEnumerable items,
            int oldStartingIdx)
        {
            if (items == null)
                return;

            int i = oldStartingIdx;
            foreach (TCollItem item in items)
            {
                OnItemRemoved(collection, item, i);
                i++;
            }
        }

        private void SetItems
        (
            IEnumerable<TCollItem> collection,
            IEnumerable items,
            int newStartingIdx)
        {
            if (items == null)
                return;

            int i = newStartingIdx;
            foreach (TCollItem item in items)
            {
                OnItemAdded(collection, item, i);
                i++;
            }
        }


        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsetItems(sender as IEnumerable<TCollItem>, e.OldItems, e.OldStartingIndex);
            SetItems(sender as IEnumerable<TCollItem>, e.NewItems, e.NewStartingIndex);
        }

        void IStatelessBehavior<IEnumerable<TCollItem>>.Detach(IEnumerable<TCollItem> collection, bool unsetItems)
        {
            if (collection == null)
                return;

            INotifyCollectionChanged notifiableCollection =
                collection as INotifyCollectionChanged;

            if (notifiableCollection != null)
            {
                notifiableCollection.CollectionChanged -= Collection_CollectionChanged;
            }

            if (unsetItems)
            {
                UnsetItems(collection, collection, 0);
            }
        }

        void IStatelessBehavior<IEnumerable<TCollItem>>.Attach(IEnumerable<TCollItem> collection, bool setItems)
        {
            if (collection == null)
                return;

            if (setItems)
            {
                SetItems(collection, collection, 0);
            }

            INotifyCollectionChanged notifiableCollection =
                collection as INotifyCollectionChanged;

            if (notifiableCollection != null)
            {
                notifiableCollection.CollectionChanged += Collection_CollectionChanged;
            }
        }
    }
}
