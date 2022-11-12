using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MobileOperator
{
    public partial class EditAbonentPage : Page
    {
        private Abonent currentAbonent = new Abonent();

        private string date;

        public EditAbonentPage(Abonent selectedAbonent)
        {
            InitializeComponent();

            DataContext = selectedAbonent;

            currentAbonent = selectedAbonent;

            //date = currentAbonent.Person.Birthdate_s.ToString();

            TxtBoxBirhdate.Text = date;
        }

        private void BtnBackPage_Click(object sender, RoutedEventArgs e)
        {
            Context.Set(null);

            NavigationService.Navigate(new AbonentsPage());
        }

        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            

            CheckingEnteredData();

            //TxtBoxBirhdate.Text = date;

            try
            {
                if (currentAbonent.abonent_ID != 0)
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

            if (string.IsNullOrWhiteSpace(currentAbonent.Person.Last_name))
            {
                errors.AppendLine("Incorrect last name value entered!");
            }
            if (string.IsNullOrWhiteSpace(currentAbonent.Person.First_name))
            {
                errors.AppendLine("Incorrect first name value entered!");
            }
            if (string.IsNullOrWhiteSpace(currentAbonent.Person.Middle_name))
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }
            if (string.IsNullOrWhiteSpace(currentAbonent.Person.Gender))
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }

            DateTime dDate;

            if (DateTime.TryParse(date, out dDate))
            {
                String.Format("{0:dd.MM.yyyy}", dDate);

                currentAbonent.Person.Birthdate_s = date;
            }
            else
            {
                Console.WriteLine("Invalid");
            }
            //if (string.IsNullOrWhiteSpace(currentSubscriber.Birthdate))//TODO
            //errors.AppendLine("DANGER Birthdate!");

            if (currentAbonent.Person.Series_passport.Length != 4)
            {
                errors.AppendLine("Incorrect value of the passport series has been entered!");
            }
            if (currentAbonent.Person.Number_passport.Length != 6)
            {
                errors.AppendLine("Incorrect value of the passport number has been entered!");
            }
            if (currentAbonent.Person.Address.Length > 300)
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