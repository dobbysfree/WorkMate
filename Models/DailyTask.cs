using WorkMate.Views;
using System;
using System.ComponentModel;
using System.Text;

namespace WorkMate.Models
{
    public class DailyTask : INotifyPropertyChanged, IEditableObject
    {
        public DailyTask backupCopy;
        private bool inEdit;

        DateTime _Date;
        public DateTime Date
        {
            get => _Date;
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        ushort _EmployeeNo;
        public ushort EmployeeNo
        {
            get => _EmployeeNo;
            set
            {
                if (_EmployeeNo != value)
                {
                    _EmployeeNo = value;
                    OnPropertyChanged(nameof(EmployeeNo));
                }
            }
        }
        public string UserID { get; set; }

        string _StartTime;
        public string StartTime
        {
            get => _StartTime;
            set
            {
                if (_StartTime != value)
                {
                    _StartTime = value;
                    OnPropertyChanged(nameof(StartTime));
                }
            }
        }

        string _EndTime;
        public string EndTime
        {
            get => _EndTime;
            set
            {
                if (_EndTime != value)
                {
                    _EndTime = value;
                    OnPropertyChanged(nameof(EndTime));
                }
            }
        }

        public string Area { get => MainView.DicKeyValues[AreaCode].iValue; }
        ushort _AreaCode;
        public ushort AreaCode
        {
            get => _AreaCode;
            set
            {
                if (_AreaCode != value)
                {
                    _AreaCode = value;
                    OnPropertyChanged(nameof(AreaCode));
                }
            }
        }

        string _Note;
        public string Note
        {
            get => _Note;
            set
            {
                if (_Note != value)
                {
                    _Note = value;
                    OnPropertyChanged(nameof(Note));
                }
            }
        }

        string _Info;
        public string Info
        {
            get => _Info;
            set
            {
                if (_Info != value)
                {
                    _Info = value;
                    OnPropertyChanged(nameof(Info));
                }
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

         // IEditableObject
        public void BeginEdit()
        {
            if (!inEdit)
            {
                backupCopy = new DailyTask { 
                    Date        = this.Date,
                    EmployeeNo  = this.EmployeeNo,
                    StartTime   = this.StartTime,
                    EndTime     = this.EndTime,
                    AreaCode    = this.AreaCode,
                    Note        = this.Note,
                    Info        = this.Info
                };
                inEdit = true;
            }
        }

        public void CancelEdit()
        {
            if (inEdit)
            {
                this.Date       = backupCopy.Date;
                this.EmployeeNo = backupCopy.EmployeeNo;
                this.StartTime  = backupCopy.StartTime;
                this.EndTime    = backupCopy.EndTime;
                this.AreaCode   = backupCopy.AreaCode;
                this.Note       = backupCopy.Note;
                this.Info       = backupCopy.Info;
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