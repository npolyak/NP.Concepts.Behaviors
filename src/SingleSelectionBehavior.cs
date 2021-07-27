using NP.Utilities;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NP.Concepts.Behaviors
{
    public class SingleSelectionBehavior<TSelectable> : VMBase
        where TSelectable : class, ISelectableItem<TSelectable>
    {
        IDisposable _behaviorDisposable = null;
        public event Action SelectedItemChangedEvent = null;


        #region TheCollection Property
        private IEnumerable<TSelectable> _collection;
        public IEnumerable<TSelectable> TheCollection
        {
            get => _collection;
            set
            {
                if (ReferenceEquals(_collection, value))
                    return;

                _collection = value;

                _behaviorDisposable =
                    _collection?.AddBehavior(OnItemAdded, OnItemRemoved);

                OnCollectionSet();
            }
        }
        #endregion TheCollection Property

        public SingleSelectionBehavior()
        {

        }

        protected virtual void OnCollectionSet()
        {

        }

        protected virtual void BeforeItemAdded(TSelectable item)
        {

        }

        private void OnItemAdded(TSelectable item)
        {
            BeforeItemAdded(item);

            if (item.IsSelected)
            {
                Item_IsSelectedChanged(item);
            }

            item.IsSelectedChanged += Item_IsSelectedChanged;
        }

        private void OnItemRemoved(TSelectable item)
        {
            item.IsSelectedChanged -= Item_IsSelectedChanged;

            if (item.ObjEquals(TheSelectedItem) && TheSelectedItem != null)
            {
                DoOnSelectedItemRemoved();
            }
        }

        protected virtual void DoOnSelectedItemRemoved()
        {
            TheSelectedItem = null;
        }

        public SingleSelectionBehavior(IEnumerable<TSelectable> collection)
        {
            TheCollection = collection;
        }

        private void Item_IsSelectedChanged(ISelectableItem<TSelectable> item)
        {
            if (item.IsSelected)
            {
                this.TheSelectedItem = (TSelectable)item;
            }
            else
            {
                TheSelectedItem = null;
            }
        }

        #region TheSelectedItem Property
        private TSelectable _selectedItem;
        [XmlIgnore]
        public TSelectable TheSelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                if (ReferenceEquals(_selectedItem, value))
                {
                    return;
                }

                if (_selectedItem != null)
                {
                    _selectedItem.IsSelected = false;
                }

                this._selectedItem = value;

                if (_selectedItem != null)
                {
                    _selectedItem.IsSelected = true;
                }

                SelectedItemChangedEvent?.Invoke();
                this.OnPropertyChanged(nameof(TheSelectedItem));
            }
        }
        #endregion TheSelectedItem Property
    }
}
