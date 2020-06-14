using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace getting_work_done
{
    class ViewModel : Bindable
    {
        public ViewModel()
        {
            Producer = new Producer();
            Consumer = new FastParallelConsumer();

            StartCommand = new AsyncCommand(async () =>
            {
                await Consumer.ConsumeWorkItemsAsync(Producer.ProduceWorkItemsAsync());
            });
        }

        public IProducer Producer
        {
            get => Get<IProducer>();
            set => Set(value);
        }
        
        public IConsumer Consumer
        {
            get => Get<IConsumer>();
            set => Set(value);
        }

        public ICommand StartCommand { get; }

        public ICommand ResetCommand { get; }
    }

    public interface IProducer
    {
        IAsyncEnumerable<WorkItem<int>> ProduceWorkItemsAsync();

        public ObservableCollection<WorkItem<int>> WorkItems { get; }
    }

    public class Producer : IProducer
    {
        private readonly object _lock = new object();

        public Producer()
        {
            // I know, this isn't pretty. Required because worker threads add to the WorkItems ObservableCollection.
            // Should not be required in a good design. This is just for demonstration purposes.
            BindingOperations.EnableCollectionSynchronization(WorkItems, _lock);
        }

        public async IAsyncEnumerable<WorkItem<int>> ProduceWorkItemsAsync()
        {
            foreach (var item in ProduceWorkItems())
            {
                await Task.Delay(10);

                yield return item;
            }
        }

        IEnumerable<WorkItem<int>> ProduceWorkItems()
        {
            for (var i = 0; i < 4; i++)
            {
                var workItem = new WorkItem<int>(() => 1);

                WorkItems.Add(workItem);
                
                yield return workItem;
            }
        }

        public ObservableCollection<WorkItem<int>> WorkItems { get; } = new ObservableCollection<WorkItem<int>>();
    }

    public interface IConsumer
    {
        Task ConsumeWorkItemsAsync(IAsyncEnumerable<WorkItem<int>> items);

        public ObservableCollection<WorkItem<int>> WorkItems { get; }
    }

    public class SequentialConsumer : IConsumer
    {
        public async Task ConsumeWorkItemsAsync(IAsyncEnumerable<WorkItem<int>> items)
        {
            await foreach (var item in items)
            {
                var result = await item.DoWorkAsync();

                var newWorkItem = new WorkItem<int>(() => result);

                WorkItems.Add(newWorkItem);

                await newWorkItem.DoWorkAsync();
            }
        }

        public ObservableCollection<WorkItem<int>> WorkItems { get; } = new ObservableCollection<WorkItem<int>>();
    }

    public class EnumerateFirstSequentialConsumer : IConsumer
    {
        public async Task ConsumeWorkItemsAsync(IAsyncEnumerable<WorkItem<int>> items)
        {
            var result = await items.SelectAwait(async item => await item.DoWorkAsync()).ToListAsync();

            foreach (var item in result)
            {
                var newWorkItem = new WorkItem<int>(() => item);

                WorkItems.Add(newWorkItem);

                await newWorkItem.DoWorkAsync();
            }
        }

        public ObservableCollection<WorkItem<int>> WorkItems { get; } = new ObservableCollection<WorkItem<int>>();
    }

    public class FastParallelConsumer : IConsumer
    {
        public async Task ConsumeWorkItemsAsync(IAsyncEnumerable<WorkItem<int>> items)
        {
            var parallelized = await items.ToListAsync();

            parallelized.AsParallel().Select(async item =>
            {
                var result = await item.DoWorkAsync();

                var newWorkItem = new WorkItem<int>(() => result);

                WorkItems.Add(newWorkItem);

                await newWorkItem.DoWorkAsync();
            }).ToList();
        }

        public ObservableCollection<WorkItem<int>> WorkItems { get; } = new ObservableCollection<WorkItem<int>>();
    }
}
