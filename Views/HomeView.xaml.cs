using WorkMate.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace WorkMate.Views
{
    /// <summary>
    /// HomeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HomeView : UserControl, INotifyPropertyChanged
    {
        int _TaskCount;
        public int TaskCount
        {
            get => _TaskCount;
            set
            {
                if (_TaskCount != value)
                {
                    _TaskCount = value;
                    OnPropertyChanged(nameof(TaskCount));
                }
            }
        }

        int _IssueCount;
        public int IssueCount
        {
            get => _IssueCount;
            set
            {
                if (_IssueCount != value)
                {
                    _IssueCount = value;
                    OnPropertyChanged(nameof(IssueCount));
                }
            }
        }

        public HomeView()
        {
            InitializeComponent();
            
            TaskCount   = ShortTermTaskView.TodoList.Count;
            //IssueCount  = IssueTicketView.IssueTickets.Count;

            ShortTermTaskView.ListCountChanged += OnTaskCountChanged;
            IssueTicketView.ListCountChanged += OnIssueCountChanged;

            DataContext = this;
        }

        void OnTaskCountChanged()
        {
            TaskCount = ShortTermTaskView.TodoList.Count;
        }

        void OnIssueCountChanged()
        {
            IssueCount = IssueTicketView.IssueTickets.Count;
        }


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}