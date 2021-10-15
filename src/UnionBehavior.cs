using NP.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NP.Concepts.Behaviors
{
    public class UnionBehavior<T> : VMBase
    {
        IEnumerable<T> _coll1;
        IEnumerable<T> _coll2;

        public ObservableCollection<T> Result { get; } = 
            new ObservableCollection<T>();

        IDisposable _coll1Behavior;
        IDisposable _coll2Behavior;

        public UnionBehavior(IEnumerable<T> coll1, IEnumerable<T> coll2)
        {
            _coll1 = coll1;
            _coll2 = coll2;

            _coll1Behavior = coll1.AddBehavior(OnItemAdded, OnItemRemoved);
            _coll2Behavior = coll2.AddBehavior(OnItemAdded, OnItemRemoved);

            Result.CollectionChanged += Result_CollectionChanged;
        }

        private void Result_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(null);
        }

        private void OnItemRemoved(T item)
        {
            Result.Remove(item);
        }

        private void OnItemAdded(T item)
        {
            Result.Add(item);
        }
    }
}
