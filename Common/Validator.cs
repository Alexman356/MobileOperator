using System.Text;
using System.Windows;

namespace MobileOperator
{
    public class Validator
    {
        public bool IsValidPersonData(Person person)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(person.Last_name)
                || !IsValidLetters(person.Last_name))
            {
                errors.AppendLine("Incorrect last name value entered!");
            }

            if (string.IsNullOrWhiteSpace(person.First_name)
                || !IsValidLetters(person.First_name))
            {
                errors.AppendLine("Incorrect first name value entered!");
            }

            if ((!string.IsNullOrWhiteSpace(person.Middle_name) && !IsValidLetters(person.Middle_name))
                || person.Middle_name.Length > 50)
            {
                errors.AppendLine("Incorrect middle name value entered!");
            }

            if (person.Series_passport.Length != 4)
            {
                errors.AppendLine("The passport series must consist of 4 digits!");
            }

            if (!IsValidDigit(person.Series_passport))
            {
                errors.AppendLine("There can be no letters in the passport series!");
            }

            if (person.Number_passport.Length != 6)
            {
                errors.AppendLine("Incorrect value of the passport number has been entered!");
            }

            if (!IsValidDigit(person.Number_passport))
            {
                errors.AppendLine("There can be no letters in the passport number!");
            }

            if (string.IsNullOrWhiteSpace(person.Address)
                || person.Address.Length > 300)
            {
                errors.AppendLine("Incorrect value of the address has been entered!");
            }

            if (person.Email?.Length > 100)
            {
                errors.AppendLine("Incorrect value of the email has been entered!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());

                return false;
            }

            return true;
        }

        public bool IsValidRateData(Rate rate)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(rate.Name_rate) || rate.Name_rate.Length > 100)
            {
                errors.AppendLine("Incorrect name rate value entered!");
            }
            if (rate.Cost > 32767 || !IsValidDigit(rate.Cost.ToString()))
            {
                errors.AppendLine("Incorrect cost value entered!");
            }
            if (rate.Internet > 32767 || !IsValidDigit(rate.Internet.ToString()))
            {
                errors.AppendLine("Incorrect internet value entered!");
            }
            if (rate.Minutes > 32767 || !IsValidDigit(rate.Minutes.ToString()))
            {
                errors.AppendLine("Incorrect minutes value entered!");
            }
            if (rate.SMS > 32767 || !IsValidDigit(rate.SMS.ToString()))
            {
                errors.AppendLine("Incorrect SMS value entered!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());

                return false;
            }

            return true;
        }

        public bool IsValidEmployeeData(Employee employee)
        {
            StringBuilder errors = new StringBuilder();

            if (!IsValidDigit(employee.Salary.ToString()))
            {
                errors.AppendLine("Incorrect value of the salary has been entered!");
            }

            if (employee.User.Login.Length > 50)
            {
                errors.AppendLine("Incorrect value of the login has been entered!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());

                return false;
            }

            return true;
        }

        private bool IsValidDigit(string value)
        {
            foreach (var symbol in value)
            {
                if (!char.IsNumber(symbol))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidLetters(string value)
        {
            foreach (var symbol in value)
            {
                if (!char.IsLetter(symbol))
                {
                    return false;
                }
            }

            return true;
        }
    }
}