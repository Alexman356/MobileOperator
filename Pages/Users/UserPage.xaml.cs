using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MobileOperator
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            DGUser.ItemsSource = Context.Get().Users.ToList();
        }

        private void BtnEditUserClick(object sender, RoutedEventArgs e)
        {
            if (DGUser.SelectedItem != null)
            {
                NavigationService.Navigate(new EditUserPage((User)DGUser.SelectedItem));
            }
            else
            {
                MessageBox.Show("Выберите пользователя");
            }
        }

        private void CmbBoxUserSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBoxUser.SelectedIndex != -1)
            {
                TxtBoxUser.IsEnabled = true;
            }
        }

        private void TxtBoxSearchUser(object sender, TextChangedEventArgs e)
        {
            var text = TxtBoxUser.Text.ToLower();

            SearchUser(text);
        }

        private void SearchUser(string text)
        {
            foreach (var user in Context.Get().Users)
            {
                ChangeRowVisible(user, RowIsContainsText(user, text));
            }
        }

        private bool RowIsContainsText(User user, string text)
        {
            switch (CmbBoxUser.SelectedIndex)
            {
                case 0:
                {
                    return user.Login.Contains(text);
                }

                default:
                    return false;
            }
        }

        private void ChangeRowVisible(User row, bool isToShow)
        {
            var itemContainer = GetItemContainer(row);

            if (!(itemContainer is DataGridRow dataGridRow))
            {
                return;
            }

            switch (isToShow)
            {
                case true:
                    dataGridRow.Visibility = Visibility.Visible;
                    break;
                case false:
                    dataGridRow.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private DependencyObject GetItemContainer(User row)
        {
            var userForHide = Context.Get().Users
                .Where(login => login.Login == row.Login)
                .ToList()
                .FirstOrDefault();
            var itemContainer = DGUser.ItemContainerGenerator
                .ContainerFromItem(userForHide);

            return itemContainer;
        }
    }
}
