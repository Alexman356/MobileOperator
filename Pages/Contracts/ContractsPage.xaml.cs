using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class ContractsPage : Page
    {
        public ContractsPage()
        {
            InitializeComponent();
            IQueryable<Contract> contracts = Context.Get().Contracts;
            if (UserIdentity.Role == "Абонент")
            {
                contracts = contracts
                    .Where(contract => contract.abonent_ID == AbonentIdentity.abonent_ID);
                ColumnIdContract.Visibility = Visibility.Collapsed;
                ColumnIdEmployee.Visibility = Visibility.Collapsed;
                ColumnIdAbonent.Visibility = Visibility.Collapsed;
            }

            DGContract.ItemsSource = contracts.ToList();
        }

        private void BtnAddContractClick(object sender, RoutedEventArgs e)
        {
            GoToPage(new AddContractPage(this, new Contract()));
        }

        private void BtnAddNumberClick(object sender, RoutedEventArgs e)
        {
            GoToPage(new ChooseAbonentForNumberPage(this, new Contract()));
        }

        private void BtnEditRateClick(object sender, RoutedEventArgs e)
        {
            if (DGContract.SelectedItem == null)
            {
                MessageBox.Show("The contract was not selected!");

                return;
            }

            var selectedNumber = DGContract.SelectedItem as Contract;
            var numberToChange = Context.Get().Numbers
                .Single(number => number.Number_telephone == selectedNumber.Number_telephone);

            GoToPage(new ChooseRateForContract(this, numberToChange));
        }

        private void GoToPage(Page contract)
        {
            NavigationService.Navigate(contract);
        }

        private void BtnDelContractClick(object sender, RoutedEventArgs e)
        {
            var contractsForRemove = DGContract.SelectedItems.Cast<Contract>().ToList();
            if (DGContract.SelectedItem == null)
            {
                MessageBox.Show("The contract for remove was not selected!");

                return;
            }

            var dialogResult = MessageBox.Show(
                $"You definitely want to delete the following {contractsForRemove.Count()} elements?",
                "Attention!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    Context.Get().Contracts.RemoveRange(contractsForRemove);
                    Context.Get().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");
                    DGContract.ItemsSource = Context.Get().Contracts.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DGContractCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var selectedContract = DGContract.SelectedItem as Contract;
            var contractToChange = Context.Get().Contracts
                .Single(contract => contract.contract_ID == selectedContract.contract_ID);
            contractToChange.Status = !contractToChange.Status;
            Context.Get().SaveChanges();
        }

        private void CmbBoxContractSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxContract.SelectedIndex != -1)
            {
                TxtBoxContract.IsEnabled = true;
            }
        }

        private void TxtBoxSearchContract(object sender, TextChangedEventArgs e)
        {
            if (CmbBoxContract.SelectedIndex == -1)
            {
                MessageBox.Show("Choose a search filter");

                return;
            }

            var text = TxtBoxContract.Text.ToLower();

            SearchContract(text);
        }

        private void SearchContract(string text)
        {
            foreach (var contract in DGContract.ItemsSource as List<Contract>)
            {
                var rowIsContainsText = RowIsContainsText(contract, text);
                ChangeRowVisible(contract, rowIsContainsText);
            }
        }

        private bool RowIsContainsText(Contract contract, string text)
        {
            switch (CmbBoxContract.SelectedIndex)
            {
                case 0:
                {
                    return contract.contract_ID.ToString().Contains(text);
                }

                case 1:
                {
                    return contract.Date_s.Contains(text);
                }

                case 2:
                {
                    return contract.Number_telephone.Contains(text);
                }

                case 3:
                {
                    return contract.employee_ID.ToString().Contains(text);
                }

                default:
                {
                    return false;
                }
            }
        }

        private void ChangeRowVisible(Contract row, bool isToShow)
        {
            var itemContainer = DGContract.ItemContainerGenerator.ContainerFromItem(row);

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