using NP.Utilities;
using NP.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NP.Concepts.Behaviors
{
    public class FilterCollectionBehavior<T>
    {
        private Func<T, bool> _filter;
        public Func<T, bool> Filter 
        {
            get => _filter;
            
            set
            {
                _filter = value;
                SetBehavior();
            }
        }


        IDisposable _filteringBehavior;

        private IEnumerable<T> _inputCollection;
        [TileDataPoint(Direction=TileDataPointDirection.Source)]
        public IEnumerable<T> InputCollection
        {
            get => _inputCollection;

            set
            {
                if (ReferenceEquals(InputCollection, value))
                    return;

                _inputCollection = value;

                SetBehavior();
            }
        }

        private void SetBehavior()
        {
            _filteringBehavior?.Dispose();

            OutputCollection.RemoveAllTyped();

            _filteringBehavior = _inputCollection.AddBehavior(OnItemAdded, OnItemRemoved);
        }

        [TileDataPoint(Direction=TileDataPointDirection.Target)]
        public ObservableCollection<T> OutputCollection { get; } = new ObservableCollection<T>();

        private void OnItemAdded(T item)
        {
            if (_filter?.Invoke(item) == true)
            {
                OutputCollection.Add(item);
            }
        }

        private void OnItemRemoved(T item)
        {
            if (_filter?.Invoke(item) == true)
            {
                OutputCollection.Remove(item);
            }
        }

        public FilterCollectionBehavior()
        {
        }
    }
}
