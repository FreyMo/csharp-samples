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

            statusObservables.CombineLatest(
                (lastStates) =>
                    lastStates switch
                    {
                        _ when lastStates.All(s => s == Status.Ready) => LampStatus.On,
                        _ when lastStates.Any(s => s == Status.Running) => LampStatus.Blinking,
                        _ => LampStatus.Off
                    }
                ).DistinctUntilChanged()
                .Subscribe(lampState => GreenStatusLamp(lampState));

            statusObservables.CombineLatest(
                (lastStates) =>
                    lastStates switch
                    {
                        _ when lastStates.Any(s => s == Status.Error) => LampStatus.On,
                        _ when lastStates.Any(s => s == Status.NotReady) => LampStatus.Blinking,
                        _ => LampStatus.Off
                    }
                ).DistinctUntilChanged()
                .Subscribe(lampState => RedStatusLamp(lampState));
        }

        private void GreenStatusLamp(LampStatus lampStatus)
        {
            switch (lampStatus)
            {
                case LampStatus.On:
                    Console.WriteLine("StatusReady: GREEN Light ON");
                    break;
                case LampStatus.Blinking:
                    Console.WriteLine("StatusRunning: GREEN Light blinking ON");
                    break;
                case LampStatus.Off:
                    Console.WriteLine("No status for green light active: GREEN Light OFF");
                    break;
            }
        }

        private void RedStatusLamp(LampStatus lampStatus)
        {
            switch (lampStatus)
            {
                case LampStatus.On:
                    Console.WriteLine("StatusError: RED Light ON");
                    break;
                case LampStatus.Blinking:
                    Console.WriteLine("StatusNotReady: RED Light blinking ON");
                    break;
                case LampStatus.Off:
                    Console.WriteLine("No status for red light active: RED Light OFF");
                    break;
            }
        }
    }
}