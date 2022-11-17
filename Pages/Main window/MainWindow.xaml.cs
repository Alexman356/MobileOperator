using System.Windows;
using System.Windows.Input;

namespace MobileOperator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            var addUniqueRandomDataCommand = new AddUniqueRandomDataCommand();
            addUniqueRandomDataCommand.Execute();
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