using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class ChooseRateForContract : Page
    {
        public ChooseRateForContract(Number Number_telephone)
        {
            InitializeComponent();
            DGRate.ItemsSource = Context.Get().Rates.ToList();
            СurrentNumber = Number_telephone;
        }

        private Number СurrentNumber { get; set; }

        private void BtnSaveChooseRateClick(object sender, RoutedEventArgs e)
        {
            var rate = (Rate)DGRate.SelectedItem;

            СurrentNumber.rate_ID = rate.Rate_ID;
            Context.Get().SaveChanges();
            MessageBox.Show("Тариф подключен");
            NavigationService.Navigate(new ContractsPage());
        }

        private void BtnContractPageClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Тариф не был выбран");
            NavigationService.Navigate(new ContractsPage());
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
            foreach (var rate in Context.Get().Rates)
            {
                ChangeRowVisible(rate, RowIsContainsText(rate, text));
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
            var itemContainer = GetItemContainer(row);

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

        private DependencyObject GetItemContainer(Rate row)
        {
            var RatesForHide = Context.Get().Rates
                .Where(rate => rate.Rate_ID == row.Rate_ID)
                .ToList()
                .FirstOrDefault();
            var itemContainer = DGRate.ItemContainerGenerator.ContainerFromItem(RatesForHide);

            return itemContainer;
        }
    }
}
