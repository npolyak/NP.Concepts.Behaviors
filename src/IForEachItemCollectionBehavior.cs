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
using System.Linq;

namespace NP.Concepts.Behaviors
{
    public interface IForEachItemCollectionBehavior<TCollItem> :
        ICollectionStatelessBehavior<TCollItem>
    {
        private void SetItems(IEnumerable items)
        {
            if (items == null)
                return;

            foreach (TCollItem item in items)
            {
                if (item is TCollItem behaviorItem)
                {
                    OnItemAdded(behaviorItem);
                }
            }
        }

        private void UnsetItems(IEnumerable items)
        {
            if (items == null)
                return;

            foreach (TCollItem item in items)
            {
                if (item is TCollItem behaviorItem)
                {
                    OnItemRemoved(behaviorItem);
                }
            }
        }


        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsetItems(e.OldItems);
            SetItems(e.NewItems);
        }

        void DetachImpl(IEnumerable<TCollItem> collection, bool unsetItems = true)
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
                UnsetItems(collection.ToList());
            }
        }

        void IStatelessBehavior<IEnumerable<TCollItem>>.Detach(IEnumerable<TCollItem> collection, bool unsetItems = true)
        {
            DetachImpl(collection, unsetItems);
        }

        void AttachImpl(IEnumerable<TCollItem> collection, bool setItems = true)
        {
            if (collection == null)
                return;

            if (setItems)
            {
                SetItems(collection);
            }

            INotifyCollectionChanged notifiableCollection =
                collection as INotifyCollectionChanged;

            if (notifiableCollection != null)
            {
                notifiableCollection.CollectionChanged += Collection_CollectionChanged;
            }
        }

        void IStatelessBehavior<IEnumerable<TCollItem>>.Attach(IEnumerable<TCollItem> collection, bool setItems = true)
        {
            AttachImpl(collection, setItems);
        }
    }
}
