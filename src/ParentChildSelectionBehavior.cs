﻿// (c) Nick Polyak 2018 - http://awebpros.com/
// License: Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0.html)
//
// short overview of copyright rules:
// 1. you can use this framework in any commercial or non-commercial 
//    product as long as you retain this copyright message
// 2. Do not blame the author of this software if something goes wrong. 
// 
// Also, please, mention this software in any documentation for the 
// products that use it.
using NP.Utilities;
using System;
using System.Collections.ObjectModel;

namespace NP.Concepts.Behaviors
{
    public class ParentChildSelectionBehavior<TParent, TChild>
        where TParent : class, ISelectableItem<TParent>
        where TChild : class, ISelectableItem<TChild>
    {
        IDisposable _childrenBehaviorDisposable = null;

        TParent _parent;
        public TParent Parent
        {
            get => _parent;

            set
            {
                if (_parent.ObjEquals(value))
                    return;

                if (_parent != null)
                {
                    _parent.IsSelectedChanged -=
                        ParentChildSelectionBehavior_IsSelectedChanged;
                }

                _parent = value;

                if (_parent != null)
                {
                    _parent.IsSelectedChanged +=
                        ParentChildSelectionBehavior_IsSelectedChanged;
                }
            }
        }

        ObservableCollection<TChild> _children;
        public ObservableCollection<TChild> Children
        {
            private get => _children;
            set
            {
                if (ReferenceEquals(_children, value))
                    return;

                _children = value;

                _childrenBehaviorDisposable?.Dispose();
                _childrenBehaviorDisposable = _children.AddBehavior
                 (
                    child => child.IsSelectedChanged += Child_IsSelectedChanged,
                    child => child.IsSelectedChanged -= Child_IsSelectedChanged
                 );
            }
        }

        // unselect children if parent is unselected
        private void ParentChildSelectionBehavior_IsSelectedChanged(ISelectableItem<TParent> parent)
        {
            if (!parent.IsSelected)
            {
                foreach(TChild child in this.Children)
                {
                    if (child.IsSelected)
                    {
                        child.IsSelected = false;
                    }
                }
            }
        }

        // select the parent if its child is selected. 
        private void Child_IsSelectedChanged(ISelectableItem<TChild> child)
        {
            if ((child.IsSelected) && (this.Parent != null))
            {
                this.Parent.IsSelected = true;
            }
        }
    }
}
