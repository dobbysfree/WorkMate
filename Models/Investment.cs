using System;
using System.ComponentModel;

namespace WorkMate.Models
{
    internal class Investment : INotifyPropertyChanged
    {
        public int InvestNo { get; set; }

        string _WBSCode;
        public string WBSCode
        {
            get { return _WBSCode; }
            set { _WBSCode = value; OnPropertyChanged("WBSCode"); }
        }

        string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged("Title"); }
        }

        string _Item;
        public string Item
        {
            get { return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }

        string _Budget;
        public string Budget
        {
            get { return _Budget; }
            set { _Budget = value; OnPropertyChanged("Budget"); }
        }

        string _ExecutionAmount;
        public string ExecutionAmount
        {
            get { return _ExecutionAmount; }
            set { _ExecutionAmount = value; OnPropertyChanged("ExecutionAmount"); }
        }

        string _Margin;
        public string Margin
        {
            get { return _Margin; }
            set { _Margin = value; OnPropertyChanged("Margin"); }
        }

        string _ProcessLine;
        public string ProcessLine
        {
            get { return _ProcessLine; }
            set { _ProcessLine = value; OnPropertyChanged("ProcessLine"); }
        }

        string _PONo;
        public string PONo
        {
            get { return _PONo; }
            set { _PONo = value; OnPropertyChanged("PONo"); }
        }

        short _Status;
        public short Status
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged("Status"); }
        }

        DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; OnPropertyChanged("UpdateDate"); }
        }

        string _Note;
        public string Note
        {
            get { return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }

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