using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace event_sourcing_with_multiple_observables
{
    enum Status
    {
        Error,
        NotReady,
        Ready,
        Running
    }

    interface IStatus
    {
        IObservable<Status> GetCurrentStatus();
    }

    class ComponentWithStatus : IStatus
    {
        private readonly BehaviorSubject<Status> _currentStatus;

        public ComponentWithStatus(Status initialStatus)
        {
            _currentStatus = new BehaviorSubject<Status>(initialStatus);
        }

        public IObservable<Status> GetCurrentStatus() => _currentStatus;

        public void  SetStatus(Status newStatus)
        {
            _currentStatus.OnNext(newStatus);
        }
    }

    class AnotherComponentWithStatus : IStatus
    {
        private readonly BehaviorSubject<Status> _currentStatus;

        public AnotherComponentWithStatus(Status initialStatus)
        {
            _currentStatus = new BehaviorSubject<Status>(initialStatus);
        }

        public IObservable<Status> GetCurrentStatus() => _currentStatus;

        public void SetStatus(Status newStatus)
        {
            _currentStatus.OnNext(newStatus);
        }
    }

    class AgainAnotherComponentWithStatus : IStatus
    {
        private readonly BehaviorSubject<Status> _currentStatus;

        public AgainAnotherComponentWithStatus(Status initialStatus)
        {
            _currentStatus = new BehaviorSubject<Status>(initialStatus);
        }

        public IObservable<Status> GetCurrentStatus() => _currentStatus;

        public void SetStatus(Status newStatus)
        {
            _currentStatus.OnNext(newStatus);
        }
    }
}
