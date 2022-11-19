using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class ChooseRateForContract : Page
    {
        public ChooseRateForContract(ContractsPage contractsPage, Number Number_telephone)
        {
            InitializeComponent();
            DGRate.ItemsSource = Context.Get().Rates.ToList();
            СurrentNumber = Number_telephone;
            ContractsPage = contractsPage;
        }

        private ContractsPage ContractsPage { get; set; }

        private Number СurrentNumber { get; }

        private void BtnSaveChooseRateClick(object sender, RoutedEventArgs e)
        {
            var rate = (Rate)DGRate.SelectedItem;
            СurrentNumber.rate_ID = rate.Rate_ID;
            Context.Get().SaveChanges();
            MessageBox.Show("The rate is connected!");
            ContractsPage = new ContractsPage();

            if (UserIdentity.Role == "Абонент")
            {
                ContractsPage.BdrDelContract.Visibility = Visibility.Collapsed;
                ContractsPage.BdrAddNumber.Visibility = Visibility.Collapsed;
                ContractsPage.BdrAddContract.Visibility = Visibility.Collapsed;
            }

            ContractsPage = new ContractsPage();
            GoToPage();
        }

        private void BtnContractPageClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The rate is not connected");
            GoToPage();
        }

        private void GoToPage()
        {
            NavigationService.Navigate(ContractsPage);
        }

        private void CmbBoxRateOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxRate.SelectedIndex != -1)
            {
                TxtBoxRate.IsEnabled = true;
            }
        }

        private void TxtBoxRateOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = TxtBoxRate.Text.ToLower();

            SearchRate(text);
        }

        private void SearchRate(string text)
        {
            foreach (var rate in DGRate.ItemsSource as List<Rate>)
            {
                var rowIsContainsText = RowIsContainsText(rate, text);
                ChangeRowVisible(rate, rowIsContainsText);
            }
        }

        private bool RowIsContainsText(Rate rate, string text)
        {
            switch (CmbBoxRate.SelectedIndex)
            {
                case 0:
                {
                    return rate.Rate_ID.ToString().Contains(text);
                }

                case 1:
                {
                    return rate.Name_rate.ToLower().Contains(text);
                }

                default:
                {
                    return false;
                }
            }
        }

        private void ChangeRowVisible(Rate row, bool isToShow)
        {
            var itemContainer = DGRate.ItemContainerGenerator.ContainerFromItem(row);

            if (!(itemContainer is DataGridRow dataGridRow))
            {
                return;
            }

            if (isToShow)
            {
                dataGridRow.Visibility = Visibility.Visible;
            }

            if (!isToShow)
            {
                dataGridRow.Visibility = Visibility.Collapsed;
            }
        }
    }
}
