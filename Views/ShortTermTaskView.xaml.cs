using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WorkMate.Views
{
    /// <summary>
    /// TodoChecklistView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ShortTermTaskView : UserControl, INotifyPropertyChanged
    {
        public static ObservableCollection<ShortTermTask> TodoList { get; set; } = new ObservableCollection<ShortTermTask>();

        public static event Action ListCountChanged;

        public ShortTermTaskView()
        {
            InitializeComponent();

            ParsingDBData();
            ListCountChanged?.Invoke();
        }


        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData();
        }

        void ParsingDBData()
        {
            TodoList.Clear();

            try
            {
                var query = $"SELECT * FROM tb_task_short_term WHERE Is_Done='0'";

                var dt    = DB.SelectSingle(query);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;
                    TodoList.Add(new ShortTermTask { 
                        Idx         = (int)row[0],
                        CreateDate  = (DateTime)row[1],
                        Todo        = (string)row[2],
                        IsDone      = (bool)row[3],
                        UpdateDate  = (string)row[4]
                    });
                }
                dgv.ItemsSource = TodoList;
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

        private void RowEditBeginEvent(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e != null)
            {
                var row = (ShortTermTask)e.Row.DataContext;
                row.BeginEdit();
            }
        }

        private void RowEditEndingEvent(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e == null) return;

            var row = (ShortTermTask)e.Row.DataContext;
            if (row.Idx == 0)
            {
                var query = new StringBuilder();
                query.Append("INSERT INTO tb_task_short_term (Create_Date, Todo) VALUES ");
                query.Append($"('{row.CreateDate.ToString("yyyy-MM-dd")}', ");
                query.Append($"'{row.Todo}')");
                var idx = DB.ExecuteWithLastIdx(query.ToString());
                row.Idx = idx;
            }
            else if (row.IsDone != row.backupCopy.IsDone || row.Todo != row.backupCopy.Todo)
            {
                UpdateTbTaskShortTerm(row);

                if (row.IsDone != row.backupCopy.IsDone) UpdateDailyTask(row);
            }

            row.EndEdit();
            ListCountChanged?.Invoke();
        }

        void UpdateTbTaskShortTerm(ShortTermTask row)
        {
            var query = new StringBuilder();
            query.Append("UPDATE tb_task_short_term SET ");
            query.Append($"Create_Date='{row.CreateDate.ToString("yyyy-MM-dd")}', ");
            query.Append($"Todo='{row.Todo}', ");
            query.Append($"Is_Done='{(row.IsDone ? '1' : '0')}', ");
            var upDt = row.IsDone ? DateTime.Now.ToString("yyyy-MM-dd") : "";
            query.Append($"Update_Date='{upDt}' ");
            query.Append($"WHERE Idx='{row.Idx}'");
            DB.Execute(query.ToString());
        }

        void UpdateDailyTask(ShortTermTask row)
        {
            var date = row.CreateDate;
            var daily = DailyTaskView.DailyTasks.FirstOrDefault(x => x.Date == date);
            if (daily != null)
            {
                daily.Note += new StringBuilder().AppendLine($"- {row.Todo}");

                var query = new StringBuilder();
                query.Append("UPDATE tb_daily_task SET ");
                query.Append($"Note='{daily.Note}' ");
                query.Append($"WHERE Date='{date.ToString("yyyy-MM-dd")}' ");
                query.Append($"AND User_ID='{MainView.User.UserID}'");
                DB.Execute(query.ToString());
            }
        }

        private void AddClickEvent(object sender, RoutedEventArgs e)
        {
            TodoList.Add(new ShortTermTask { 
                CreateDate = WeekDate(), 
                IsDone = false 
            });
        }

        DateTime WeekDate()
        {
            var day = DateTime.Now;
            if (day.DayOfWeek == DayOfWeek.Sunday) day.AddDays(1);
            else if (day.DayOfWeek == DayOfWeek.Saturday) day.AddDays(2);
            return day;
        }

        /*
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // 화면의 크기 가져오기
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // 창의 크기 가져오기
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            // 최상단 오른쪽에 위치하기 위해, 왼쪽 위치를 화면의 너비에서 창의 너비를 뺀 값으로 설정
            this.Left = screenWidth - windowWidth;
            this.Top = screenHeight - windowHeight;
        }
        */
    }
}