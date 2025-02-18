using MahApps.Metro.Controls;
using WorkMate.Feature;
using WorkMate.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace WorkMate.Views
{
    /// <summary>
    /// LoginView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        public static Account User { get; set; } = new Account();

        public LoginView()
        {
            InitializeComponent();

            tb_id.Text = App.IConfig["user:id"];
            tb_pw.Text = App.IConfig["user:pw"];
        }

        #region login button click event
        private void ClickLogin(object sender, RoutedEventArgs e)
        {
            User.UserID = tb_id.Text;
            User.PW = tb_pw.Text;

            if (string.IsNullOrEmpty(User.UserID) || string.IsNullOrEmpty(User.PW))
            {
                statusbar.MessageQueue.Enqueue("ID or PW 확인");
                return;
            }

            var query = $"SELECT * FROM tb_account WHERE ID='{User.UserID}' AND PW='{User.PW}';";

            try
            {
                var dt  = DB.SelectSingle(query);
                var row = dt.Rows[0].ItemArray;

                User.EmployeeNo = (ushort)row[0];
                User.IsActive   = (bool)row[1];
                User.TeamCode   = (ushort)row[4];
                User.FirstName  = (string)row[5];
                User.LastName   = (string)row[6];
                User.JoinDate   = (DateTime)row[7];
                User.LeaveDate  = (string)row[8];

                if (User.IsActive)
                {
                    new MainView().Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }
        #endregion

        private void ClickSetting(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory + "/AppConfig.json";
            if (File.Exists(path) == true) Process.Start("Notepad.exe", path);
        }
    }
}