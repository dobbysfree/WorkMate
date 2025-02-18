using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WorkMate.Views
{
    /// <summary>
    /// InvestmentView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InvestmentView : UserControl, INotifyPropertyChanged
    {
        ObservableCollection<Investment> Investments = new ObservableCollection<Investment>();

        public InvestmentView()
        {
            InitializeComponent();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData();
        }

        void ParsingDBData()
        {
            Investments.Clear();

            try
            {
                var query = $"SELECT * FROM tb_investment;";

                var dt = DB.SelectSingle(query);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;

                    Investments.Add(new Investment { 
                        InvestNo    = (int)row[0],
                        WBSCode     = (string)row[1],
                        Title       = (string)row[2],
                        Item        = (string)row[3],
                        Budget      = (string)row[4],
                        ExecutionAmount = (string)row[5],
                        Margin      = (string)row[6],
                        ProcessLine = (string)row[7],
                        PONo        = (string)row[8],
                        Status      = (short)row[9],
                        UpdateDate  = (DateTime)row[10],
                        Note        = (string)row[11]
                    });
                }

                dgv.ItemsSource = Investments;
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
    }
}