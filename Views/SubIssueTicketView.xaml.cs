using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace WorkMate.Views
{
    /// <summary>
    /// SubIssueTicketView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SubIssueTicketView : MetroWindow, INotifyPropertyChanged
    {
        public SubIssueTicketView(IssueTicket ticket)
        {
            InitializeComponent();
            
            cbxProcess.ItemsSource  = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Process_Code);
            cbxSide.ItemsSource     = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Side_Type);
            cbxTask.ItemsSource     = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Task_Type);

            ticket.CreateDate       = DateTime.Now;
            ticket.IssueStatus      = 21;

            DataContext             = ticket;
        }

        private void SaveButtonClickEvent(object sender, RoutedEventArgs e)
        {
            TicketDBInsert();
            Close();
        }

        void TicketDBInsert()
        {
            var data = (IssueTicket)DataContext;

            var query = new StringBuilder();
            query.Append("INSERT INTO tb_issue_ticket ");
            query.Append("(Process_Code, Side_Type, Unit, Issue_Status, Task_Type, Title, Create_Date, Deadline_Date) ");
            query.Append("VALUES (");

            query.Append($"'{data.ProcessCode}', ");
            query.Append($"'{data.SideType}', ");
            query.Append($"'{data.Unit}', ");
            query.Append($"'{data.IssueStatus}', ");
            query.Append($"'{data.TaskType}', ");
            query.Append($"'{data.Title}', ");
            query.Append($"'{data.CreateDate.ToString("yyyy-MM-dd")}', ");
            query.Append($"'{(string.IsNullOrEmpty(data.Deadline) ? "" : data.Deadline)}');");
            var ticketNo = DB.ExecuteWithLastIdx(query.ToString());
            data.No = ticketNo;

            if (!string.IsNullOrEmpty(tbxComment.Text)) CommentDBInsert(ticketNo);
        }

        void CommentDBInsert(int ticketNo)
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO tb_issue_comment ");
            query.Append("(Ticket_No, Comment) ");
            query.Append("VALUES (");
            query.Append($"'{ticketNo}', ");
            query.Append($"'{tbxComment.Text}');");
            DB.Execute(query.ToString());
        }


        private void CancelButtonClickEvent(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}