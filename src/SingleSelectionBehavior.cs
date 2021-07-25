using NP.Utilities;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NP.Concepts.Behaviors
{
    public class SingleSelectionBehavior<TElement, TSelectable> : VMBase
        where TSelectable : class, ISelectableItem<TSelectable>
    {
        IDisposable _behaviorDisposable = null;
        public event Action SelectedItemChangedEvent = null;


        #region TheCollection Property
        private IEnumerable<TElement> _collection;
        public IEnumerable<TElement> TheCollection
        {
            get => _collection;
            set
            {
                if (ReferenceEquals(_collection, value))
                    return;

                _collection = value;

                _behaviorDisposable =
                    _collection?.AddBehavior
                    (
                        (item) => ((TSelectable)(object)item).IsSelectedChanged += Item_IsSelectedChanged,
                        (item) => ((TSelectable)(object)item).IsSelectedChanged -= Item_IsSelectedChanged
                    )
                    .AddBehavior<TElement>
                    (
                        null, 
                        (item) =>
                        {
                            if (item.ObjEquals(TheSelectedItem) && (TheSelectedItem != null))
                            {
                                TheSelectedItem = null;
                            }
                        }
                    );
            }
        }
        #endregion TheCollection Property

        public SingleSelectionBehavior()
        {

        }

        public SingleSelectionBehavior(IEnumerable<TElement> collection)
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

    public class SingleSelectionBehavior<TSelectable> : SingleSelectionBehavior<TSelectable, TSelectable>
         where TSelectable : class, ISelectableItem<TSelectable>
    {
        public SingleSelectionBehavior()
        {

        }

        public SingleSelectionBehavior(IEnumerable<TSelectable> collection)
        {
            TheCollection = collection;
        }
    }
}
