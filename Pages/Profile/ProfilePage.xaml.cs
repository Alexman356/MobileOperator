using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            InitProfile();
        }

        private void BtnChangePassClick(object sender, RoutedEventArgs e)
        {
            if (TbPass.Visibility == Visibility.Collapsed)
            {
                TbPass.Visibility = Visibility.Visible;
                PassBox.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrWhiteSpace(PassBox.Password))
            {
                MessageBox.Show("The password was not entered!");
                return;
            }

            var dialogResult = MessageBox.Show(
                $"Do you really want to change the password for your account?",
                "Attention!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (dialogResult == MessageBoxResult.Yes)
            {
                User loginCur = Context.Get().Users
                    .Single(user => user.Login == UserIdentity.Login);
                loginCur.Password = PassBox.Password;
                Context.Get().SaveChanges();

                MessageBox.Show("Password changed successfully!");
                TbPass.Visibility = Visibility.Collapsed;
                PassBox.Visibility = Visibility.Collapsed;
                PassBox.Password = "";
            }
        }

        private void InitProfile()
        {
            LastNameTb.Text = UserIdentity.Last_name;
            FirstNameTb.Text = UserIdentity.First_name;
            MiddleNameTb.Text = UserIdentity.Middle_name;
            BirthdateTb.Text = UserIdentity.Birthdate.ToShortDateString();
            GenderTb.Text = UserIdentity.Gender;
            PasSerTb.Text = UserIdentity.Series_passport;
            PasNumTb.Text = UserIdentity.Number_passport;
            AddressTb.Text = UserIdentity.Address;
            EmailTb.Text = UserIdentity.Email;
            LoginTb.Text = UserIdentity.Login;

            if (UserIdentity.Role != "Абонент")
            {
                SalaryTb.Text = EmployeeIdentity.Salary.ToString();
                PostTb.Text = EmployeeIdentity.Post;
                TbSalary.Visibility = Visibility.Visible;
                TbPost.Visibility = Visibility.Visible;

            }
        }
    }
}