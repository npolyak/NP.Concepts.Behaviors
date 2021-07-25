using System.Collections.Generic;

namespace NP.Concepts.Behaviors
{
    public class CollectionAggregator<TCollItem> : IForEachItemDetailedCollectionBehavior<TCollItem>
    {
        public CollectionAggregator()
        {
        }

        void IForEachItemDetailedCollectionBehavior<TCollItem>.OnItemAdded(IEnumerable<TCollItem> collection, TCollItem item, int oldIdx)
        {
            
        }

        void IForEachItemDetailedCollectionBehavior<TCollItem>.OnItemRemoved(IEnumerable<TCollItem> collection, TCollItem item, int newIdx)
        {
            
        }
    }
}
