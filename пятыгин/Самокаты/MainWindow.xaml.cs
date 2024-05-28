using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Самокаты
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "private string connectionString = \"Data Source=(localdb)\\\\MSSQLLocalDB;Initial Catalog=самокаты;Integrated Security=True\";";
        public MainWindow()
        {
            InitializeComponent();
            LoadScooters();
        }

        private void LoadScooters()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT ScooterID, Name, Description, PriceHour, PriceDay FROM ElectricScooters", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var scooterList = new System.Collections.ObjectModel.ObservableCollection<Scooter>();
                    while (reader.Read())
                    {
                        scooterList.Add(new Scooter
                        {
                            ScooterID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            PriceHour = reader.GetDecimal(3),
                            PriceDay = reader.GetDecimal(4)
                        });
                    }
                    dgScooters.ItemsSource = scooterList;
                }
            }
        }

        private void OnAddUpdateScooter(object sender, RoutedEventArgs e)
        {
            if (IsUserAuthenticated())
            {
                // Обработка добавления или обновления самоката
            }
            else
            {
                MessageBox.Show("Please login to add or update a scooter.");
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
        }

        private bool IsUserAuthenticated()
        {
            // Здесь должен быть код для проверки авторизации пользователя
            return false; // По умолчанию пользователь не авторизован
        }
    }

    public class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            // Здесь должен быть код для формы авторизации
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }

    public class Scooter
    {
        public int ScooterID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceHour { get; set; }
        public decimal PriceDay { get; set; }
    }
}
