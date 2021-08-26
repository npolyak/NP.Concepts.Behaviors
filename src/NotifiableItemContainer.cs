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
using System.ComponentModel;


namespace NP.Concepts.Behaviors
{
    public class NotifiableItemContainer<TItem>
    {
        public event Action RecalculateResultEvent;

        #region Item Property
        private TItem _item;
        public TItem Item
        {
            get
            {
                return this._item;
            }
            set
            {
                if (this._item.ObjEquals(value))
                {
                    return;
                }

                if (_item is INotifyPropertyChanged oldNotifiable)
                {
                    oldNotifiable.PropertyChanged -= OnItemChanged;
                }

                this._item = value;

                RecalculateResultEvent?.Invoke();

                if (_item is INotifyPropertyChanged newNotifiable)
                {
                    newNotifiable.PropertyChanged += OnItemChanged;
                }
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            RecalculateResultEvent?.Invoke();
        }
        #endregion Item Property
    }
}
