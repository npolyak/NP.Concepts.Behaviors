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
