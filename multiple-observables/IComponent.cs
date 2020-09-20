using System;

namespace multiple_observables
{
    public interface IComponent
    {
        IObservable<Status> GetStatusObservable();
    }
}
