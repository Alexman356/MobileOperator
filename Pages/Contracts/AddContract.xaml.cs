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
            selectedContract.employee_ID = EmployeeIdentity.employee_ID;
            selectedContract.Abonent.User.Role = "Абонент";
            selectedContract.Date = DateTime.Now;

            CurrentContract = selectedContract;
            DataContext = selectedContract;
            Validator = new Validator();

            IQueryable<string> numbersForHide = Context.Get().Contracts
               .Select(contract => contract.Number_telephone);

            IQueryable<Number> numberForShow = Context.Get().Numbers
                .Where(number => !numbersForHide.Contains(number.Number_telephone));

            DGNumbers.ItemsSource = numberForShow.ToList();
        }

        private Validator Validator { get; }

        private Contract CurrentContract { get; }

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new ContractsPage());
        }

        private void BtnSaveContractClick(object sender, RoutedEventArgs e)
        {
            if (!IsTextBoxNull()
                || !Validator.IsValidPersonData(CurrentContract.Abonent.Person)
                || !IsSelectIndexCmbBox())
            {
                return;
            }

            SetAbonentGenderFromCmb();

            if (DGNumbers.SelectedItem == null)
            {
                MessageBox.Show("The number telephone was not selected!");

                return;
            }

            var numberSelected = DGNumbers.SelectedItem as Number;
            CurrentContract.Number_telephone = numberSelected.Number_telephone;

            try
            {
                Context.Get().Contracts.Add(CurrentContract);
                Context.Get().SaveChanges();
                MessageBox.Show("Новый договор добавлен");

                var numberPhone = Context.Get().Numbers
                    .SingleOrDefault(number => number.Number_telephone == CurrentContract.Number_telephone);

                NavigationService.Navigate(new ChooseRateForContract(numberPhone));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool IsSelectIndexCmbBox()
        {
            if (CmbBoxGender.SelectedIndex == -1)
            {
                MessageBox.Show("The gender was not selected!");

                return false;
            }

            return true;
        }

        private void SetAbonentGenderFromCmb()
        {
            switch (CmbBoxGender.SelectedIndex)
            {
                case 0:
                    {
                        CurrentContract.Abonent.Person.Gender = "Мужской";

                        break;
                    }

                case 1:
                    {
                        CurrentContract.Abonent.Person.Gender = "Женский";

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
                || string.IsNullOrWhiteSpace(TxtBoxLogin.Text)
                || string.IsNullOrWhiteSpace(TxtBoxPass.Text))
            {
                MessageBox.Show("Not all fields are filled in!");

                return false;
            }

            return true;
        }
    }
}