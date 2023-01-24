using System.Linq;
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
        public MainWindow(PagesModel pages)
        {
            InitializeComponent();
            Pages = pages;
        }

        private PagesModel Pages { get; }

        private void BtnExitPanelClick(object sender, RoutedEventArgs e)
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
            WindowState = WindowState.Minimized;
        }

        private void BtnAbonentsClick(object sender, RoutedEventArgs e)
        {
            Pages.Abonents.DGAbonents.ItemsSource = Context.Get().Abonents.ToList();
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

        private void BtnSecurityClick(object sender, RoutedEventArgs e)
        {
            Pages.User.DGUser.ItemsSource = Context.Get().Users.ToList();
            PageStart.Content = Pages.User;
        }

        private void BtnProfileClick(object sender, RoutedEventArgs e)
        {
            PageStart.Content = Pages.Profile;
        }

        private void BtnLogOutClick(object sender, RoutedEventArgs e)
        {
            var dialogResult = MessageBox.Show(
                $"Do you really want to log out of your account?",
                "Attention!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                Pages.LoginWindow.Show();
                Close();
            }
        }
    }
}