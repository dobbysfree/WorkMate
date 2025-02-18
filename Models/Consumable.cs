using System.ComponentModel;

namespace WorkMate.Models
{
    internal class Consumable : INotifyPropertyChanged
    {
        public int Idx { get; set; }
        public string Process { get; set; }
        public int TaskType { get; set; }
        public string OEM { get; set; }
        public string Maker { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string IsImport { get; set; }
        public string Contact { get; set; }

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