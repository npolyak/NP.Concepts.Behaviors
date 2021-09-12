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
    public class RemoveItemBehavior<T> : ForEachItemOverrideBehavior<T>
        where T : IRemovable
    {
        protected ICollection<T> Items { get; set; }

        public RemoveItemBehavior(ICollection<T> items) : base(items)
        {
            Items = items;
        }

        protected override void OnItemAdded(T item)
        {
            item.RemoveEvent += OnRemoveEventFired;
        }

        protected override void OnItemRemoved(T item)
        {
            item.RemoveEvent -= OnRemoveEventFired;
        }

        private void OnRemoveEventFired(IRemovable item)
        {
            this.OnRemoveItem((T)item);
        }

        protected virtual void OnRemoveItem(T item)
        {
            this.Items?.Remove(item);
        }
    }
}
