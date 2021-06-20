using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FileAndDirectoryBrowserWebApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetPropertyValue<TPropertyType>(ref TPropertyType originalValue,
            TPropertyType newValue,
            Action<TPropertyType>? changedAction = null,
            [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<TPropertyType>.Default.Equals(originalValue, newValue))
            {
                originalValue = newValue;

                changedAction?.Invoke(newValue);

                if (propertyName != null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
