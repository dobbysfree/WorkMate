using WorkMate.Views;
using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    public class IssueTicket : INotifyPropertyChanged, IEditableObject
    {
        private IssueTicket backupCopy;
        private bool inEdit;

        public int No { get; set; }

        ushort _ProcessCode;        
        public string Process { get => MainView.DicKeyValues[ProcessCode].iValue; }
        public ushort ProcessCode
        {
            get => _ProcessCode;
            set
            {
                if (_ProcessCode != value)
                {
                    _ProcessCode = value;
                    OnPropertyChanged(nameof(ProcessCode));
                }
            }
        }


        ushort _SideType;
        public string Side { get => MainView.DicKeyValues[SideType].iValue; }
        public ushort SideType
        {
            get => _SideType;
            set
            {
                if (_SideType != value)
                {
                    _SideType = value;
                    OnPropertyChanged(nameof(SideType));
                }
            }
        }

        string _Unit;
        public string Unit
        {
            get => _Unit;
            set
            {
                if (_Unit != value)
                {
                    _Unit = value;
                    OnPropertyChanged(nameof(Unit));
                }
            }
        }

        ushort _IssueStatus;
        public string Status { get => MainView.DicKeyValues[IssueStatus].iValue; }
        public ushort IssueStatus
        {
            get => _IssueStatus;
            set
            {
                if (_IssueStatus != value)
                {
                    _IssueStatus = value;
                    OnPropertyChanged(nameof(IssueStatus));
                }
            }
        }

        ushort _TaskType;
        public string Task { get => MainView.DicKeyValues[TaskType].iValue; }
        public ushort TaskType
        {
            get => _TaskType;
            set
            {
                if (_TaskType != value)
                {
                    _TaskType = value;
                    OnPropertyChanged(nameof(TaskType));
                }
            }
        }

        string _Title;
        public string Title
        {
            get => _Title;
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        DateTime _CreateDate;
        public DateTime CreateDate
        {
            get => _CreateDate;
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    OnPropertyChanged(nameof(CreateDate));
                }
            }
        }

        string _Deadline;
        public string Deadline
        {
            get => _Deadline;
            set
            {
                if (_Deadline != value)
                {
                    _Deadline = value;
                    OnPropertyChanged(nameof(Deadline));
                }
            }
        }

        string _FinishDate;
        public string FinishDate
        {
            get => _FinishDate;
            set
            {
                if (_FinishDate != value)
                {
                    _FinishDate = value;
                    OnPropertyChanged(nameof(FinishDate));
                }
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        // IEditableObject
        public void BeginEdit()
        {
            if (!inEdit)
            {
                backupCopy = new IssueTicket { 
                    No    = this.No,
                    ProcessCode = this.ProcessCode,
                    SideType    = this.SideType,
                    Unit        = this.Unit,
                    IssueStatus = this.IssueStatus,
                    TaskType    = this.TaskType,
                    Title       = this.Title,
                    CreateDate  = this.CreateDate,
                    Deadline    = this.Deadline,
                    FinishDate  = this.FinishDate
                };
                inEdit = true;
            }
        }

        public void CancelEdit()
        {
            if (inEdit)
            {
                this.No       = backupCopy.No;
                this.ProcessCode    = backupCopy.ProcessCode;
                this.SideType       = backupCopy.SideType;
                this.Unit           = backupCopy.Unit;
                this.IssueStatus    = backupCopy.IssueStatus;
                this.TaskType       = backupCopy.TaskType;
                this.Title          = backupCopy.Title;
                this.CreateDate     = backupCopy.CreateDate;
                this.Deadline       = backupCopy.Deadline;
                this.FinishDate     = backupCopy.FinishDate;
                inEdit              = false;
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