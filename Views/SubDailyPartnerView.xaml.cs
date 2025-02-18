using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace WorkMate.Views
{
    /// <summary>
    /// SubDailyPartnerView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SubDailyPartnerView : MetroWindow, INotifyPropertyChanged
    {
        const string compnay = "Company";
        const string update = "Update";
        const string save = "Save";

        public DailyPartner DataContextItem { get; set; }
        public SubDailyPartnerView(DailyPartner data)
        {
            InitializeComponent();
            cbxCompany.ItemsSource = MainView.DicKeyValues.Where(x => x.Value.iKey == compnay);

            if (data !=  null)
            {
                dtPicker.IsEnabled   = false;
                cbxCompany.IsEnabled = false;
                btnUpdate.Content    = update;
            }
            else
            {
                data        = new DailyPartner();
                data.Date   = DateTime.Now;
                btnUpdate.Content = save;
            }

            DataContextItem = data;
            DataContextItem.BeginEdit();
            DataContext = DataContextItem;
         }

        private void UpdateButtonClickEvent(object sender, RoutedEventArgs e)
        {
            var btn = ((System.Windows.Controls.Button)sender).Content.ToString();
            if (btn == update) DBUpdate();
            else if (btn == save) DBInsert();

            DataContextItem.EndEdit();
            DialogResult = true;
            Close();
        }

        void DBUpdate()
        {
            var query = new StringBuilder();
            query.Append("UPDATE tb_daily_partner SET ");
            query.Append($"Worker='{DataContextItem.Worker}', ");
            query.Append($"Today_Task='{DataContextItem.TodayTask}', ");
            query.Append($"Nextday_Task='{DataContextItem.NextdayTask}', ");
            query.Append($"Memo='{DataContextItem.Memo}' ");
            query.Append($"WHERE Idx='{DataContextItem.Idx}';");
            DB.Execute(query.ToString());
        }

        void DBInsert()
        {
            DataContextItem.Company = (ushort)cbxCompany.SelectedValue;

            var query = new StringBuilder();
            query.Append("INSERT INTO tb_daily_partner (Date, Company, Worker, Today_Task, Nextday_Task, Memo) ");
            query.Append("VALUES (");
            query.Append($"'{DataContextItem.Date.ToString("yyyy-MM-dd")}', ");
            query.Append($"'{DataContextItem.Company}', ");
            query.Append($"'{DataContextItem.Worker}', ");
            query.Append($"'{DataContextItem.TodayTask}', ");
            query.Append($"'{DataContextItem.NextdayTask}', ");
            query.Append($"'{DataContextItem.Memo}');");
            DB.Execute(query.ToString());
        }

        private void CancelButtonClickEvent(object sender, RoutedEventArgs e)
        {       
            DataContextItem.CancelEdit();
            DialogResult = false;
            Close();
        }


        // INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
