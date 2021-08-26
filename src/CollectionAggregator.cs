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
