using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace multiple_observables
{
    public class Component : IComponent
    {
        private readonly BehaviorSubject<Status> _currentStatus;

        public Component(Status initialStatus)
        {
            _currentStatus = new BehaviorSubject<Status>(initialStatus);
        }

        public IObservable<Status> GetStatusObservable() => _currentStatus;

        public void SetStatus(Status newStatus)
        {
            _currentStatus.OnNext(newStatus);
        }
    }
}
