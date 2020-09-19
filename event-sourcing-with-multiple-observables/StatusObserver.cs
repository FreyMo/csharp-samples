using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace event_sourcing_with_multiple_observables
{
    class StatusObserver
    {
        private List<IObservable<Status>> _statusComponents;

        public StatusObserver(IEnumerable<IStatus> statusComponents)
        {
            _statusComponents = new List<IObservable<Status>>();

            foreach (var statusComponent in statusComponents)
            {
                _statusComponents.Add(statusComponent.GetCurrentStatus());
            }

            Observable.CombineLatest(_statusComponents,
                (allLastStates) => allLastStates.Any(status => status == Status.Error))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusError(hasState));
            Observable.CombineLatest(_statusComponents,
                (allLastStates) => allLastStates.All(status => status == Status.Ready))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusReady(hasState));
            Observable.CombineLatest(_statusComponents,
                (allLastStates) => allLastStates.Any(status => status == Status.NotReady))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusNotReady(hasState));
            Observable.CombineLatest(_statusComponents,
                (allLastStates) => allLastStates.Any(status => status == Status.Running))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusRunning(hasState));

            
            //var lastStatus = Observable.CombineLatest(_statusComponents, (allLastStates) =>
            //    allLastStates switch
            //    {
            //        _ when allLastStates.All(s => s == Status.Ready) => Status.Ready,
            //        _ when allLastStates.Any(s => s == Status.Running) => Status.Running,
            //        _ when allLastStates.Any(s => s == Status.NotReady) => Status.NotReady,
            //        _ when allLastStates.Any(s => s == Status.Error) => Status.Error,
            //    }
            //);

            //lastStatus.Subscribe(s => StatusReady(s));
        }
        private void StatusReady(bool status)
        {
            if (status)
                Console.WriteLine("StatusReady: GREEN Light ON");
            else
                Console.WriteLine("StatusReady vanished: GREEN Light OFF");
        }

        private void StatusRunning(bool status)
        {
            if (status)
                Console.WriteLine("StatusRunning: GREEN Light blinking ON");
            else
                Console.WriteLine("StatusRunning vanished: GREEN Light blinking OFF");
        }

        private void StatusError(bool status)
        {
            if (status)
                Console.WriteLine("StatusError: RED Light ON");
            else
                Console.WriteLine("StatusError vanished: RED Light OFF");
        }

        private void StatusNotReady(bool status)
        {
            if (status)
                Console.WriteLine("StatusNotReady: RED Light blinking ON");
            else
                Console.WriteLine("StatusNotReady vanished: RED Light blinking OFF");
        }
    }
}