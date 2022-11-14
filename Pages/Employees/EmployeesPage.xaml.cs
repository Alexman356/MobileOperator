using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            DGEmployee.ItemsSource = Context.Get().Employees.ToList();
        }

        private void BtnAddEmployeeClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditEmployeePage(new Employee()));
        }

        private void BtnEditEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (DGEmployee.SelectedItem != null)
            {
                NavigationService.Navigate(new AddEditEmployeePage((Employee)DGEmployee.SelectedItem));
            }
            else
            {
                MessageBox.Show("Выберите сотрудника");
            }
        }

        private void BtnDeleteEmployeeClick(object sender, RoutedEventArgs e)
        {
            var employeeIdsForRemove = DGEmployee.SelectedItems.Cast<Employee>()
               .Select(employee => employee.employee_ID);

            IQueryable<Employee> employeesForRemove = Context.Get().Employees
                .Where(employee => employeeIdsForRemove.Contains(employee.employee_ID));

            IQueryable<int> personIdsForRemove = employeesForRemove
                .Select(employee => employee.person_ID);

            IQueryable<string> userIdsForRemove = employeesForRemove
                .Select(employee => employee.user_login);

            IQueryable<Person> personsForRemove = Context.Get().Persons
                .Where(person => personIdsForRemove.Contains(person.person_ID));

            IQueryable<User> usersForRemove = Context.Get().Users
                .Where(user => userIdsForRemove.Contains(user.Login));

            IQueryable<Contract> contractsForRemove = Context.Get().Contracts///////////////////////////////////////////
                .Where(contract => employeeIdsForRemove.Contains(contract.employee_ID));

            var dialogResult = MessageBox.Show(
                $"Вы точно хотите удалить следующие {employeesForRemove.Count()} элементов?",
                "Внимание!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    foreach(var contract in contractsForRemove)
                    {
                        contract.employee_ID = null;
                    }

                    Context.Get().SaveChanges();
                    Context.Get().Employees.RemoveRange(employeesForRemove);
                    Context.Get().Persons.RemoveRange(personsForRemove);
                    Context.Get().Users.RemoveRange(usersForRemove);
                    Context.Get().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");
                    DGEmployee.ItemsSource = Context.Get().Employees.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CmbBoxEmployeeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxEmployee.SelectedIndex != -1)
            {
                TxtBoxEmployee.IsEnabled = true;
            }
        }

        private void TxtBoxSearchEmployee(object sender, TextChangedEventArgs e)
        {
            if (CmbBoxEmployee.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите фильтр поиска!");

                return;
            }

            var text = TxtBoxEmployee.Text.ToLower();

            SearchEmployee(text);
        }

        private void SearchEmployee(string text)
        {
            foreach (var employee in Context.Get().Employees)
            {
                ChangeRowVisible(employee, RowIsContainsText(employee, text));
            }
        }

        private bool RowIsContainsText(Employee employee, string text)
        {
            switch (CmbBoxEmployee.SelectedIndex)
            {
                case 0:
                    {
                        return employee.employee_ID.ToString().Contains(text);
                    }
                case 1:
                    {
                        return employee.Person.Birthdate_s.Contains(text);
                    }
                case 2:
                    {
                        return employee.Person.Number_passport.Contains(text);
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private void ChangeRowVisible(Employee row, bool isToShow)
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

        private DependencyObject GetItemContainer(Employee row)
        {
            var employeeForHide = Context.Get().Employees
                .Where(employee => employee.employee_ID == row.employee_ID)
                .ToList()
                .FirstOrDefault();
            var itemContainer = DGEmployee.ItemContainerGenerator.ContainerFromItem(employeeForHide);

            return itemContainer;
        }
    }
}
