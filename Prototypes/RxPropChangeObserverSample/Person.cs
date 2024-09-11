using NP.Utilities;
using System.ComponentModel;

namespace RxPropChangeObserverSample
{
    public class Person : VMBase
    {
        #region Name Property
        private string? _name = null;
        public string? Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name == value)
                {
                    return;
                }

                this._name = value;
                this.OnPropertyChanged(nameof(Name));
            }
        }
        #endregion Name Property

        public static void Test(object? o, PropertyChangedEventArgs e)
        {

        }
    }
}
