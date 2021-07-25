using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NP.Utilities;

namespace NP.Concepts.Behaviors
{
    public class DeepMultiSelectionWithSearchVm<T> :
        MultiSelectionWithSearchBase<T>
        where T : ISelectableItem<T>, ISearchable
    {

        protected override void OnItemSelectionChanged(T item)
        {
            if (item.IsSelected)
            {
                _selectedItems.AddIfNotThere<T>(item);
            }
            else
            {
                _selectedItems.Remove(item);
            }
        }

        ObservableCollection<T> _selectedItems = new ObservableCollection<T>();
        public override IEnumerable<T> SelectedItems => _selectedItems;
    }
}
