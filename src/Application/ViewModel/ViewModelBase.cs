using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Application.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool ThrowOnInvalidPropertyName => false;
        public virtual string DisplayName { get; set; }

        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] != null)
            {
                return;
            }

            var msg = $"Invalid property name: {propertyName}";

            if (ThrowOnInvalidPropertyName)
            {
                throw new Exception(msg);
            }

            Debug.Fail(msg);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
        }
    }
}