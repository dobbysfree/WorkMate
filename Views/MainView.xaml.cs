using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace WorkMate.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : MetroWindow, INotifyPropertyChanged
    {
        public static Account User { get; set; } = new Account();
        public static Dictionary<ushort, DicEnum> DicKeyValues { get; set; }
        public static List<Unit> Units { get; set; } 
        
        public MainView()
        {
            InitializeComponent();
            var rst = GetAccountInfoFromDB();
            if (!rst) Close();

            GetTypeEnumsFromDB();
            GetUnitListFromDB();

            Title = $"[{User.Team}] {User.FirstName}";
            StatusTitle.Content = $"{User.Area} {User.Role} Support Application";

            mnDailyTask.Tag     = new DailyTaskView();
            mnUnitStatus.Tag    = new UnitStatusView();
            mnIssues.Tag        = new IssueTicketView();
            mnInvestment.Tag    = new InvestmentView();
            mnDailyPartner.Tag  = new DailyPartnerView();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = e.InvokedItem;
        }

        void GetTypeEnumsFromDB()
        {
            DicKeyValues = new Dictionary<ushort, DicEnum>();

            try
            {
                var query = $"SELECT * FROM tb_enums;";
                var dt    = DB.SelectSingle(query);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;
                    DicKeyValues[(ushort)row[0]] = new DicEnum { iKey = (string)row[1], iValue = (string)row[2] };
                }
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        void GetUnitListFromDB()
        {
            Units = new List<Unit>();
            try
            {
                var query = $"SELECT Process_Code, Side_Type, Unit FROM tb_unit_status;";
                var dt = DB.SelectSingle(query);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i].ItemArray;
                    Units.Add(new Unit { 
                        //UnitNo      = (ushort)row[0],
                        ProcessCode = (ushort)row[0],
                        SideType    = (ushort)row[1],
                        UnitName    = (string)row[2]
                    });                    
                }
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        private void ClosedApp(object sender, EventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        bool GetAccountInfoFromDB()
        {
            string id = App.IConfig["user:id"];
            var query = $"SELECT * FROM tb_account WHERE User_ID='{id}'";
            try
            {
                var dt = DB.SelectSingle(query);
                if (dt.Rows.Count == 0)
                {
                    App.ILog.Error($"The user ID {id} does not exist");
                    return false;
                }

                var row         = dt.Rows[0].ItemArray;
                User.EmployeeNo = (ushort)row[0];
                User.IsActive   = (bool)row[1];
                User.UserID     = (string)row[2];
                User.PW         = (string)row[3];
                User.AreaCode   = (ushort)row[4];
                User.TeamCode   = (ushort)row[5];
                User.Role       = (string)row[6];
                User.FirstName  = (string)row[7];
                User.LastName   = (string)row[8];
                User.JoinDate   = (DateTime)row[9];
                User.LeaveDate  = (string)row[10];
                User.Level      = (ushort)row[11];
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
            return true;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

    }
}