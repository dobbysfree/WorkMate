using WorkMate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkMate.Views
{
    /// <summary>
    /// ConsumableView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConsumableView : UserControl
    {
        List<Consumable> Consumables { get; set; } = new List<Consumable>();

        public ConsumableView()
        {
            InitializeComponent();
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        void ParsingData()
        {

        }
    }
}