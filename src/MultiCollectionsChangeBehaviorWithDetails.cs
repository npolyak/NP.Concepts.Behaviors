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

using NP.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
namespace NP.Concepts.Behaviors
{
    public abstract class MultiCollectionsChangeBehaviorWithDetails<T> 
        : VMBase, IForEachItemCollectionBehavior<IEnumerable<T>>
    {
        public ObservableCollection<IEnumerable<T>> Collections { get; } =
            new ObservableCollection<IEnumerable<T>>();

        void ICollectionItemBehavior<IEnumerable<T>>.OnItemAdded(IEnumerable<T> item)
        {
            _individualCollectionItemBehavior.Attach(item);
        }

        void ICollectionItemBehavior<IEnumerable<T>>.OnItemRemoved(IEnumerable<T> item)
        {
            _individualCollectionItemBehavior.Detach(item);
        }

        protected abstract void OnCollectionItemAdded(IEnumerable<T> collection, T item, int idx);

        protected abstract void OnCollectionItemRemoved(IEnumerable<T> collection, T item, int oldIdx);

        protected IForEachItemDetailedCollectionBehavior<T> _individualCollectionItemBehavior;
        
        public SynchronizationContext TheSyncContext { get; set; }


        private void DettachCollectionImpl()
        {
            (this as IForEachItemCollectionBehavior<IEnumerable<T>>).Detach(Collections);
        }


        private void AttachCollectionImpl()
        {
            (this as IForEachItemCollectionBehavior<IEnumerable<T>>).Attach(Collections);
        }

        public virtual void DettachCollections()
        {
            TheSyncContext.RunWithinContext(AttachCollectionImpl);
        }

        public virtual void AttachCollections()
        {
            TheSyncContext.RunWithinContext(AttachCollectionImpl);
        }

        public MultiCollectionsChangeBehaviorWithDetails()
        {
            _individualCollectionItemBehavior = 
                new DoForEachItemDetailedCollectionBehavior<T>(OnCollectionItemAdded, OnCollectionItemRemoved);

            AttachCollections();
        }
    }
}
