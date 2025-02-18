using System.ComponentModel;

namespace WorkMate.Models
{
    internal class Contact : INotifyPropertyChanged
    {
        public int Idx {  get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Memo { get; set; }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}