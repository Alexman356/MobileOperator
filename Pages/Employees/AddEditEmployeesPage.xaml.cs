using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class AddEditEmployeePage : Page
    {
        public AddEditEmployeePage(Employee selectedEmployee)
        {
            InitializeComponent();

            if (selectedEmployee.Person == null)
            {
                selectedEmployee.Person = new Person();
                selectedEmployee.User = new User();
                TitleNameEmployee.Text = "Add rate";
                selectedEmployee.User.Role = "Оператор";
            }
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
                if (СurrentEmployee.employee_ID == null)
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

        private string date;

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
            if (СurrentEmployee.Person.Middle_name.Length > 50)
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }
            if (string.IsNullOrWhiteSpace(СurrentEmployee.Person.Gender))
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }
            //if (string.IsNullOrWhiteSpace(currentSubscriber.Birthdate)) //TODO ДАВНЕШНЕЯ ПРОБЛЕМА
            //errors.AppendLine("DANGER Birthdate!");

            DateTime dDate;
            if (DateTime.TryParse(date, out dDate))
            {
                String.Format("{0:dd.MM.yyyy}", dDate);

                СurrentEmployee.Person.Birthdate_s = date;
            }
            else
            {
                Console.WriteLine("Invalid");
            }

            if (СurrentEmployee.Person.Series_passport.Length != 4)
            {
                errors.AppendLine("Incorrect value of the passport series has been entered!");
            }
            if (СurrentEmployee.Person.Number_passport.Length != 6)
            {
                errors.AppendLine("Incorrect value of the passport number has been entered!");
            }
            if (СurrentEmployee.Person.Address.Length > 300)
            {
                errors.AppendLine("Incorrect value of the address has been entered!");
            }
            if (СurrentEmployee.Person.Email?.Length > 100)
            {
                errors.AppendLine("Incorrect value of the email has been entered!");
            }
            if (СurrentEmployee.User.Login.Length > 50)
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
