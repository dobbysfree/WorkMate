using Akka.Actor;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using WorkMate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace WorkMate
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        #region Parameter

        public static IConfiguration IConfig { get; set; }
        public static ILogger ILog { get; set; }
        public static ActorSystem ActSys { get; set; }
        public static string IConfDB { get; set; }
        #endregion

        #region Main
        public App()
        {
            ILog = new LoggerConfiguration()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.File(
                   @"logs/log_.txt",
                   rollingInterval: RollingInterval.Day,
                   outputTemplate: "[{Timestamp:HH:mm:ss.fffff}] {Message:lj}{NewLine}",
                   fileSizeLimitBytes: 50_000_000,
                   rollOnFileSizeLimit: true,
                   shared: true,
                   flushToDiskInterval: TimeSpan.FromSeconds(1),
                   retainedFileCountLimit: 50)
               .CreateLogger();

            // Create Actor System
            ActSys = ActorSystem.Create("ActSys");

            IConfig = new ConfigurationBuilder().AddJsonFile("AppConfig.json").Build();

            IConfDB = new StringBuilder()
                .Append($"server={IConfig["mysql:ip"]}").Append(";")
                .Append($"port={IConfig["mysql:port"]}").Append(";")
                .Append($"database={IConfig["mysql:database"]}").Append(";")
                .Append($"uid={IConfig["mysql:uid"]}").Append(";")
                .Append($"pwd={IConfig["mysql:pwd"]}").Append(";")
                .Append($"{IConfig["mysql:conf"]}").ToString();
        }
        #endregion
    }
}