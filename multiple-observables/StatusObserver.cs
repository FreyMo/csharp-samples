using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace multiple_observables
{
    public class StatusObserver
    {
        public StatusObserver(IEnumerable<IComponent> components)
        {
            var statusObservables = components.Select(x => x.GetStatusObservable()).ToList();

            // CombineLatest is used to get an observable that orchestrates multiple other observables.
            // It fires when any of the underlying observables fire (and the predicate is satisfied as well).

            // DistinctUntilChanged filters out observed values that are equal to their predecessors.

            Observable.CombineLatest(statusObservables,
                (lastStates) => lastStates.Any(status => status == Status.Error))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusError(hasState));
                
            Observable.CombineLatest(statusObservables,
                (lastStates) => lastStates.All(status => status == Status.Ready))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusReady(hasState));

            Observable.CombineLatest(statusObservables,
                (lastStates) => lastStates.Any(status => status == Status.NotReady))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusNotReady(hasState));

            Observable.CombineLatest(statusObservables,
                (lastStates) => lastStates.Any(status => status == Status.Running))
                .DistinctUntilChanged()
                .Subscribe(hasState => StatusRunning(hasState));
        }

        private void StatusReady(bool status)
        {
            if (status) Console.WriteLine("StatusReady: GREEN Light ON");
            else Console.WriteLine("StatusReady vanished: GREEN Light OFF");
        }

        private void StatusRunning(bool status)
        {
            if (status) Console.WriteLine("StatusRunning: GREEN Light blinking ON");
            else Console.WriteLine("StatusRunning vanished: GREEN Light blinking OFF");
        }

        private void StatusError(bool status)
        {
            if (status) Console.WriteLine("StatusError: RED Light ON");
            else Console.WriteLine("StatusError vanished: RED Light OFF");
        }

        private void StatusNotReady(bool status)
        {
            if (status) Console.WriteLine("StatusNotReady: RED Light blinking ON");
            else Console.WriteLine("StatusNotReady vanished: RED Light blinking OFF");
        }
    }
}