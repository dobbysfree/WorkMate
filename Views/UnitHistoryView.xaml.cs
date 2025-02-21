using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace WorkMate.Views
{
    /// <summary>
    /// UnitHistoryView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UnitHistoryView : MetroWindow, INotifyPropertyChanged
    {
        ObservableCollection<UnitHistory> UnitHistories = new ObservableCollection<UnitHistory>();
        IEnumerable<KeyValuePair<ushort, DicEnum>> TaskTypes { get; set; }

        string UnitNo { get; set; }

        public UnitHistoryView(UnitStatus unit)
        {
            InitializeComponent();
            
            DataContext = unit;
            UnitNo = $"{unit.ProcessCode}{unit.SideType}{unit.Unit.Replace("-", "")}";

            TaskTypes = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Task_Type);

            ParsingDBData();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData();
        }

        void ParsingDBData()
        {
            UnitHistories.Clear();

            try
            {
                var query = $"SELECT * FROM tb_unit_history WHERE Unit_No={UnitNo};";

                var dt = DB.SelectSingle(query.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;
                    UnitHistories.Add(new UnitHistory { 
                        Idx         = (int)row[0],                        
                        TaskType    = (ushort)row[2],
                        Note        = (string)row[3],
                        TicketNo    = (int)row[4],
                        CreateDT    = (DateTime)row[5],
                    });
                }

                dgv.ItemsSource = UnitHistories;
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void AddClickEvent(object sender, RoutedEventArgs e)
        {
            UnitHistories.Add(new UnitHistory { 
                CreateDT = DateTime.Now,                
            });
        }

        private void RowEditBeginEvent(object sender, System.Windows.Controls.DataGridBeginningEditEventArgs e)
        {
            if (e != null)
            {
                var row = (UnitHistory)e.Row.DataContext;
                row.BeginEdit();
            }
        }

        private void RowEditEndingEvent(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e == null) return;

            var row = (UnitHistory)e.Row.DataContext;
            if (row.Idx == 0)
            {
                var query = new StringBuilder();
                query.Append("INSERT INTO tb_unit_history (Unit_No, Task_Type, Note, Ticket_No, Create_Date) VALUES ");
                query.Append("(");
                query.Append($"'{UnitNo}', ");
                query.Append($"'{row.TaskType}', ");
                query.Append($"'{row.Note}', ");
                query.Append($"'{row.TicketNo}', ");
                query.Append($"'{row.CreateDT.ToString("yyyy-MM-dd HH:mm:ss")}'");
                query.Append(")");
                var idx = DB.ExecuteWithLastIdx(query.ToString());
                row.Idx = idx;
            }
            else
            {

            }

            row.EndEdit();
        }
    }
}