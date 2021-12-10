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

namespace NP.Concepts.Behaviors
{
    public class ForEachItemEventCollectionBehavior<TCollItem> : 
        IForEachItemCollectionBehavior<TCollItem>
    {
        public event Action<TCollItem> UnsetItemEvent;
        public event Action<TCollItem> SetItemEvent;

        void ICollectionItemBehavior<TCollItem>.OnItemAdded(TCollItem item)
        {
            UnsetItemEvent?.Invoke(item);
        }

        void ICollectionItemBehavior<TCollItem>.OnItemRemoved(TCollItem item)
        {
            SetItemEvent?.Invoke(item);
        }
    }
}
