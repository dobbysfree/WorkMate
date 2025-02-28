using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WorkMate.Views
{
    /// <summary>
    /// IssueView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IssueTicketView : UserControl, INotifyPropertyChanged
    {
        public static ObservableCollection<IssueTicket> IssueTickets { get; set; } = new ObservableCollection<IssueTicket>();
        public static event Action ListCountChanged;

        public IssueTicketView()
        {
            InitializeComponent();

            cbxProcess.ItemsSource  = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Process_Code);
            cbxStatus.ItemsSource   = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Issue_Status);
            cbxTask.ItemsSource     = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Task_Type);

            ParsingDBData();
            DataContext = this;
            ListCountChanged?.Invoke();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData();
        }

        private void ShowIssueComment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgv.SelectedValue != null) new IssueCommentView((IssueTicket)dgv.SelectedValue).ShowDialog();
        }

        void ParsingDBData()
        {
            IssueTickets.Clear();

            try
            {

                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM tb_issue_ticket");

                var vaProcess   = (ushort)cbxProcess.SelectedValue;
                var vaStatus    = (ushort)cbxStatus.SelectedValue;
                var vaTask      = (ushort)cbxTask.SelectedValue;

                if (vaProcess != 0) query.Append($" WHERE Process_Code='{vaProcess}'");
                if (vaStatus != 0)
                {
                    query.Append(!query.ToString().Contains("WHERE") ? " WHERE " : " AND ");
                    query.Append($"Issue_Status='{vaStatus}'");
                }
                if (vaTask != 0)
                {
                    query.Append(!query.ToString().Contains("WHERE") ? " WHERE " : " AND ");
                    query.Append($"Task_Type='{vaTask}'");
                }

                var dt = DB.SelectSingle(query.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;

                    IssueTickets.Add(new IssueTicket() {
                        No    = (int)row[0],
                        ProcessCode = (ushort)row[1],
                        SideType    = (ushort)row[2],
                        Unit        = (string)row[3],
                        IssueStatus = (ushort)row[4],
                        TaskType    = (ushort)row[5],
                        Title       = (string)row[6],
                        CreateDate  = (DateTime)row[7],
                        Deadline    = (string)row[8],
                        FinishDate  = (string)row[9]
                    });
                }

                dgv.ItemsSource = IssueTickets;
                
                //MixCnt = IssueTickets.Count(x => x.ProcessCode == 40);
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void AddClickEvent(object sender, RoutedEventArgs e)
        {
            IssueTicket ticket = new IssueTicket();
            new SubIssueTicketView(ticket).ShowDialog();

            if (ticket.No != 0) IssueTickets.Add(ticket);

            ListCountChanged?.Invoke();
        }
    }
}