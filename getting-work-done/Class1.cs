using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace getting_work_done
{
    public class AsyncCommand : AsyncCommandBase
	{
		private readonly Func<bool> _canExecuteFunc;
		private readonly Func<Task> _executionFunc;

		public AsyncCommand(Func<Task> executionFunc) : this(executionFunc, () => true)
		{
		}

		public AsyncCommand(Func<Task> executionFunc, Func<bool> canExecuteFunc)
		{
			_executionFunc = executionFunc;
			_canExecuteFunc = canExecuteFunc;
		}

		public override bool CanExecute(object parameter)
		{
			return _canExecuteFunc() && base.CanExecute(parameter);
		}

		public override Task ExecuteAsync(object parameter)
		{
			return _executionFunc();
		}
	}

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

	public interface IAsyncCommand : ICommandBase, INotifyPropertyChanged
	{
		bool IsRunning { get; }

		Task ExecuteAsync(object parameter);
	}


	public interface ICommandBase : ICommand
	{
		void RaiseCanExecuteChanged();
	}

}
