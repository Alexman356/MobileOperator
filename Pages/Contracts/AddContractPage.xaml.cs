using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class AddContractPage : Page
    {
        public AddContractPage(ContractsPage contractsPage, Contract selectedContract)
        {
            InitializeComponent();

            selectedContract.Abonent = new Abonent();
            selectedContract.Abonent.Person = new Person();
            selectedContract.Abonent.User = new User();
            selectedContract.Status = true;
            selectedContract.employee_ID = EmployeeIdentity.employee_ID;
            selectedContract.Abonent.User.Role = "Абонент";
            selectedContract.Date = DateTime.Now;

            ContractsPage = contractsPage;
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

        private ContractsPage ContractsPage { get; }

        private Contract CurrentContract { get; }

        private void BtnBackPageClick(object sender, RoutedEventArgs e)
        {
            GoToPage(ContractsPage);
        }

        private void BtnSaveContractClick(object sender, RoutedEventArgs e)
        {
            if (!IsTextBoxNull()
                || !Validator.IsValidPersonData(CurrentContract.Abonent.Person)
                || !IsSelectIndexCmbBox()
                || Validator.LoginIsExist(TxtBoxLogin.Text))
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
                MessageBox.Show("New contract added!");

                var numberPhone = Context.Get().Numbers
                    .SingleOrDefault(number => number.Number_telephone == CurrentContract.Number_telephone);

                GoToPage(new ChooseRateForContract(ContractsPage, numberPhone));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GoToPage(Page page)
        {
            NavigationService.Navigate(page);
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