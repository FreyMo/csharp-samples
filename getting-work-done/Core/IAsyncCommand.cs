using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace getting_work_done
{
    public interface IAsyncCommand : ICommand, INotifyPropertyChanged
	{
		void RaiseCanExecuteChanged();
		
		bool IsRunning { get; }

		Task ExecuteAsync(object parameter);
	}
}
