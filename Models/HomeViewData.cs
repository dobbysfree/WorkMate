using WorkMate.Views;
using System.ComponentModel;

namespace WorkMate.Models
{
    public class HomeViewData : INotifyPropertyChanged
    {        
        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}