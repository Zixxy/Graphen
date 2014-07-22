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

namespace Graphen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void OpenNewSolution(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Show();
        }

        //TODO
        private void LoadPreviousGraph(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //TODO
        private void OpenSettingsDialog(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.Close();
        }
    }
}
