using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class EditUserPage : Page
    {
        public EditUserPage(UserPage userPage, User selectedUser)
        {
            InitializeComponent();
            CurrentUser = selectedUser;
            DataContext = selectedUser;
            UserPage = userPage;

            if (CurrentUser.Role == "Оператор")
            {
                CmbBoxRole.SelectedIndex = 0;
            }

            if (CurrentUser.Role == "Администратор")
            {
                CmbBoxRole.SelectedIndex = 1;
            }

            if (CurrentUser.Role == "Абонент")
            {
                CmbBoxRole.SelectedIndex = 2;
            }
        }

        private Validator Validator { get; }

        private User CurrentUser { get; }

        private UserPage UserPage { get; }

        private void BtnSaveUserClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.Login))
            {
                MessageBox.Show("Incorrect login value entered!");

                return;
            }

            if (Validator.LoginIsExist(TxtBoxLogin.Text))
            {
                MessageBox.Show("The user with the entered username already exists!");

                return;
            }

            switch (CmbBoxRole.SelectedIndex)
            {
                case 0:
                {
                    CurrentUser.Role = "Оператор";

                    break;
                }

                case 1:
                {
                    CurrentUser.Role = "Администратор";

                    break;
                }

                case 2:
                {
                    CurrentUser.Role = "Абонент";

                    break;
                }
            }

            try
            {
                Context.Get().SaveChanges();
                MessageBox.Show("The user has been edited!");
                GoToPage(UserPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBackUserClick(object sender, RoutedEventArgs e)
        {
            GoToPage(UserPage);
        }

        private void GoToPage(Page page)
        {
            NavigationService.Navigate(page);
        }
    }
}