using System;
using System.Threading.Tasks;

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
}
