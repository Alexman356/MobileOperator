using System;
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

            DGContract.ItemsSource = Context.Get().Contracts.ToList();
        }

        private void AddContractClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddContract(new Contract()));
        }

        private void AddNumberClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ChooseAbonentForNumber(new Contract()));
        }

        private void EditRateClick(object sender, RoutedEventArgs e)
        {
            if (DGContract.SelectedItem == null)
            {
                MessageBox.Show("The contract was not selected!");

                return;
            }

            var selectedNumber = DGContract.SelectedItem as Contract;
            var numberToChange = Context.Get().Numbers
                .Single(number => number.Number_telephone == selectedNumber.Number_telephone);

            NavigationService?.Navigate(new ChooseRateForContract(numberToChange));
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
                $"Вы точно хотите удалить следующие {contractsForRemove.Count()} элементов?",
                "Внимание!",
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
                MessageBox.Show("Выберите фильтр поиска!");

                return;
            }

            var text = TxtBoxContract.Text.ToLower();

            SearchContract(text);
        }

        private void SearchContract(string text)
        {
            foreach (var application in Context.Get().Contracts)
            {
                ChangeRowVisible(application, RowIsContainsText(application, text));
            }
        }

        private bool RowIsContainsText(Contract contract, string text)//TODO Добавить еще условий
        {
            switch (CmbBoxContract.SelectedIndex)
            {
                case 0:
                    {
                        return contract.contract_ID.ToString().Contains(text);
                    }

                case 1:
                    {
                        return contract.Date.ToString().Contains(text);
                    }

                case 2:
                    {
                        return contract.Number_telephone.ToString().Contains(text);
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private void ChangeRowVisible(Contract row, bool isToShow)
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

        private DependencyObject GetItemContainer(Contract row)
        {
            var contractsForHide = Context.Get().Contracts
                .Where(application => application.contract_ID == row.contract_ID)
                .ToList()
                .FirstOrDefault();
            var itemContainer = DGContract.ItemContainerGenerator.ContainerFromItem(contractsForHide);

            return itemContainer;
        }

        private void DGContractCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var selectedContract = DGContract.SelectedItem as Contract;
            var contractToChange = Context.Get().Contracts
                .Single(contract => contract.contract_ID == selectedContract.contract_ID);
            contractToChange.Status = !contractToChange.Status;
            Context.Get().SaveChanges();
        }
    }
}
