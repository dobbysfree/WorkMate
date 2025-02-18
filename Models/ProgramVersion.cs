using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    internal class ProgramVersion : INotifyPropertyChanged
    {
        public int Idx {  get; set; }
        public string Process {  get; set; }
        public string SideType { get; set; }
        public string Unit { get; set; }
        public string PC { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string DevCompany { get; set; }
        public string Manager {  get; set; }
        public string Site { get; set; }
        public int Task_Type { get; set; }
        public string Note { get; set; }
        public DateTime LatestDate { get; set; }

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