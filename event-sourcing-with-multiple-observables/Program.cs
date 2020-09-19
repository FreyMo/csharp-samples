using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace event_sourcing_with_multiple_observables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var c1 = new ComponentWithStatus(Status.NotReady);
            var c2 = new AnotherComponentWithStatus(Status.NotReady);
            var c3 = new AgainAnotherComponentWithStatus(Status.NotReady);

            var allStatusComponents = new List<IStatus> { c1, c2, c3 };


            var statusObserver = new StatusObserver(allStatusComponents);

            Thread.Sleep(1000);

            c1.SetStatus(Status.Ready);
            c2.SetStatus(Status.Ready);
            c3.SetStatus(Status.Ready);

            Thread.Sleep(1000);

            c1.SetStatus(Status.Running);

            Thread.Sleep(1000);

            c2.SetStatus(Status.Error);

            Thread.Sleep(1000);

            c2.SetStatus(Status.Ready);
            c1.SetStatus(Status.Ready);
            c3.SetStatus(Status.Ready);

            Console.ReadKey();
        }
    }
}
