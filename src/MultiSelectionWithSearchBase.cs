using NP.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NP.Concepts.Behaviors
{
    public abstract class MultiSelectionWithSearchBase<T> :
        VMBase, IMultiSelectionWithSearchVm
        where T : ISelectableItem<T>, ISearchable
    {
        private string _lowerCaseSearchStr;

        IDisposable _disposableBehavior;

        public IEnumerable AllItemsToDisplay => AllItems;

        private IEnumerable<T> _allItems;
        public IEnumerable<T> AllItems
        {
            get => _allItems;

            protected set
            {
                if (ReferenceEquals(_allItems, value))
                    return;

                _disposableBehavior?.Dispose();

                _allItems = value;

                if (_allItems != null)
                {
                    _disposableBehavior = _allItems.AddBehavior
                    (
                        item => item.IsSelectedChanged += OnItemSelectionChanged,
                        item => item.IsSelectedChanged -= OnItemSelectionChanged
                    );
                }
            }
        }

        protected abstract void OnItemSelectionChanged(T obj);

        #region SearchStr Property
        private string _searchStr;
        public string SearchStr
        {
            get
            {
                return this._searchStr;
            }
            set
            {
                if (this._searchStr == value)
                {
                    return;
                }

                this._searchStr = value;
                this.OnPropertyChanged(nameof(SearchStr));

                _lowerCaseSearchStr = _searchStr?.ToLower();

                foreach (T item in AllItems)
                {
                    item.SearchStr = _lowerCaseSearchStr;
                }
            }
        }
        #endregion SearchStr Property


        public abstract IEnumerable<T> SelectedItems { get; }

        public IEnumerable SelectedItemsToDisplay =>
            SelectedItems;
    }
}
