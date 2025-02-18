using WorkMate.Views;
using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    internal class UnitHistory : INotifyPropertyChanged, IEditableObject
    {
        private UnitHistory backupCopy;
        private bool inEdit;

        public int Idx { get; set; }
        //public int UnitNo { get; set; }

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

        int _TicketNo;
        public int TicketNo
        {
            get => _TicketNo;
            set
            {
                if (_TicketNo != value)
                {
                    _TicketNo = value;
                    OnPropertyChanged(nameof(TicketNo));
                }
            }
        }

        public DateTime CreateDT { get; set; }

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
                backupCopy = new UnitHistory { 
                    Idx         = this.Idx,
                    TaskType    = this.TaskType,
                    Note        = this.Note,
                    TicketNo    = this.TicketNo,
                    CreateDT    = this.CreateDT
                };
                inEdit = true;
            }
        }

        public void CancelEdit()
        {
            if (inEdit)
            {
                this.Idx        = backupCopy.Idx;
                this.TaskType   = backupCopy.TaskType;
                this.Note       = backupCopy.Note;
                this.TicketNo   = backupCopy.TicketNo;
                this.CreateDT   = backupCopy.CreateDT;
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