using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MobileOperator.Pages
{
    public partial class AddContract : Page
    {
        public AddContract(Contract selectedContract)
        {
            InitializeComponent();
            selectedContract.Number = new Number();
            selectedContract.Abonent = new Abonent();
            selectedContract.Employee = new Employee();
            selectedContract.Abonent.Person = new Person();
            selectedContract.Abonent.User = new User();
            selectedContract.Employee.employee_ID = 15;
            selectedContract.Abonent.User.Role = "Абонент";
            selectedContract.Abonent.User.PasswordSalt = "123";
            selectedContract.Date = DateTime.Now;
            CurrentContract = selectedContract;
            DataContext = selectedContract;
        }

        private Contract CurrentContract { get; set; }

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new ContractsPage());
        }

        private void BtnSaveContractClick(object sender, RoutedEventArgs e)
        {
            //CheckingEnteredData();

            try
            {
                Context.Get().Contracts.Add(CurrentContract);
                Context.Get().SaveChanges();
                MessageBox.Show("Новый договор добавлен");
                NavigationService.Navigate(new ContractsPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /*StringBuilder errors = new StringBuilder();

            if (currentApplication.Employee_ID == Guid.Empty)
                errors.AppendLine("DANGER Employee!");
            if (currentApplication.Rate_ID == Guid.Empty)
                errors.AppendLine("DANGER Rate!");
            if (currentApplication.Subscriber_ID == Guid.Empty)
                errors.AppendLine("DANGER Subscriber!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            currentApplication.SubApplication_ID = Guid.NewGuid();
            currentApplication.DateOfApplication = DateTime.Now;

            try
            {
                MobileOperatorEntities.GetContext().Applications.Add(currentApplication);
                MobileOperatorEntities.GetContext().SaveChanges();
                MessageBox.Show("Заявка добавлена!");
                NavigationService.Navigate(new Applications());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }*/
        }
    }
}
