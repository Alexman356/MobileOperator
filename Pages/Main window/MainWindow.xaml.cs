using MobileOperator.Pages;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MobileOperator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly List<Page> list;

        public MainWindow()
        {
            InitializeComponent();
            list = new List<Page>
            {
                new AbonentsPage(),
                new RatesPage(),
                new EmployeesPage(),
                new ContractsPage(),
                new SearchPage()
            };
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

        private void BtnAbonents_Click(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new AbonentsPage();
        }
        private void BtnRates_Click(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new RatesPage();
        }

        private void BtnEmployees_Click(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new EmployeesPage();
        }

        private void BtnContracts_Click(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new ContractsPage();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            PageStart.Content = new SearchPage();
        }

        private void BtnHide_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void BtnSecurity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}