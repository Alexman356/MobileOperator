using System;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class EditAbonentPage : Page
    {
        private Abonent CurrentAbonent;

        public EditAbonentPage(Abonent selectedAbonent)
        {
            InitializeComponent();
            DataContext = selectedAbonent;
            CurrentAbonent = selectedAbonent;
            Validator = new Validator();

            if (CurrentAbonent.Person.Gender == "Мужской")
            {
                CmbBoxAbonent.SelectedIndex = 0;
            }

            if (CurrentAbonent.Person.Gender == "Женский")
            {
                CmbBoxAbonent.SelectedIndex = 1;
            }
        }

        private Validator Validator { get; }

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new AbonentsPage());
        }

        private void BtnSaveDataClick(object sender, RoutedEventArgs e)
        {
            if (!Validator.IsValidPersonData(CurrentAbonent.Person))
            {
                return;
            }

            if (CmbBoxAbonent.SelectedIndex == 0)
            {
                CurrentAbonent.Person.Gender = "Мужской";
            }

            if (CmbBoxAbonent.SelectedIndex == 1)
            {
                CurrentAbonent.Person.Gender = "Женский";
            }

            try
            {
                Context.Get().SaveChanges();
                MessageBox.Show("Абонент отредактирован!");
                NavigationService.Navigate(new AbonentsPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}