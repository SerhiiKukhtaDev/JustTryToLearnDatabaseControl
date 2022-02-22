using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace JustTryToLearnDatabaseEditor.Models.Base
{
    public abstract class NotifiedModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }

    public abstract class Model<TParent> : NotifiedModel
    {
        public abstract void SetParent(TParent parent);
    }
}