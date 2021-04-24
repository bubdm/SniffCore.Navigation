using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SniffCore
{
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        protected void NotifyPropertyChanging([CallerMemberName] string property = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(property));
        }

        protected void NotifyPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected void NotifyAndSet<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
        {
            NotifyPropertyChanging(propertyName);
            backingField = newValue;
            NotifyPropertyChanged(propertyName);
        }

        protected void NotifyAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(backingField, newValue))
                NotifyAndSet(ref backingField, newValue, propertyName);
        }
    }
}