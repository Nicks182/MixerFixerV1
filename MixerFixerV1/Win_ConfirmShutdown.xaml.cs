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
using System.Windows.Shapes;

namespace MixerFixerV1
{
    /// <summary>
    /// Interaction logic for Win_ConfirmShutdown.xaml
    /// </summary>
    public partial class Win_ConfirmShutdown : Window
    {
        public int G_DialogResult = 0; // Default, do nothing

        public Win_ConfirmShutdown()
        {
            InitializeComponent();
        }

        private void Btn_Shutdown_Click(object sender, RoutedEventArgs e)
        {
            G_DialogResult = 1;
            Close();
        }

        private void Btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            G_DialogResult = 2;
            Close();
        }

    }
}
