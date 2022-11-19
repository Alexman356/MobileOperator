using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class ChooseAbonentForNumberPage : Page
    {
        public ChooseAbonentForNumberPage(ContractsPage contractsPage, Contract selectedContract)
        {
            InitializeComponent();
            DGAbonents.ItemsSource = Context.Get().Abonents.ToList();
            selectedContract.Status = true;
            selectedContract.employee_ID = EmployeeIdentity.employee_ID;
            selectedContract.Date = DateTime.Now;
            ContractsPage = contractsPage;
            CurrentContract = selectedContract;
            DataContext = selectedContract;

            IQueryable<string> numbersForHide = Context.Get().Contracts
                .Select(contract => contract.Number_telephone);

            IQueryable<Number> numberForShow = Context.Get().Numbers
                .Where(number => !numbersForHide.Contains(number.Number_telephone));

            DGNumbers.ItemsSource = numberForShow.ToList();
        }

        private ContractsPage ContractsPage { get; }

        private Contract CurrentContract { get; }

        private void BtnSaveContractClick(object sender, RoutedEventArgs e)
        {
            if (DGAbonents.SelectedItem == null)
            {
                MessageBox.Show("The abonent was not selected!");

                return;
            }

            if (DGNumbers.SelectedItem == null)
            {
                MessageBox.Show("The number was not selected!");

                return;
            }

            var abonent = (Abonent)DGAbonents.SelectedItem;
            CurrentContract.abonent_ID = abonent.abonent_ID;

            var number = (Number)DGNumbers.SelectedItem;
            CurrentContract.Number_telephone = number.Number_telephone;

            Context.Get().Contracts.Add(CurrentContract);
            Context.Get().SaveChanges();
            MessageBox.Show("New number added!");

            var numberPhone = Context.Get().Numbers
                .SingleOrDefault(numberTel => numberTel.Number_telephone == CurrentContract.Number_telephone);

            GoToPage(new ChooseRateForContract(ContractsPage, numberPhone));
        }

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            GoToPage(ContractsPage);
        }

        private void GoToPage(Page page)
        {
            NavigationService.Navigate(page);
        }

        private void CmbBoxAbonentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxAbonent.SelectedIndex != -1)
            {
                TxtBoxSearchAbonent.IsEnabled = true;
            }
        }

        private void TxtBoxSearchAbonentTextChanged(object sender, RoutedEventArgs e)
        {
            if (CmbBoxAbonent.SelectedIndex == -1)
            {
                MessageBox.Show("Select a search filter");

                return;
            }

            var text = TxtBoxSearchAbonent.Text.ToLower();

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