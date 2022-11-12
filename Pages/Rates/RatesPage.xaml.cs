using MobileOperator.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MobileOperator
{
    public partial class RatesPage : Page
    {
        public RatesPage()
        {
            InitializeComponent();
            DGRate.ItemsSource = Context.Get().Rates.ToList();
        }

        private void BtnAddRateClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditRatesPage(new Rate()));
        }

        private void BtnEditRateClick(object sender, RoutedEventArgs e)
        {
            if (DGRate.SelectedItem != null)
            {
                NavigationService.Navigate(new AddEditRatesPage((Rate)DGRate.SelectedItem));
            }
            else
            {
                MessageBox.Show("Выберите тариф");
            }
        }

        private void BtnDeleteRateClick(object sender, RoutedEventArgs e)
        {
            var ratesForRemove = DGRate.SelectedItems.Cast<Rate>().ToList();

            var dialogResult = MessageBox.Show(
                $"Вы точно хотите удалить следующие {ratesForRemove.Count()} элементов?",
                "Внимание!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    Context.Get().Rates.RemoveRange(ratesForRemove);
                    Context.Get().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");
                    DGRate.ItemsSource = Context.Get().Rates.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CmbBoxRateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxRate.SelectedIndex != -1)
            {
                TxtBoxRate.IsEnabled = true;
            }
        }

        private void TxtBoxSearchRate(object sender, TextChangedEventArgs e)
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
