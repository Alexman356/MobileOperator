using System.Windows;
using System.Windows.Input;

namespace MobileOperator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridTopRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void BtnHideClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void BtnAbonentsClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new AbonentsPage();
        }

        private void BtnRatesClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new RatesPage();
        }

        private void BtnEmployeesClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new EmployeesPage();
        }

        private void BtnContractsClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new ContractsPage();
        }

        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new SearchPage();
        }

        private void BtnSecurityClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new UserPage();
        }

        private void BtnInfoClick(object sender, RoutedEventArgs e)
        {

        }
    }
}