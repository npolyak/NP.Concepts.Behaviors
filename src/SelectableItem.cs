using NP.Utilities;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NP.Concepts.Behaviors
{
    public interface ISelectableItem<T>
        where T : ISelectableItem<T>
    {
        bool IsSelected { get; set; }

        event Action<T> IsSelectedChanged;

        void Select();
    }

    public static class SelectableItemExtensions
    {
        public static void SelectItem<T>(this ISelectableItem<T> selectableItem)
            where T : ISelectableItem<T>
        {
            selectableItem.IsSelected = true;
        }

        public static void ToggleSelection<T>(this ISelectableItem<T> selectableItem) 
            where T : ISelectableItem<T>
        {
            selectableItem.IsSelected = !selectableItem.IsSelected;
        }
    }


    public class SelectableItem<TSelectableItem> : 
        VMBase, 
        ISelectableItem<TSelectableItem>, 
        INotifyPropertyChanged
        where TSelectableItem : SelectableItem<TSelectableItem>
    {
        bool _isSelected = false;

        [XmlIgnore]
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;

                IsSelectedChanged?.Invoke((TSelectableItem) this);

                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event Action<TSelectableItem> IsSelectedChanged;

        public void Select()
        {
            this.SelectItem();
        }
    }

    public interface ISelectableItemWrapper<T>
        where T : SelectableItem<T>
    {
        SelectableItem<T> TheSelectableItem { get; }
    }
}
