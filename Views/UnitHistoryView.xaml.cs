using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WorkMate.Views
{
    /// <summary>
    /// UnitHistoryView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UnitHistoryView : MetroWindow, INotifyPropertyChanged
    {
        ObservableCollection<UnitHistory> UnitHistories = new ObservableCollection<UnitHistory>();        

        public UnitHistoryView(UnitStatus unit)
        {
            InitializeComponent();
            
            DataContext = unit;
            ParsingDBData(unit.UnitNo);
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData(((UnitStatus)DataContext).UnitNo);
        }

        void ParsingDBData(short unitNo)
        {
            UnitHistories.Clear();

            try
            {
                var query = $"SELECT Idx, Task_Type, Note, Ticket_No, Create_Date FROM tb_unit_history WHERE Unit_No={unitNo};";

                var dt = DB.SelectSingle(query.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;
                    UnitHistories.Add(new UnitHistory { 
                        Idx         = (int)row[0],
                        TaskType    = (ushort)row[1],
                        Note        = (string)row[2],
                        TicketNo    = (int)row[3],
                        CreateDT    = (DateTime)row[4],
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
            var data = (UnitStatus)DataContext;

            UnitHistories.Add(new UnitHistory { 
                TicketNo = data.UnitNo
            });
        }

        private void RowEditBeginEvent(object sender, System.Windows.Controls.DataGridBeginningEditEventArgs e)
        {

        }

        private void RowEditEndingEvent(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {

        }
    }
}