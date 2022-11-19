using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MobileOperator
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridTopRowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void BtnHideClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void BtnAuthClick(object sender, RoutedEventArgs e)
        {
            var login = TxtBoxLogin.Text;
            var pass = TxtBoxPass.Password;

            IQueryable<User> identifiableUser = Context.Get().Users
                .Where(user => user.Login == login);

            if (!identifiableUser.Any())
            {
                MessageBox.Show("Invalid login entered!");

                return;
            }

            var identifiedUser = identifiableUser.Single();
            var encryptor = new Encryptor();
            var enteredPassHash = encryptor.PasswordEncrypt(pass, identifiedUser.PasswordSalt);

            if (enteredPassHash != identifiedUser.PasswordHash)
            {
                MessageBox.Show("Invalid pass entered!");

                return;
            }

            TxtBoxLogin.Text = "";
            TxtBoxPass.Password = "";

            AuthorizeUser(identifiedUser);
        }

        private void AuthorizeUser(User authenticatedUser)
        {
            switch (authenticatedUser.Role)
            {
                case "Администратор":
                {
                    InitPagesForAdmin(authenticatedUser);

                    break;
                }

                case "Оператор":
                {
                    InitPagesForOperator(authenticatedUser);

                    break;
                }

                case "Абонент":
                {
                    InitPagesForAbonent(authenticatedUser);

                    break;
                }

                default:
                {
                    MessageBox.Show("Please contact your administrator with this error");

                    return;
                }
            }
        }

        private void InitPagesForAbonent(User authenticatedUser)
        {
            AuthorizeAbonent(authenticatedUser);

            var pages = new PagesModel
            {
                Contracts = new ContractsPage(),
                Rates = new RatesPage(),
                Profile = new ProfilePage(),
                LoginWindow = this,
            };
            pages.Rates.GridRatePage.Visibility = Visibility.Collapsed;
            pages.Rates.RowDefinitionButton.Height = new GridLength(1.0, GridUnitType.Star);
            pages.Contracts.BdrDelContract.Visibility = Visibility.Collapsed;
            pages.Contracts.BdrAddNumber.Visibility = Visibility.Collapsed;
            pages.Contracts.BdrAddContract.Visibility = Visibility.Collapsed;
            var mainWindow = new MainWindow(pages);
            mainWindow.BtnAbonents.Visibility = Visibility.Collapsed;
            mainWindow.BtnEmployees.Visibility = Visibility.Collapsed;
            mainWindow.BtnUsers.Visibility = Visibility.Collapsed;
            Hide();
            mainWindow.Show();
        }

        private void InitPagesForOperator(User authenticatedUser)
        {
            AuthorizeEmployee(authenticatedUser);
            var pages = new PagesModel
            {
                Abonents = new AbonentsPage(),
                Contracts = new ContractsPage(),
                Rates = new RatesPage(),
                Profile = new ProfilePage(),
                LoginWindow = this,
            };
            pages.Rates.GridRatePage.Visibility = Visibility.Collapsed;
            pages.Rates.RowDefinitionButton.Height = new GridLength(1.0, GridUnitType.Star);
            var mainWindow = new MainWindow(pages);
            mainWindow.BtnEmployees.Visibility = Visibility.Collapsed;
            mainWindow.BtnUsers.Visibility = Visibility.Collapsed;
            Hide();
            mainWindow.Show();
        }

        private void InitPagesForAdmin(User authenticatedUser)
        {
            AuthorizeEmployee(authenticatedUser);
            var pages = new PagesModel
            {
                Abonents = new AbonentsPage(),
                Contracts = new ContractsPage(),
                Employees = new EmployeesPage(),
                Rates = new RatesPage(),
                User = new UserPage(),
                Profile = new ProfilePage(),
                LoginWindow = this,
            };
            var mainWindow = new MainWindow(pages);
            Hide();
            mainWindow.Show();
        }

        private void AuthorizeEmployee(User authenticatedUser)
        {
            var authenticatedEmployee = Context.Get().Employees
                .Single(employee => employee.user_login == authenticatedUser.Login);
            var authenticatedPerson = Context.Get().Persons
                .Single(person => person.person_ID == authenticatedEmployee.person_ID);
            EmployeeIdentity.employee_ID = authenticatedEmployee.employee_ID;
            EmployeeIdentity.Salary = authenticatedEmployee.Salary;
            EmployeeIdentity.Post = authenticatedEmployee.Post;

            SetUserIdentity(authenticatedPerson, authenticatedUser);
        }

        private void AuthorizeAbonent(User authenticatedUser)
        {
            var authenticatedAbonent = Context.Get().Abonents
                .Single(abonent => abonent.user_login == authenticatedUser.Login);
            var authenticatedPerson = Context.Get().Persons
                .Single(person => person.person_ID == authenticatedAbonent.person_ID);

            AbonentIdentity.abonent_ID = authenticatedAbonent.abonent_ID;
            SetUserIdentity(authenticatedPerson, authenticatedUser);
        }

        private static void SetUserIdentity(Person person, User authenticatedUser)
        {
            UserIdentity.Login = authenticatedUser.Login;
            UserIdentity.Role = authenticatedUser.Role;
            UserIdentity.PasswordSalt = authenticatedUser.PasswordSalt;
            UserIdentity.person_ID = person.person_ID;
            UserIdentity.Last_name = person.Last_name;
            UserIdentity.First_name = person.First_name;
            UserIdentity.Middle_name = person.Middle_name;
            UserIdentity.Birthdate = person.Birthdate;
            UserIdentity.Gender = person.Gender;
            UserIdentity.Series_passport = person.Series_passport;
            UserIdentity.Number_passport = person.Number_passport;
            UserIdentity.Address = person.Address;
            UserIdentity.Email = person.Email;
        }
    }
}
