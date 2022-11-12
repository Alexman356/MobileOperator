using System;
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
                $"Вы точно хотите удалить следующие {abonentsForRemove.Count()} элементов?",
                "Внимание!",
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
                    MessageBox.Show("Данные успешно удалены!");
                    DGAbonents.ItemsSource = Context.Get().Abonents.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnEditAbonentClick(object sender, RoutedEventArgs e)
        {
            if (DGAbonents.SelectedItem != null)
            {
                NavigationService?.Navigate(new EditAbonentPage((Abonent)DGAbonents.SelectedItem));
            }
            else
            {
                MessageBox.Show("Выберите абонента");
            }
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
                MessageBox.Show("Выберите фильтр поиска");

                return;
            }

            var text = TxtBoxAbonent.Text.ToLower();

            SearchAbonent(text);
        }

        private void SearchAbonent(string text)
        {
            foreach (var abonent in Context.Get().Abonents)
            {
                ChangeRowVisible(abonent, RowIsContainsText(abonent, text));
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
                        return abonent.Person.Birthdate.ToString().Contains(text);
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

        private DependencyObject GetItemContainer(Abonent row)
        {
            var abonentsForHide = Context.Get().Abonents
                .Where(abonent => abonent.abonent_ID == row.abonent_ID)
                .ToList()
                .FirstOrDefault();

            var itemContainer = DGAbonents.ItemContainerGenerator.ContainerFromItem(abonentsForHide);

            return itemContainer;
        }
    }
}