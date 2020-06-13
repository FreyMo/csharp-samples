using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace getting_work_done
{
    // Props to Rene Peuser
    // https://github.com/RenePeuser/OptimizedBindingBase
    public abstract class Bindable : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties;

        public event PropertyChangedEventHandler PropertyChanged;

        protected Bindable()
        {
            _properties = new Dictionary<string, object>();
        }

        public T Get<T>([CallerMemberName] string propertyName = "")
        {
            if (!_properties.ContainsKey(propertyName))
            {
                return default;
            }

            return (T)_properties[propertyName];
        }

        public bool Set<T>(T newValue, [CallerMemberName] string propertyName = "")
        {
            var currentValue = Get<T>(propertyName);
            if (newValue.Equals(currentValue))
            {
                return false;
            }

            _properties[propertyName] = newValue;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
