using System.ComponentModel;
using System.Reactive.Linq;

namespace RxPropChangeObserverSample
{
    public class PropChangedInfo<T, TProp>
    {
        public T Obj { get; }
        public string? PropertyName { get; }
        public TProp? PropValue { get; }


        public PropChangedInfo()
        {

        }

        public PropChangedInfo(T obj, string? propName, TProp? propValue)
        {
            Obj = obj;
            PropertyName = propName;
            PropValue = propValue;
        }

    }

    public static class RxHelper
    {
        public static TProp? GetProp<T, TProp>(this T obj, Func<T, TProp>? propGetter)
        {
            if (propGetter == null)
                return default(TProp);

            return propGetter.Invoke(obj);
        }

        public static IObservable<PropChangedInfo<T, TProp>>
            PropChangedToObservable<T, TProp>
        (
            this T objToObserve,
            string? propName,
            Func<T, TProp>? propGetter
        )
        {
            PropChangedInfo<T, TProp> CreatePropChangedInfo()
            {
                return new PropChangedInfo<T, TProp>(objToObserve, propName, objToObserve.GetProp(propGetter));
            }

            var result = Observable.Empty<PropChangedInfo<T, TProp>>().StartWith(CreatePropChangedInfo());

            if (objToObserve is INotifyPropertyChanged notifiable && propName != null)
            {
                var propChangeObservable = Observable.FromEvent<PropertyChangedEventHandler, PropChangedInfo<T, TProp>>
                (
                    handler => (o, e) => handler(CreatePropChangedInfo()),
                    h => notifiable.PropertyChanged += h,
                    h => notifiable.PropertyChanged -= h
                );

                result = result.Concat(propChangeObservable);
            }

            return result;
        }
    }
}
