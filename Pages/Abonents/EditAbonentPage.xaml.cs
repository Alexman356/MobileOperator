using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class EditAbonentPage : Page //TODO На этой странице пофиксить емайл и дату рождения
    {
        private Abonent CurrentAbonent;

        public EditAbonentPage(Abonent selectedAbonent)
        {
            InitializeComponent();
            DataContext = selectedAbonent;
            CurrentAbonent = selectedAbonent;
        }

        private void BtnBackPage_Click(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new AbonentsPage());
        }

        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            CheckingEnteredData();

            try
            {
                if (CurrentAbonent.abonent_ID != 0)
                {
                    Context.Get().SaveChanges();
                    MessageBox.Show("Абонент отредактирован!");
                    NavigationService.Navigate(new AbonentsPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CheckingEnteredData()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CurrentAbonent.Person.Last_name))
            {
                errors.AppendLine("Incorrect last name value entered!");
            }
            if (string.IsNullOrWhiteSpace(CurrentAbonent.Person.First_name))
            {
                errors.AppendLine("Incorrect first name value entered!");
            }
            if (string.IsNullOrWhiteSpace(CurrentAbonent.Person.Middle_name))
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }
            if (string.IsNullOrWhiteSpace(CurrentAbonent.Person.Gender))
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }
            
            //if (string.IsNullOrWhiteSpace(currentSubscriber.Birthdate))//TODO
            //errors.AppendLine("DANGER Birthdate!");

            if (CurrentAbonent.Person.Series_passport.Length != 4)
            {
                errors.AppendLine("Incorrect value of the passport series has been entered!");
            }
            if (CurrentAbonent.Person.Number_passport.Length != 6)
            {
                errors.AppendLine("Incorrect value of the passport number has been entered!");
            }
            if (CurrentAbonent.Person.Address.Length > 300)
            {
                errors.AppendLine("Incorrect value of the address has been entered!");
            }
            //if (currentAbonent.Person.Email.Length > 100)
            //{
             //   errors.AppendLine("Incorrect value of the email has been entered!");
            //}

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }

    }
}