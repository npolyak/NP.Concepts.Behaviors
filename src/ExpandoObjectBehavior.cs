using NP.Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace NP.Concepts.Behaviors
{
    public class ExpandoObjectBehavior<T>
    {
        private IEnumerable<T> _coll;

        public ExpandoObject Result { get; } = new ExpandoObject();

        private IDictionary<string, object> Dict => Result;

        private readonly IDisposable _behavior;
        private readonly Func<T, string> _keyProducer;
        private readonly Func<T, IObservable<string>> _keyObservableGetter;
        private Dictionary<string, IDisposable> _keyChangeObservables = 
            new Dictionary<string, IDisposable>();

        public ExpandoObjectBehavior
        (
            IEnumerable<T> coll, 
            Func<T, string> keyProducer, 
            Func<T, IObservable<string>> keyObservableGetter)
        {
            _coll = coll;

            _keyProducer = keyProducer;
            _keyObservableGetter = keyObservableGetter;

            _behavior = coll.AddBehavior(OnItemAdded, OnItemRemoved);
        }

        private void OnKeyChanged(string newKey)
        {
            var oldEntry = Dict.FirstOrDefault(kvp => _keyProducer((T)kvp.Value).ObjEquals(newKey));

            if (oldEntry.Key == newKey)
            {
                return;
            }

            OnItemRemoved(oldEntry.Key);

            OnItemAdded((T)oldEntry.Value);
        }

        private void OnItemRemoved(string key)
        {
            Dict.Remove(key);

            if (_keyChangeObservables.TryGetValue(key, out IDisposable subscription))
            {
                subscription?.Dispose();

                _keyChangeObservables.Remove(key);
            }
        }

        private void OnItemRemoved(T item)
        {
            OnItemRemoved(_keyProducer(item));
        }

        private void OnItemAdded(T item)
        {
            string key = _keyProducer(item);
            Dict[key] = item;

            _keyChangeObservables[key] = _keyObservableGetter(item).Subscribe(OnKeyChanged);
        }
    }
}
