using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace getting_work_done
{
    public interface IWorkItem : INotifyPropertyChanged
    {
        int Progress { get; set; }
    }

    public class WorkItem<T> : Bindable, IWorkItem
    {
        private readonly Lazy<Task<T>> _lazyTask;
        private readonly Func<T> _func;

        public WorkItem(Func<T> func)
        {
            _lazyTask = new Lazy<Task<T>>(DoWorkInternalAsync);
            _func = func;
        }

        public Task<T> DoWorkAsync()
        {
            return _lazyTask.Value;
        }

        public int Progress
        {
            get => Get<int>();
            set => Set(value);
        }

        private async Task<T> DoWorkInternalAsync()
        {
            for (var i = 0; i < 101; i++)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                Progress = i;
            }

            return _func();
        }
    }
}
