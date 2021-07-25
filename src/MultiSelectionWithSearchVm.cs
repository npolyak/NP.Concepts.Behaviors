using System.Collections.Generic;
using System.Linq;

namespace NP.Concepts.Behaviors
{
    public class ShallowMultiSelectionWithSearchVm<T> :
        MultiSelectionWithSearchBase<T>
        where T : ISelectableItem<T>, ISearchable
    {

        protected override void OnItemSelectionChanged(T item)
        {
            OnPropertyChanged(nameof(SelectedItems));
            OnPropertyChanged(nameof(SelectedItemsToDisplay));
        }

        public override IEnumerable<T> SelectedItems =>
            AllItems.Where(item => item.IsSelected).ToList();
    }
}
