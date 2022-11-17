using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class AddEditRatesPage : Page//TODO Проверка на буквы в стоимости и т.п.
    {
        private Rate CurrentRate;

        public AddEditRatesPage(Rate selectedRate)
        {
            InitializeComponent();

            if (selectedRate.Name_rate != null)
            {
                TitleNameRate.Text = "Edit rate";
            }

            CurrentRate = selectedRate;
            DataContext = CurrentRate;
            Validator = new Validator();
        }

        private Validator Validator { get; }

        private void BtnSaveRateClick(object sender, RoutedEventArgs e)
        {
            if (!Validator.IsValidRateData(CurrentRate))
            {
                return;
            }

            try
            {
                if (CurrentRate.Rate_ID != 0)
                {
                    Context.Get().SaveChanges();
                    MessageBox.Show("Тариф отредактирован!");
                    NavigationService.Navigate(new RatesPage());
                }
                else
                {
                    Context.Get().Rates.Add(CurrentRate);
                    Context.Get().SaveChanges();
                    MessageBox.Show("Тариф добавлен!");
                    NavigationService.Navigate(new RatesPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBackRateClick(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new RatesPage());
        }
    }
}
