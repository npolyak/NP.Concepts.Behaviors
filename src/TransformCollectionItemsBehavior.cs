using NP.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NP.Concepts.Behaviors
{
    public class TransformCollectionItemsBehavior<TInput, TOutput>
    {
        public Func<TInput, TOutput> Transformer { get; }

        public Func<TInput, TOutput, bool> Matcher { get; }

        private IEnumerable<TInput> _inputCollection;

        private IDisposable _transformBehavior;

        [TileDataPoint(Direction=TileDataPointDirection.Source)]
        public IEnumerable<TInput> InputCollection
        {
            get => _inputCollection;

            set
            {
                if (ReferenceEquals(InputCollection, value))
                    return;

                _inputCollection = value;

                _transformBehavior?.Dispose();

                _transformBehavior = _inputCollection?.AddBehavior(OnItemAdded, OnItemRemoved);
            }
        }

        private void OnItemAdded(TInput inputItem)
        {
            TOutput outputItem = Transformer(inputItem);

            OutputCollection.Add(outputItem);
        }

        private void OnItemRemoved(TInput inputItem)
        {
            TOutput outputItemToRemove =
                OutputCollection.FirstOrDefault(outputItem => Matcher(inputItem, outputItem));

            OutputCollection.Remove(outputItemToRemove);
        }

        [TileDataPoint(Direction=TileDataPointDirection.Target)]
        public ObservableCollection<TOutput> OutputCollection { get; } = 
            new ObservableCollection<TOutput>();
        
        public TransformCollectionItemsBehavior
        (
            Func<TInput, TOutput> transformer,
            Func<TInput, TOutput, bool> matcher)
        {
            Transformer = transformer;  
            Matcher = matcher;
        }
    }
}
