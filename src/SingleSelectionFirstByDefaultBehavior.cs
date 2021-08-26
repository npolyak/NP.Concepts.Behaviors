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
