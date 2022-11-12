using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MobileOperator.Pages
{
    public partial class AddEditRatesPage : Page
    {
        private Rate currentRate;

        public AddEditRatesPage(Rate selectedRate)
        {
            InitializeComponent();

            if (selectedRate != null)
            {
                TitleNameRate.Text = "Edit rate";
            }

            currentRate = selectedRate;

            DataContext = currentRate;
        }

        private void BtnSaveRate_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentRate.Name_rate))///////////Подумать над условиями!
            {////////////////////////
                errors.AppendLine("DANGER Name!");////////////
            }/////////////////////

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (currentRate.rate_ID != 0)
                {
                    Context.Get().SaveChanges();
                    MessageBox.Show("Тариф отредактирован!");
                    NavigationService.Navigate(new RatesPage());
                }
                else
                {
                    Context.Get().Rates.Add(currentRate);
                    Context.Get().SaveChanges();
                    MessageBox.Show("Тариф добавлен!");
                    NavigationService.Navigate(new RatesPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBackRate_Click(object sender, RoutedEventArgs e)
        {
            Context.Set(null);

            NavigationService.Navigate(new RatesPage());
        }
    }
}
