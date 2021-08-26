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
