using System;
using System.Collections.Generic;
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
            GoToPage(new AddEditEmployeePage(this, new Employee()));
        }

        private void BtnEditEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (DGEmployee.SelectedItem != null)
            {
                GoToPage(new AddEditEmployeePage(this, (Employee)DGEmployee.SelectedItem));
            }
            else
            {
                MessageBox.Show("Select an employee to edit");
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

            IQueryable<Contract> contractsForRemove = Context.Get().Contracts
                .Where(contract => employeeIdsForRemove.Contains(contract.employee_ID));

            var dialogResult = MessageBox.Show(
                $"You definitely want to delete the following {employeesForRemove.Count()} elements?",
                "Attention!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var contract in contractsForRemove)
                    {
                        contract.employee_ID = null;
                    }

                    Context.Get().SaveChanges();
                    Context.Get().Employees.RemoveRange(employeesForRemove);
                    Context.Get().Persons.RemoveRange(personsForRemove);
                    Context.Get().Users.RemoveRange(usersForRemove);
                    Context.Get().SaveChanges();
                    MessageBox.Show("The data has been successfully deleted!");
                    DGEmployee.ItemsSource = Context.Get().Employees.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void GoToPage(Page employee)
        {
            NavigationService.Navigate(employee);
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
            var text = TxtBoxEmployee.Text.ToLower();

            SearchEmployee(text);
        }

        private void SearchEmployee(string text)
        {
            foreach (var employee in DGEmployee.ItemsSource as List<Employee>)
            {
                var rowIsContainsText = RowIsContainsText(employee, text);
                ChangeRowVisible(employee, rowIsContainsText);
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
            var itemContainer = DGEmployee.ItemContainerGenerator.ContainerFromItem(row);

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