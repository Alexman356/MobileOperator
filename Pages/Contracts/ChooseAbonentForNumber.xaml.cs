using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class ChooseAbonentForNumber : Page
    {
        public ChooseAbonentForNumber(Contract selectedContract)
        {
            InitializeComponent();
            DGAbonents.ItemsSource = Context.Get().Abonents.ToList();
            
            selectedContract.Status = true;
            selectedContract.employee_ID = EmployeeIdentity.employee_ID; //TODO Разграничение прав
            selectedContract.Date = DateTime.Now;
            CurrentContract = selectedContract;
            DataContext = selectedContract;


            IQueryable<string> numbersForHide = Context.Get().Contracts
               .Select(contract => contract.Number_telephone);

            IQueryable<Number> numberForShow = Context.Get().Numbers
                .Where(number => !numbersForHide.Contains(number.Number_telephone));

            DGNumbers.ItemsSource = numberForShow.ToList();
        }

        private Contract CurrentContract { get; set; }

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

            NavigationService.Navigate(new ChooseRateForContract(numberPhone));
        }

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContractsPage());
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
                MessageBox.Show("Выберите фильтр поиска");

                return;
            }

            var text = TxtBoxSearchAbonent.Text.ToLower();

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
