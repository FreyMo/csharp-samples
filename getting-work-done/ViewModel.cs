using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace getting_work_done
{
    class ViewModel
    {
        public ViewModel()
        {
            StartCommand = new AsyncCommand(async () =>
            {
                var results = WorkItems.Select(item => item.DoWorkAsync());

                await Task.WhenAll(results);

                Console.WriteLine();
            });
        }

        public IEnumerable<WorkItem<int>> WorkItems { get; } = new WorkItem<int>[]
        {
                new WorkItem<int>(() => 1),
                new WorkItem<int>(() => 2),
                new WorkItem<int>(() => 3),
                new WorkItem<int>(() => 4)
        };

        public ICommand StartCommand { get; }
    }
}
