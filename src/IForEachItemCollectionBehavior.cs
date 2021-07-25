using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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
