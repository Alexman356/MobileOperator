using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class AbonentsPage : Page
    {
        public AbonentsPage()
        {
            InitializeComponent();
            DGAbonents.ItemsSource = Context.Get().Abonents.ToList();
        }

        private void BtnDeleteAbonentClick(object sender, RoutedEventArgs e)
        {
            var abonentIdsForRemove = DGAbonents.SelectedItems.Cast<Abonent>()
                .Select(abonent => abonent.abonent_ID);

            IQueryable<Abonent> abonentsForRemove = Context.Get().Abonents
                .Where(abonent => abonentIdsForRemove.Contains(abonent.abonent_ID));

            IQueryable<int> personIdsForRemove = abonentsForRemove
                .Select(abonent => abonent.person_ID);

            IQueryable<string> userIdsForRemove = abonentsForRemove
                .Select(abonent => abonent.user_login);

            IQueryable<Person> personsForRemove = Context.Get().Persons
                .Where(person => personIdsForRemove.Contains(person.person_ID));

            IQueryable<User> usersForRemove = Context.Get().Users
                .Where(user => userIdsForRemove.Contains(user.Login));

            IQueryable<Contract> contractsForRemove = Context.Get().Contracts
                .Where(contract => abonentIdsForRemove.Contains(contract.abonent_ID));

            var dialogResult = MessageBox.Show(
                $"You definitely want to delete the following {abonentsForRemove.Count()} elements?",
                "Attention!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    Context.Get().Contracts.RemoveRange(contractsForRemove);
                    Context.Get().Abonents.RemoveRange(abonentsForRemove);
                    Context.Get().Persons.RemoveRange(personsForRemove);
                    Context.Get().Users.RemoveRange(usersForRemove);
                    Context.Get().SaveChanges();
                    MessageBox.Show("The data has been successfully deleted!");
                    DGAbonents.ItemsSource = Context.Get().Abonents.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnEditAbonentClick(object sender, RoutedEventArgs e)
        {
            if (DGAbonents.SelectedItem != null)
            {
                GoToEditAbonentPage(DGAbonents.SelectedItem as Abonent);
            }
            else
            {
                MessageBox.Show("Select the subscriber to edit");
            }
        }

        private void GoToEditAbonentPage(Abonent abonent)
        {
            var editAbonentPage = new EditAbonentPage(this, abonent);
            NavigationService.Navigate(editAbonentPage);
        }

        private void CmbBoxAbonentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxAbonent.SelectedIndex != -1)
            {
                TxtBoxAbonent.IsEnabled = true;
            }
        }

        private void TxtBoxSearchAbonent(object sender, RoutedEventArgs e)
        {
            if (CmbBoxAbonent.SelectedIndex == -1)
            {
                MessageBox.Show("Select a search filter");

                return;
            }

            var text = TxtBoxAbonent.Text.ToLower();

            SearchAbonent(text);
        }

        private void SearchAbonent(string text)
        {
            foreach (var abonent in DGAbonents.ItemsSource as List<Abonent>)
            {
                var rowIsContainsText = RowIsContainsText(abonent, text);
                ChangeRowVisible(abonent, rowIsContainsText);
            }
        }

        private bool RowIsContainsText(Abonent abonent, string text)
        {
            switch (CmbBoxAbonent.SelectedIndex)
            {
                case 0:
                    {
                        return abonent.abonent_ID.ToString().Contains(text);
                    }
                case 1:
                    {
                        return abonent.Person.Birthdate_s.Contains(text);
                    }
                case 2:
                    {
                        return abonent.Person.Number_passport.Contains(text);
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private void ChangeRowVisible(Abonent row, bool isToShow)
        {
            var itemContainer = DGAbonents.ItemContainerGenerator.ContainerFromItem(row);

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