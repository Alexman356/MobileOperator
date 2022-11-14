using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace MobileOperator
{
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void RdBtnSubscribers_Checked(object sender, RoutedEventArgs e)
        {
            DGRequests.ItemsSource = FillDataGridView(@"SELECT * FROM Abonents").DefaultView;
        }

        private void RdBtnRates_Checked(object sender, RoutedEventArgs e)
        {
            DGRequests.ItemsSource = FillDataGridView(@"SELECT * FROM Rates").DefaultView;
        }

        private void RdBtnStaff_Checked(object sender, RoutedEventArgs e)
        {
            DGRequests.ItemsSource = FillDataGridView(@"SELECT * FROM Employees").DefaultView;
        }

        private void RdBtnApplications_Checked(object sender, RoutedEventArgs e)
        {
            DGRequests.ItemsSource = FillDataGridView(@"SELECT Contracts.contract_ID,
            Employees.employee_ID, Persons.Last_name+' '+Persons.First_name+' '+Persons.Middle_name 
            AS 'Full name' FROM Contracts, Persons, Employees 
            WHERE Employees.employee_ID = Contracts.employee_ID AND Employees.person_ID = Persons.person_ID").DefaultView;
        }

        private void BtnDataRequest_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TxtBoxNameRate.Text))
            {
                MessageBox.Show("Обязательно укажите название тарифа!\nДопустим ввод первых символов.");
                return;
            }

            if (ChBoxRateProfit.IsChecked == true && String.IsNullOrEmpty(TxtBoxProfit.Text))
            {
                MessageBox.Show("Не указана прибыль в условии!", "Внимание");

                ChBoxRateProfit.IsChecked = false;

                return;
            }

            string sqlSelect = $"SELECT r.rate_ID, r.Name_rate, r.Cost, (COUNT(n.Number_telephone) * r.Cost) As Profit" +
                       $"FROM Contracts с, Rates r, Numbers n WHERE r.rate_ID = n.rate_ID AND r.Name_rate LIKE '%{TxtBoxNameRate.Text}%'" +
                       $"GROUP BY r.Rate_ID, r.Name_rate, r.Cost";

            if (ChBoxRateNumberSubscriber.IsChecked == true)
            {
                string tmp = ", COUNT(Applications.SubApplication_ID) As [Number of connected subscribers]";
                sqlSelect = $"SELECT r.Rate_ID, r.Name, r.Cost, (COUNT(Applications.SubApplication_ID)*r.Cost) As Profit{tmp} " +
                    $"FROM Applications, Rates r " +
                    $"WHERE r.Rate_ID = Applications.Rate_ID AND r.Name LIKE '%{TxtBoxNameRate.Text}%'" +
                    $"GROUP BY r.Rate_ID, r.Name, r.Cost";

                DGRequests2.ItemsSource = FillDataGridView(sqlSelect).DefaultView;
            }

            if (ChBoxDescendingProfit.IsChecked == false && ChBoxRateNumberSubscriber.IsChecked == false)
            {
                DGRequests2.ItemsSource = FillDataGridView(sqlSelect).DefaultView;
            }

            if (ChBoxRateProfit.IsChecked == true)
            {
                sqlSelect += $" HAVING (COUNT(Applications.SubApplication_ID)*r.Cost)>{int.Parse(TxtBoxProfit.Text)}";
                DGRequests2.ItemsSource = FillDataGridView(sqlSelect).DefaultView;
            }    

            if (ChBoxDescendingProfit.IsChecked == true)
            {
                sqlSelect += " ORDER BY (COUNT(Applications.SubApplication_ID)) DESC";
                DGRequests2.ItemsSource = FillDataGridView(sqlSelect).DefaultView;
            }

        }

        private void BtnDataRequest2_Click(object sender, RoutedEventArgs e)
        {
            if (RdBtnAdd.IsChecked == true)
            {
                InsertDish();
            }
            else if (RdBtnChange.IsChecked == true)
            {
                UpdateDish();
            }
            else if (RdBtnDelete.IsChecked == true)
            {
                DeleteDish();
            }
            else
            {
                MessageBox.Show("Вы не выбрали действие", "Внимание");
            }
        }
        private void InsertDish()
        {
            Check();

            string sqlValues = @"INSERT INTO Rates (Rate_ID, Name, Cost, Internet, Minutes, SMS) VALUES" +
                $"('{Guid.NewGuid()}','{TxtBoxRateDataName.Text}'," +
                $"'{TxtBoxRateDataCost.Text}','{TxtBoxRateDataInternet.Text}'," +
                $"'{TxtBoxRateDataMinutes.Text}','{TxtBoxRateDataSMS.Text}')";

            DGRequests4.ItemsSource = FillDataGridView(sqlValues).DefaultView;
        }

        private void DeleteDish()
        {
            if (String.IsNullOrEmpty(TxtBoxIdRate.Text))
            {
                MessageBox.Show("Обязательно укажите GUID тарифа, для которого будете менять данные", "Внимание");
                return;
            }

            string sqlValues = $@"DELETE FROM Rates WHERE Rate_ID = '{TxtBoxIdRate.Text}'";

            DGRequests4.ItemsSource = FillDataGridView(sqlValues).DefaultView;
        }
        private void UpdateDish()
        {

            if (String.IsNullOrEmpty(TxtBoxIdRate.Text))
            {
                MessageBox.Show("Обязательно укажите GUID тарифа, для которого будете менять данные", "Внимание");
                return;
            }

            if (!Guid.TryParse(TxtBoxIdRate.Text, out Guid guid))
            {
                MessageBox.Show("Некорректное значение GUID тарифа!", "Внимание");
                return;
            }

            Check();

            string sqlValues = $"UPDATE Rates SET " +
                $"Name = '{TxtBoxRateDataName.Text}', Cost = '{TxtBoxRateDataCost.Text}'," +
                $"Internet ='{TxtBoxRateDataInternet.Text}', Minutes = '{TxtBoxRateDataMinutes.Text}'," +
                $"SMS = '{TxtBoxRateDataSMS.Text}' WHERE Rate_ID = '{TxtBoxIdRate.Text}'";

            DGRequests4.ItemsSource = FillDataGridView(sqlValues).DefaultView;
        }

        private void BtnColQuery_Click(object sender, RoutedEventArgs e)
        {
            string sqlSelect = "";

            if (String.IsNullOrEmpty(TxtBoxSubscriberId.Text))
            {
                MessageBox.Show("Обязательно укажите GUID абонента!", "Внимание");
                return;
            }

            if (RbCol.IsChecked == true)
            {
                sqlSelect = $@"SELECT 
                Subscriber.Subscriber_ID,
                Subscriber.Surname+' '+Subscriber.Name+' '+Subscriber.Patronymic As Full_Name,
                Rates.Name AS Connected_rate,
                Staff.Name AS Employee,
                DateOfApplication As Connection_date
                FROM Subscriber, Applications
                INNER JOIN Rates ON Rates.Rate_ID = Applications.Rate_ID
                INNER JOIN Staff ON Staff.Employee_ID = Applications.Employee_ID
                WHERE Subscriber.Subscriber_ID='{TxtBoxSubscriberId.Text}' AND
                Subscriber.Subscriber_ID=Applications.Subscriber_ID";

                DGRequests3.ItemsSource = FillDataGridView(sqlSelect).DefaultView;
            }
            else if (RbUncol.IsChecked == true)
            {
                sqlSelect = $@"SELECT 
                Subscriber.Subscriber_ID,
                Subscriber.Surname+' '+Subscriber.Name+' '+Subscriber.Patronymic As Full_Name,
                Rates.Name AS Name_rate
                FROM Subscriber, Applications
                INNER JOIN Rates ON Rates.Rate_ID = Applications.Rate_ID
                WHERE Subscriber.Subscriber_ID='{TxtBoxSubscriberId.Text}' AND
                Subscriber.Subscriber_ID=Applications.Subscriber_ID";

                DGRequests3.ItemsSource = FillDataGridView(sqlSelect).DefaultView;
            }
            else
            {
                MessageBox.Show("Не был выбран вид подзапроса!", "Ошибка");
            }
        }

        private void Check()
        {
            if (String.IsNullOrEmpty(TxtBoxRateDataName.Text))
            {
                MessageBox.Show("Некоректное значение названия тарифа!", "Внимание");
                return;
            }

            decimal cost = 0;

            if ((!String.IsNullOrEmpty(TxtBoxRateDataCost.Text)) &&
            (!decimal.TryParse(TxtBoxRateDataCost.Text, out cost)))
            {
                MessageBox.Show("Некоректное значение цены!", "Внимание");
                return;
            }

            decimal internet = 0;

            if ((!String.IsNullOrEmpty(TxtBoxRateDataInternet.Text)) &&
            (!decimal.TryParse(TxtBoxRateDataInternet.Text, out internet)))
            {
                MessageBox.Show("Некоректное значение размера пакета интернета!", "Внимание");
                return;
            }

            decimal minutes = 0;

            if ((!String.IsNullOrEmpty(TxtBoxRateDataMinutes.Text)) &&
            (!decimal.TryParse(TxtBoxRateDataMinutes.Text, out minutes)))
            {
                MessageBox.Show("Некоректное значение размера пакета минут!", "Внимание");
                return;
            }

            double sms = 0;
            if (!double.TryParse(TxtBoxRateDataSMS.Text, out sms))
            {
                MessageBox.Show("Некоректное значение размера пакета СМС!", "Внимание");
                return;
            }
        }


        private void BtnRateAll_Click(object sender, RoutedEventArgs e)
        {
            DGRequests4.ItemsSource = FillDataGridView(@"SELECT * FROM Rates").DefaultView;
        }

        DataTable FillDataGridView(string sqlSelect)
        {
            SqlConnection connection = new SqlConnection(@"data source=ALEX-PC\SQLSERVER;initial catalog=MobileOperator2022;integrated security=True;");
            SqlCommand command = connection.CreateCommand();

            command.CommandText = sqlSelect;

            if (TxtBoxNameRate.Text != "")
            {
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = TxtBoxNameRate.Text;
            }

            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = command;

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

    }
}