using System.Collections.Generic;
using System.Linq;

namespace NP.Concepts.Behaviors
{
    public class SingleSelectionFirstByDefaultBehavior<TSelectable> : 
        SingleSelectionBehavior<TSelectable>
        where TSelectable : class, ISelectableItem<TSelectable>
    {
        public SingleSelectionFirstByDefaultBehavior()
        {

        }

        public SingleSelectionFirstByDefaultBehavior(IEnumerable<TSelectable> collection) : base(collection)
        {

        }

        public void SelectFirst()
        {
            this.TheCollection?.FirstOrDefault()?.Select();
        }

        protected override void OnCollectionSet()
        {
            base.OnCollectionSet();

            SelectFirst();
        }

        protected override void BeforeItemAdded(TSelectable item)
        {
            base.BeforeItemAdded(item);

            if (TheSelectedItem == null)
            {
                item.Select();
            }
        }

        protected override void DoOnSelectedItemRemoved()
        {
            base.DoOnSelectedItemRemoved();

            SelectFirst();
        }
    }
}
