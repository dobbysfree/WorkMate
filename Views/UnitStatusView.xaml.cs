﻿using WorkMate.Feature;
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
    /// UnitStatusView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UnitStatusView : UserControl, INotifyPropertyChanged
    {
        ObservableCollection<UnitStatus> UnitStatuses = new ObservableCollection<UnitStatus>();
        
        public UnitStatusView()
        {
            InitializeComponent();

            cbxProcess.ItemsSource  = MainView.DicKeyValues.Where(x => x.Value.iKey == "" || x.Value.iKey == "Process_Code");
            cbxSide.ItemsSource     = MainView.DicKeyValues.Where(x => x.Value.iKey == "" || x.Value.iKey == "Side_Type");
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            ParsingDBData();
        }

        void ParsingDBData()
        {
            UnitStatuses.Clear();

            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM tb_unit_status");

                var procssVa    = (ushort)cbxProcess.SelectedValue;
                var sideVa      = (ushort)cbxSide.SelectedValue;

                if (procssVa != 0) query.Append($" WHERE Process_Code='{procssVa}'");
                if (sideVa != 0)
                {
                    query.Append(!query.ToString().Contains("WHERE") ? " WHERE " : " AND ");
                    query.Append($"Side_Type='{sideVa}'");
                }

                var dt = DB.SelectSingle(query.ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;
                    UnitStatuses.Add(new UnitStatus {
                        //UnitNo          = (ushort)row[0],
                        ProcessCode     = (ushort)row[0],
                        SideType        = (ushort)row[1],
                        Unit            = (string)row[2],
                        InvestNo        = (int)row[3],
                        ProcessLine     = (ushort)row[4],
                        Installation    = (string)row[5],
                        IsRTSO          = (string)row[6],
                        IsPower         = (string)row[7],
                        IsYTSO          = (string)row[8],
                        SetupSingle     = (string)row[9],
                        SetupIntegrated = (string)row[10],
                        Consistency     = (string)row[11],
                        Memo            = (string)row[12],
                        ProductNote     = (string)row[13],
                        UpdateDate      = (DateTime)row[14]
                    });                    
                }

                dgv.ItemsSource = UnitStatuses;
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        private void ShowUnitHistory_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgv.SelectedValue != null) new UnitHistoryView((UnitStatus)dgv.SelectedValue).Show();
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