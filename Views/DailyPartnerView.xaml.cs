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
    /// DailyPartnerView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DailyPartnerView : UserControl, INotifyPropertyChanged
    {
        ObservableCollection<DailyPartner> DailyPartners = new ObservableCollection<DailyPartner>();
        
        public DailyPartnerView()
        {
            InitializeComponent();
            dtPicker.SelectedDate = DateTime.Now;
            cbxCompany.ItemsSource = MainView.DicKeyValues.Where(x => x.Value.iKey == LogTag.Empty || x.Value.iKey == LogTag.Company);
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData();
        }

        void ParsingDBData()
        {
            DailyPartners.Clear();

            try
            {   
                var yyyy    = dtPicker.SelectedDate.Value.Year;
                var mm      = dtPicker.SelectedDate.Value.Month;

                StringBuilder query = new StringBuilder();
                query.Append($"SELECT * FROM tb_daily_partner WHERE date >= '{yyyy}-{mm}-01' AND date <= '{yyyy}-{mm}-31'");
                
                var companyVa = (ushort)cbxCompany.SelectedValue;
                if (companyVa != 0) query.Append($" AND Company='{companyVa}';");

                var dt = DB.SelectSingle(query.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;

                    DailyPartners.Add(new DailyPartner { 
                        Idx         = (int)row[0],
                        Date        = (DateTime)row[1],
                        Company     = (ushort)row[2],
                        Worker      = (string)row[3],
                        TodayTask   = (string)row[4],
                        NextdayTask = (string)row[5],
                        Memo        = (string)row[6]
                    });
                }

                dgv.ItemsSource = DailyPartners;
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        private void SubViewClickEvent(object sender, RoutedEventArgs e)
        {
            var rst = new SubDailyPartnerView(null).ShowDialog();
            if (rst.Value) ParsingDBData();
        }

        private void ShowSubDailyPartner_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgv.SelectedValue != null)
            {
                var rst = new SubDailyPartnerView((DailyPartner)dgv.SelectedValue).ShowDialog();                
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

    }
}