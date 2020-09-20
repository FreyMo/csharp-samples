using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace multiple_observables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program demonstrates a practical use case of multiple observables being orchestrated.");
            Console.WriteLine();

            var c1 = new Component(Status.NotReady);
            var c2 = new Component(Status.NotReady);
            var c3 = new Component(Status.NotReady);

            var statusObserver = new StatusObserver(new[] { c1, c2, c3 });

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
        }
    }
}
