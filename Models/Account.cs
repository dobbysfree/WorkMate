using WorkMate.Views;
using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    public class Account : INotifyPropertyChanged
    {
        public ushort EmployeeNo { get; set; }
        public bool IsActive { get; set; }
        public ushort AreaCode { get; set; }
        public string Area { get => MainView.DicKeyValues[AreaCode].iValue; }
        public ushort TeamCode { get; set; }
        public string Team { get => MainView.DicKeyValues[TeamCode].iValue; }
        public string Role {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserID { get; set; }
        public string PW { get; set; }
        public DateTime JoinDate { get; set; }
        public string LeaveDate { get; set; }
        public ushort Level { get; set; }

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