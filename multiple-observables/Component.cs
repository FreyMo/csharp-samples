using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace multiple_observables
{
    public class Component : IComponent
    {
        private readonly BehaviorSubject<Status> _currentStatus;

        public Component(Status initialStatus)
        {
            _currentStatus = new BehaviorSubject<Status>(initialStatus);
        }

        public IObservable<Status> GetStatusObservable() => _currentStatus.AsObservable();

        public void SetStatus(Status newStatus) => _currentStatus.OnNext(newStatus);
    }
}
