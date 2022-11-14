using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class EditUserPage : Page
    {
        private User CurrentUser;
        public EditUserPage(User selectedUser)
        {
            InitializeComponent();
            CurrentUser = selectedUser;
            DataContext = selectedUser;
        }
        
        private void BtnSaveUserClick(object sender, RoutedEventArgs e)
        {
            CheckingEnteredData();
            
            try
            {
                Context.Get().SaveChanges();
                MessageBox.Show("Пользователь отредактирован!");
                NavigationService.Navigate(new UserPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBackUserClick(object sender, RoutedEventArgs e)
        {
            Context.Set(null);
            NavigationService.Navigate(new UserPage());
        }

        private void CheckingEnteredData()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CurrentUser.Login))
            {
                errors.AppendLine("Incorrect login value entered!");//TODO Fix
            }

            if (CurrentUser.Role != "Администратор" ||
                CurrentUser.Role != "Оператор" ||
                CurrentUser.Role != "Абонент")//TODO Fix
            {
                errors.AppendLine("Incorrect value entered!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }
    }
}