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
using System.Collections.ObjectModel;

namespace NP.Concepts.Behaviors
{
    public class FlattenReactiveTreeBehavior<TNode> : MultiCollectionsChangeBehavior<TNode>
    {
        private ObservableCollection<TNode> _observableCollection = new ObservableCollection<TNode>();
        public ReadOnlyObservableCollection<TNode> Result { get; }

        private Func<TNode, IEnumerable<TNode>> _toChildren;

        private TNode _root;

        public FlattenReactiveTreeBehavior(TNode root, Func<TNode, IEnumerable<TNode>> toChildren)
        {
            _toChildren = toChildren;

            _root = root;

            Result = new ReadOnlyObservableCollection<TNode>(_observableCollection);

            AttachCollections();
        }

        public override void AttachCollections()
        {
            base.AttachCollections();
            OnCollectionItemAdded(_root);
        }

        public override void DetachCollections()
        {
            OnCollectionItemRemoved(_root);
            base.DetachCollections();
        }

        protected override void OnCollectionItemAdded(TNode item)
        {
            _observableCollection.Add(item);

            IEnumerable<TNode> childCollection = _toChildren(item);

            if (childCollection != null)
            {
                Collections.Add(childCollection);
            }    
        }

        protected override void OnCollectionItemRemoved(TNode item)
        {
            IEnumerable<TNode> childCollection = _toChildren(item);

            if (childCollection != null)
            {
                Collections.Remove(childCollection);
            }

            _observableCollection.Remove(item);
        }
    }
}
