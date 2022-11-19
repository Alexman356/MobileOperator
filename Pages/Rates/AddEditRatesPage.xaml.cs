using System;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class AddEditRatesPage : Page
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
            Validator = new Validator();
        }

        private RatesPage RatesPage { get; set; }

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
                    MessageBox.Show("The rate has been edited!");
                    GoToRatesPage();
                }
                else
                {
                    Context.Get().Rates.Add(CurrentRate);
                    Context.Get().SaveChanges();
                    MessageBox.Show("The rate has been added!");
                    GoToRatesPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GoToRatesPage()
        {
            RatesPage = new RatesPage();
            NavigationService.Navigate(RatesPage);
        }

        private void BtnBackRateClick(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new RatesPage());
        }
    }
}
