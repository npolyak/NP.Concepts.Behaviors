using NP.Concepts.Behaviors;
using System;
using System.Collections.Generic;

namespace NP.Concepts.Behaviors
{
    public class RemoveItemBehavior<T> : ForEachItemOverrideBehavior<T>
        where T : IRemovable
    {
        IDisposable _behavior;

        private ICollection<T> Items { get; set; }

        public RemoveItemBehavior(ICollection<T> items) : base(items)
        {
            Items = items;
        }

        protected override void OnItemAdded(T item)
        {
            item.RemoveEvent += Item_RemoveEvent;
        }

        protected override void OnItemRemoved(T item)
        {
            item.RemoveEvent -= Item_RemoveEvent;
        }

        private void Item_RemoveEvent(IRemovable item)
        {
            this.Items?.Remove((T)item);
        }
    }
}
