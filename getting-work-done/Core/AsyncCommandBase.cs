using System;
using System.Threading.Tasks;

namespace getting_work_done
{
    public abstract class AsyncCommandBase : Bindable, IAsyncCommand
	{
		public event EventHandler CanExecuteChanged;

		public bool IsRunning
		{
			get => Get<bool>();
			private set => Set(value);
		}

		public async void Execute(object parameter)
		{
			IsRunning = true;
			RaiseCanExecuteChanged();

			await ExecuteAsync(parameter);

			IsRunning = false;
			RaiseCanExecuteChanged();
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public abstract Task ExecuteAsync(object parameter);

		public virtual bool CanExecute(object parameter)
		{
			return !IsRunning;
		}
	}
}
