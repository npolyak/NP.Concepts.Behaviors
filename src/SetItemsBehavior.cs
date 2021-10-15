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

using NP.Utilities;
using System;
using System.Collections.Generic;

namespace NP.Concepts.Behaviors
{
    public class SetItemsBehavior<TItem, TProp> : IDisposable
    {

        private TProp _val = default;
        public TProp Val
        {
            get => _val;

            set
            {
                if (_val.ObjEquals(value))
                {
                    return;
                }

                _behavior?.Dispose();
                _val = value;

                _behavior = _items?.AddBehavior(OnAdded/*, OnRemoved*/);
            }
        }

        #region Items Property
        private IEnumerable<TItem>? _items;
        public IEnumerable<TItem>? Items
        {
            get
            {
                return this._items;
            }
            set
            {
                if (this._items.ObjEquals(value))
                {
                    return;
                }

                _behavior?.Dispose();

                this._items = value;

                _behavior = _items?.AddBehavior(OnAdded/*, OnRemoved*/);
            }
        }
        #endregion Items Property

        //private void OnRemoved(TItem obj)
        //{
        //    _itemSetter.Invoke(obj, default(TProp)!);
        //}

        private void OnAdded(TItem item)
        {
            _itemSetter.Invoke(item, _val);
        }

        public void Dispose()
        {
            _behavior?.Dispose();
            _behavior = null;
        }


        Action<TItem, TProp> _itemSetter;
        IDisposable _behavior;

        public SetItemsBehavior(Action<TItem, TProp> itemSetter)
        {
            _itemSetter = itemSetter;
        }
    }
}
