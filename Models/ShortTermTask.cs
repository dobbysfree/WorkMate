using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    public class ShortTermTask : INotifyPropertyChanged, IEditableObject
    {
        public ShortTermTask backupCopy;
        private bool inEdit;

        public int Idx { get; set; }

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

        string _Todo;
        public string Todo
        {
            get => _Todo;
            set
            {
                if (_Todo != value)
                {
                    _Todo = value;
                    OnPropertyChanged(nameof(Todo));
                }
            }
        }

        bool _IsDone;
        public bool IsDone
        {
            get => _IsDone;
            set
            {
                if (_IsDone != value)
                {
                    _IsDone = value;
                    OnPropertyChanged(nameof(IsDone));
                }
            }
        }

        string _UpdateDate;
        public string UpdateDate
        {
            get => _UpdateDate;
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    OnPropertyChanged(nameof(UpdateDate));
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
                backupCopy = new ShortTermTask { 
                    Idx         = this.Idx,
                    Todo        = this.Todo,
                    CreateDate  = this.CreateDate,
                    IsDone      = this.IsDone,
                    UpdateDate  = this.UpdateDate                    
                };
                inEdit = true;
            }
        }

        public void CancelEdit()
        {
            if (inEdit)
            {
                this.Idx        = backupCopy.Idx;
                this.Todo       = backupCopy.Todo;
                this.CreateDate = backupCopy.CreateDate;
                this.UpdateDate = backupCopy.UpdateDate;
                this.IsDone     = backupCopy.IsDone;
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