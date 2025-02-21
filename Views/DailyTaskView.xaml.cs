using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WorkMate.Views
{
    /// <summary>
    /// DailyTaskView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DailyTaskView : UserControl, INotifyPropertyChanged
    {
        public static ObservableCollection<DailyTask> DailyTasks { get; set; } = new ObservableCollection<DailyTask>();
        DateTime ExistLastDate { get; set; }

        public DailyTaskView()
        {
            InitializeComponent();

            dtPicker.SelectedDate = DateTime.Now;
            ParsingDBData();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData();
        }

        void ParsingDBData()
        {
            DailyTasks.Clear();

            try
            {   
                var yyyy    = dtPicker.SelectedDate.Value.Year;
                var mm      = dtPicker.SelectedDate.Value.Month;

                StringBuilder query = new StringBuilder();
                query.Append("SELECT t.*, ");
                query.Append("(SELECT User_ID FROM tb_account WHERE Employee_No = t.Employee_No) AS UserId, ");
                query.Append("(SELECT MAX(date) FROM tb_daily_task WHERE Employee_No = t.Employee_No) AS LastDate ");
                query.Append("FROM tb_daily_task t ");
                query.Append($"WHERE Employee_No='{MainView.User.EmployeeNo}' ");
                query.Append($"AND date >= '{yyyy}-{mm}-01' ");
                query.Append($"AND date <= '{yyyy}-{mm}-31'");
                
                var dt = DB.SelectSingle(query.ToString());                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;

                    DailyTasks.Add(new DailyTask { 
                        Date        = (DateTime)row[0],
                        EmployeeNo  = (ushort)row[1],
                        StartTime   = (string)row[2],
                        EndTime     = (string)row[3],
                        AreaCode    = (ushort)row[4],
                        Note        = (string)row[5],
                        Info        = (string)row[6],
                        UserID      = (string)row[7],
                    });
                    ExistLastDate = (DateTime)row[8];
                }

                dgv.ItemsSource = DailyTasks;
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        private void RowEditEndingEvent(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e != null)
            {
                var row = (DailyTask)e.Row.DataContext;
                var query = new StringBuilder();
                query.Append("UPDATE tb_daily_task SET ");
                query.Append($"Start_Time='{row.StartTime}', ");
                query.Append($"End_Time='{row.EndTime}', ");
                query.Append($"Area='{row.AreaCode}', ");
                query.Append($"Note='{row.Note}', ");                
                query.Append($"Info='{row.Info}' ");
                query.Append($"WHERE Date='{row.Date.ToString("yyyy-MM-dd")}' ");
                query.Append($"AND Employee_No='{row.EmployeeNo}'");

                Console.WriteLine(query.ToString());
                DB.Execute(query.ToString());
            }
        }

        private void RowEditBeginEvent(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e != null)
            {
                var row = (DailyTask)e.Row.DataContext;
                row.BeginEdit();
            }
        }

        private void AddClickEvent(object sender, RoutedEventArgs e)
        {
            DailyTasks.Add(new DailyTask {
                Date        = ExistLastDate,
                EmployeeNo  = MainView.User.EmployeeNo,
                StartTime   = "08:00",
                EndTime     = "17:00",
                AreaCode    = 34
            });
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        /*
        void InsertTaskDate()
        {
            for (int i = 0; i < 60; i++)
            {
                DateTime dt = DateTime.Parse("2025-04-10").AddDays(i);
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday) continue;

                var query = new StringBuilder();
                query.Append("INSERT INTO tb_daily_task VALUES (");
                query.Append($"'{dt.ToString("yyyy-MM-dd")}', ");
                query.Append($"'Sam', ");
                query.Append($"'08:00', ");
                query.Append($"'17:00', ");
                query.Append($"'ESMI_LS', ");
                query.Append($"'', ");
                query.Append($"''); ");
                DB.Execute(query.ToString());
            }
        }
        */
    }
}