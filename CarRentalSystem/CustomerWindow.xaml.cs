using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CarRentalSystem
{
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
            HelloLabel.Content = "Witaj " + App.UserFullName + "!";

            LoadCars();
        }

        private void LoadCars()
        {
            DatabaseQueries dbQueries = new DatabaseQueries();

            string[][] rentedCars = dbQueries.GetRentedCars(App.UserId);
            if (rentedCars.Length == 0 )
            {
                StackPanel emptyPanel = new StackPanel();
                emptyPanel.Orientation = Orientation.Horizontal;
                emptyPanel.Margin = new Thickness(0, 10, 0, 0);
                Label label = new Label();
                label.Content = "Nie wypożyczono żadnego auta.\n";
                label.Foreground = new SolidColorBrush(Colors.Black);
                label.Margin = new Thickness(10, 0, 0, 0);
                emptyPanel.Children.Add(label);
                RentedCarsStackPanel.Children.Add(emptyPanel);
            }
            else
            {
                foreach (string[] car in rentedCars)
                {
                    StackPanel carPanel = AddCarToUI(car);
                    RentedCarsStackPanel.Children.Add(carPanel);
                }
            }

            string[][] cars = dbQueries.GetAllCars();
            foreach (string[] car in cars)
            {
                StackPanel carPanel = AddCarToUI(car);
                CarsStackPanel.Children.Add(carPanel);
            }
        }

        private StackPanel AddCarToUI(string[] car)
        {
            StackPanel carPanel = new StackPanel();
            carPanel.Orientation = Orientation.Horizontal;
            carPanel.Margin = new Thickness(0, 10, 0, 0);

            foreach (string attribute in car)
            {
                Label label = new Label();
                label.Content = attribute;
                label.Foreground = new SolidColorBrush(Colors.Black);
                label.Margin = new Thickness(10, 0, 0, 0);
                carPanel.Children.Add(label);
            }

            return carPanel;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.UserId = -1;
            App.UserFullName = "";
            this.Close();
        }
    }
}
