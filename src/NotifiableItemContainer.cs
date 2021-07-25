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
