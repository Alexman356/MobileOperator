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
            Pages = new PagesModel // TODO get from login window
            {
                Abonents = new AbonentsPage(),
                Contracts = new ContractsPage(),
                Employees = new EmployeesPage(),
                Rates = new RatesPage(),
                User = new UserPage(),
            };

            var addUniqueRandomDataCommand = new AddUniqueRandomDataCommand();
            addUniqueRandomDataCommand.Execute();
        }

        private PagesModel Pages { get; }

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
            PageStart.Content = Pages.Abonents;
        }

        private void BtnRatesClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = Pages.Rates;
        }

        private void BtnEmployeesClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = Pages.Employees;
        }

        private void BtnContractsClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = Pages.Contracts;
        }

        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
        }

        private void BtnSecurityClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = Pages.User;
        }

        private void BtnInfoClick(object sender, RoutedEventArgs e)
        {

        }
    }
}