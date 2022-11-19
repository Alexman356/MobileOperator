using System;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class EditAbonentPage : Page
    {
        public EditAbonentPage(AbonentsPage abonentsPage, Abonent selectedAbonent)
        {
            InitializeComponent();
            DataContext = selectedAbonent;
            CurrentAbonent = selectedAbonent;
            AbonentsPage = abonentsPage;
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

        private Abonent CurrentAbonent { get; }

        private AbonentsPage AbonentsPage { get; set; }

        private Validator Validator { get; }

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            GoToAbonentsPage(AbonentsPage);
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
                AbonentsPage = new AbonentsPage();
                GoToAbonentsPage(AbonentsPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GoToAbonentsPage(Page abonentsPage)
        {
            NavigationService.Navigate(abonentsPage);
        }
    }
}