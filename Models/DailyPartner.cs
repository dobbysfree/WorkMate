using WorkMate.Views;
using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    public class DailyPartner : INotifyPropertyChanged, IEditableObject
    {
        private DailyPartner backupCopy;
        private bool inEdit;

        public int Idx { get; set; }
        public DateTime Date { get; set; }

        ushort _Company;
        public string CompanyName { get => MainView.DicKeyValues[Company].iValue; }
        

        public ushort Company
        {
            get => _Company;
            set
            {
                if (_Company != value)
                {
                    _Company = value;
                    OnPropertyChanged(nameof(Company));
                }
            }
        }       

        string _Worker;
        public string Worker
        {
            get => _Worker;
            set
            {
                if (_Worker != value)
                {
                    _Worker = value;
                    OnPropertyChanged(nameof(Worker));
                }
            }
        }

        string _TodayTask;
        public string TodayTask
        {
            get => _TodayTask;
            set
            {
                if (_TodayTask != value)
                {
                    _TodayTask = value;
                    OnPropertyChanged(nameof(TodayTask));
                }
            }
        }

        string _NextdayTask;
        public string NextdayTask
        {
            get => _NextdayTask;
            set
            {
                if (_NextdayTask != value)
                {
                    _NextdayTask = value;
                    OnPropertyChanged(nameof(NextdayTask));
                }
            }
        }

        string _Memo;
        public string Memo
        {
            get => _Memo;
            set
            {
                if (_Memo != value)
                {
                    _Memo = value;
                    OnPropertyChanged(nameof(Memo));
                }
            }
        }

        // INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        // IEditableObject
        public void BeginEdit()
        {
            if (!inEdit)
            {
                backupCopy = new DailyPartner { 
                    Idx         = this.Idx,
                    Date        = this.Date,
                    Company     = this.Company,
                    Worker      = this.Worker,
                    TodayTask   = this.TodayTask,
                    NextdayTask = this.NextdayTask,
                    Memo        = this.Memo
                };
                inEdit = true;
            }
        }

        public void CancelEdit()
        {
            if (inEdit)
            {
                this.Idx        = backupCopy.Idx;
                this.Date       = backupCopy.Date;
                this.Company    = backupCopy.Company;
                this.Worker     = backupCopy.Worker;
                this.TodayTask  = backupCopy.TodayTask;
                this.NextdayTask = backupCopy.NextdayTask;
                this.Memo       = backupCopy.Memo;
                inEdit          = false;
            }
        }

        public void EndEdit()
        {
            if (inEdit)
            {
                backupCopy  = null;
                inEdit      = false;
            }
        }
    }
}