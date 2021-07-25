using System.Collections;

namespace NP.Concepts.Behaviors
{
    public interface IMultiSelectionWithSearchVm
    {
        string SearchStr { get; }

        IEnumerable AllItemsToDisplay { get; }

        IEnumerable SelectedItemsToDisplay { get; }
    }
}
