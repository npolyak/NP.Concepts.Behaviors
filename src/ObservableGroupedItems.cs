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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NP.Concepts.Behaviors
{
    public class ObservableGroupedItems<T, TGroupKey> : ObservableCollection<T>
    {
        protected void OnPropertyChanged(string propName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propName));
        }

        public TGroupKey TheKey { get; }

        public ObservableGroupedItems(TGroupKey key)
        {
            TheKey = key;
        }

        public override bool Equals(object obj)
        {
            if (obj is ObservableGroupedItems<T, TGroupKey> groupedItems)
            {
                return TheKey.ObjEquals(groupedItems.TheKey);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return TheKey.GetHashCodeExtension();
        }

        public override string ToString() => TheKey.ToString();
    }
}
