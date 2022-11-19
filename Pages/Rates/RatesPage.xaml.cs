using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            GoToAddEditRatesPage(new Rate());
        }

        private void BtnEditRateClick(object sender, RoutedEventArgs e)
        {
            if (DGRate.SelectedItem != null)
            {
                GoToAddEditRatesPage(DGRate.SelectedItem as Rate);
            }
            else
            {
                MessageBox.Show("Select the rate to edit");
            }
        }

        private void GoToAddEditRatesPage(Rate rate)
        {
            var addEditRatesPage = new AddEditRatesPage(this, rate);
            NavigationService.Navigate(addEditRatesPage);
        }

        private void BtnDeleteRateClick(object sender, RoutedEventArgs e)
        {
            var ratesForRemove = DGRate.SelectedItems.Cast<Rate>().ToList();

            var dialogResult = MessageBox.Show(
                $"You definitely want to remove the following {ratesForRemove.Count()} elements?",
                "Attention!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    Context.Get().Rates.RemoveRange(ratesForRemove);
                    Context.Get().SaveChanges();
                    MessageBox.Show("The data has been successfully deleted!");
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
