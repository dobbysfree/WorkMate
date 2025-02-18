using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace WorkMate.Views
{
    /// <summary>
    /// IssueCommentView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IssueCommentView : MetroWindow, INotifyPropertyChanged
    {
        ObservableCollection<IssueComment> IssueComments = new ObservableCollection<IssueComment>();
        
        public IssueCommentView(IssueTicket data)
        {
            InitializeComponent();
            DataContext = data;
            ParsingDBData(data.No);
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData(((IssueTicket)DataContext).No);
        }

        void ParsingDBData(int ticketNo)
        {
            IssueComments.Clear();

            try
            {
                var query = $"SELECT Idx, Comment, Create_DT FROM tb_issue_comment WHERE Ticket_No='{ticketNo}';";

                var dt = DB.SelectSingle(query.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;
                    IssueComments.Add(new IssueComment { 
                        Idx      = (int)row[0],
                        Comment  = (string)row[1],
                        CreateDT = (DateTime)row[2]
                    });
                }

                dgv.ItemsSource = IssueComments;
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        private void AddClickEvent(object sender, RoutedEventArgs e)
        {
            IssueComments.Add(new IssueComment { CreateDT = DateTime.Now });
        }

        private void RowEditEndingEvent(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e == null) return;

            var row = (IssueComment)e.Row.DataContext;
            if (row.Idx == 0)
            {
                var query = new StringBuilder();
                query.Append("INSERT INTO tb_issue_comment (Ticket_No, Comment) VALUES ");
                query.Append($"('{((IssueTicket)DataContext).No}', ");
                query.Append($"'{row.Comment}'); ");
                
                var idx = DB.ExecuteWithLastIdx(query.ToString());
                row.Idx = idx;
            }
            else if (row.Comment != row.backupCopy.Comment)
            {
                var query = new StringBuilder();
                query.Append("UPDATE tb_issue_comment SET ");
                query.Append($"Comment='{row.Comment}' ");
                query.Append($"WHERE Idx='{row.Idx}'");
                DB.Execute(query.ToString());
            }
            row.EndEdit();
        }

        private void RowEditBeginEvent(object sender, System.Windows.Controls.DataGridBeginningEditEventArgs e)
        {
            if (e != null)
            {
                var row = (IssueComment)e.Row.DataContext;
                row.BeginEdit();
            }
        }
    }
}