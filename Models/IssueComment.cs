using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    public class IssueComment : INotifyPropertyChanged, IEditableObject
    {
        public IssueComment backupCopy;
        private bool inEdit;

        public int Idx { get; set; }
        
        string _Comment;
        public string Comment
        {
            get => _Comment;
            set
            {
                if (_Comment != value)
                {
                    _Comment = value;
                    OnPropertyChanged(nameof(Comment));
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
                backupCopy = new IssueComment { 
                    Idx         = this.Idx,
                    Comment     = this.Comment,
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
                this.Comment    = backupCopy.Comment;
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