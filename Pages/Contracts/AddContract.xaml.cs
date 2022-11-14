using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class AddContract : Page
    {
        public AddContract(Contract selectedContract)
        {
            InitializeComponent();

            selectedContract.Abonent = new Abonent();
            selectedContract.Abonent.Person = new Person();
            selectedContract.Abonent.User = new User();
            selectedContract.Status = true;
            selectedContract.employee_ID = 19; //Разграничение прав
            selectedContract.Abonent.User.Role = "Абонент";
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
            //CheckingEnteredData();//TODO
            StringBuilder errors = new StringBuilder();

            try
            {
                Context.Get().Contracts.Add(CurrentContract);
                Context.Get().SaveChanges();
                MessageBox.Show("Новый договор добавлен");

                var numberPhone = Context.Get().Numbers
                    .Where(number => number.Number_telephone == CurrentContract.Number_telephone)
                    .SingleOrDefault();

                NavigationService.Navigate(new ChooseRateForContract(numberPhone));
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    errors.AppendLine("Object: " + validationError.Entry.Entity.ToString());

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        errors.AppendLine(err.ErrorMessage + " ");
                    }
                }

                MessageBox.Show(errors.ToString());
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