using System;

namespace NP.Concepts.Behaviors
{
    public interface IValueGetter<TProp>
    {
        TProp GetValue();

        IObservable<TProp> ValueObservable { get; } 
    }
}
