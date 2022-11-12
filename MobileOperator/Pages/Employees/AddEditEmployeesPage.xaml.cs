using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MobileOperator.Pages
{
    public partial class AddEditEmployeePage : Page
    {
        public AddEditEmployeePage(Employee selectedEmployee)
        {
            InitializeComponent();
            selectedEmployee.Person = new Person();
            selectedEmployee.User = new User();
            selectedEmployee.User.Role = "Оператор";
            selectedEmployee.User.PasswordSalt = "123";
            СurrentEmployee = selectedEmployee;
            DataContext = selectedEmployee;
        }

        private Employee СurrentEmployee { get; set; }

        private void BtnBackPage_Click(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new EmployeesPage());
        }

        private void BtnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            CheckingEnteredData();

            try
            {
                if (СurrentEmployee.employee_ID == 0)
                {
                    Context.Get().Employees.Add(СurrentEmployee);
                    Context.Get().SaveChanges();
                    MessageBox.Show("Новый сотрудник добавлен");
                    NavigationService.Navigate(new EmployeesPage());
                }
                else
                {
                    Context.Get().SaveChanges();
                    MessageBox.Show("Сотрудник отредактирован!");
                    NavigationService.Navigate(new EmployeesPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckingEnteredData()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(СurrentEmployee.Person.Last_name))
            {
                errors.AppendLine("Incorrect last name value entered!");
            }
            if (string.IsNullOrWhiteSpace(СurrentEmployee.Person.First_name))
            {
                errors.AppendLine("Incorrect first name value entered!");
            }
            if (string.IsNullOrWhiteSpace(СurrentEmployee.Person.Middle_name))
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }
            if (string.IsNullOrWhiteSpace(СurrentEmployee.Person.Gender))
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }
            //if (string.IsNullOrWhiteSpace(currentSubscriber.Birthdate)) //TODO ДАВНЕШНЕЯ ПРОБЛЕМА
            //errors.AppendLine("DANGER Birthdate!");



            if (СurrentEmployee.Person.Series_passport.Length != 4)
            {
                errors.AppendLine("Incorrect value of the passport series has been entered!");
            }
            if (СurrentEmployee.Person.Number_passport.Length != 6)
            {
                errors.AppendLine("Incorrect value of the passport number has been entered!");
            }
            if (СurrentEmployee.Person.Number_passport.Length != 6)
            {
                errors.AppendLine("Incorrect value of the passport number has been entered!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }
    }
}
