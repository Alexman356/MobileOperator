using System;
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
                TitleNameEmployee.Text = "Add employee";
            }

            Validator = new Validator();
            CurrentEmployee = selectedEmployee;
            DataContext = selectedEmployee;

            InitCmb();
        }

        private Validator Validator { get; }

        private Employee CurrentEmployee { get; }

        private void BtnSaveEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (!IsTextBoxNull()
                || !Validator.IsValidPersonData(CurrentEmployee.Person)
                || !Validator.IsValidEmployeeData(CurrentEmployee)
                || !IsSelectIndexCmbBox())
            {
                return;
            }


            SetEmployeeDataFromCmb();

            try
            {
                if (CurrentEmployee.employee_ID == null)
                {
                    Context.Get().Employees.Add(CurrentEmployee);
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

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new EmployeesPage());
        }

        private bool IsSelectIndexCmbBox()
        {
            if (CmbBoxGender.SelectedIndex == -1)
            {
                MessageBox.Show("The gender was not selected!");

                return false;
            }

            if (CmbBoxPost.SelectedIndex == -1)
            {
                MessageBox.Show("The post was not selected!");

                return false;
            }

            return true;
        }

        private void SetEmployeeDataFromCmb()
        {
            switch (CmbBoxGender.SelectedIndex)
            {
                case 0:
                {
                    CurrentEmployee.Person.Gender = "Мужской";

                    break;
                }

                case 1:
                {
                    CurrentEmployee.Person.Gender = "Женский";

                    break;
                }
            }

            switch (CmbBoxPost.SelectedIndex)
            {
                case 0:
                {
                    CurrentEmployee.Post = "Онлайн консультант";
                    CurrentEmployee.User.Role = "Оператор";

                    break;
                }

                case 1:
                {
                    CurrentEmployee.Post = "Оператор";
                    CurrentEmployee.User.Role = "Оператор";

                    break;
                }

                case 2:
                {
                    CurrentEmployee.Post = "Менеджер";
                    CurrentEmployee.User.Role = "Оператор";

                    break;
                }

                case 3:
                {
                    CurrentEmployee.Post = "Администратор";
                    CurrentEmployee.User.Role = "Администратор";

                    break;
                }
            }
        }

        private bool IsTextBoxNull()
        {
            if (string.IsNullOrWhiteSpace(TxtBoxLastName.Text)
                || string.IsNullOrWhiteSpace(TxtBoxFirstName.Text)
                || string.IsNullOrWhiteSpace(TxtBoxBirthdate.Text)
                || string.IsNullOrWhiteSpace(TxtBoxSerPas.Text)
                || string.IsNullOrWhiteSpace(TxtBoxNumPas.Text)
                || string.IsNullOrWhiteSpace(TxtBoxAddress.Text)
                || string.IsNullOrWhiteSpace(TxtBoxSalary.Text)
                || string.IsNullOrWhiteSpace(TxtBoxLogin.Text)
                || string.IsNullOrWhiteSpace(TxtBoxPass.Text))
            {
                MessageBox.Show("Not all fields are filled in!");

                return false;
            }

            return true;
        }

        private void InitCmb()
        {
            if (CurrentEmployee.Person.Gender == "Мужской")
            {
                CmbBoxGender.SelectedIndex = 0;
            }

            if (CurrentEmployee.Person.Gender == "Женский")
            {
                CmbBoxGender.SelectedIndex = 1;
            }

            switch (CurrentEmployee.Post)
            {
                case "Онлайн консультант":
                    {
                        CmbBoxPost.SelectedIndex = 0;

                        break;
                    }

                case "Оператор":
                    {
                        CmbBoxPost.SelectedIndex = 1;

                        break;
                    }

                case "Менеджер":
                    {
                        CmbBoxPost.SelectedIndex = 2;

                        break;
                    }

                case "Администратор":
                    {
                        CmbBoxPost.SelectedIndex = 3;

                        break;
                    }
            }
        }
    }
}
