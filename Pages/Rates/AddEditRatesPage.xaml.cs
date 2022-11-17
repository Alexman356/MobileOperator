using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class AddEditRatesPage : Page//TODO Проверка на буквы в стоимости и т.п.
    {
        private Rate CurrentRate;

        public AddEditRatesPage(RatesPage ratesPage, Rate selectedRate)
        {
            InitializeComponent();
            if (selectedRate.Name_rate != null)
            {
                TitleNameRate.Text = "Edit rate";
            }

            CurrentRate = selectedRate;
            DataContext = CurrentRate;
            RatesPage = ratesPage;
        }

        private RatesPage RatesPage { get; set; }

        private void BtnSaveRate_Click(object sender, RoutedEventArgs e)
        {
            CheckingEnteredData();

            try
            {
                if (CurrentRate.Rate_ID != 0)
                {
                    Context.Get().SaveChanges();
                    MessageBox.Show("Тариф отредактирован!");
                    GoToRatesPage();
                }
                else
                {
                    Context.Get().Rates.Add(CurrentRate);
                    Context.Get().SaveChanges();
                    MessageBox.Show("Тариф добавлен!");
                    GoToRatesPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void GoToRatesPage()
        {
            RatesPage = new RatesPage();
            NavigationService.Navigate(RatesPage);
        }

        private void CheckingEnteredData()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CurrentRate.Name_rate))
            {
                errors.AppendLine("Incorrect name rate value entered!");
            }
            if (CurrentRate.Cost > 32767)
            {
                errors.AppendLine("Incorrect cost value entered!");
            }
            if (CurrentRate.Internet > 32767)
            {
                errors.AppendLine("Incorrect internet value entered!");
            }
            if (CurrentRate.Minutes > 32767)
            {
                errors.AppendLine("Incorrect minutes value entered!");
            }
            if (CurrentRate.SMS > 32767)
            {
                errors.AppendLine("Incorrect SMS value entered!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }

        private void BtnBackRate_Click(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new RatesPage());
        }
    }
}
