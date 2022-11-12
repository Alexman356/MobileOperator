using MobileOperator.Pages;
using System;
using System.Linq;
using System.Text;
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

        private void BtnDelContractClick(object sender, RoutedEventArgs e)
        {/*
            var applicationForRemoving = DGApplication.SelectedItems.Cast<Application>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {applicationForRemoving.Count()} элементов?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MobileOperatorEntities.GetContext().Applications.RemoveRange(applicationForRemoving);
                    MobileOperatorEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");
                    DGApplication.ItemsSource = MobileOperatorEntities.GetContext().Staff.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }*/
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

    }
}
