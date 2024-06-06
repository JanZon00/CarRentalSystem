using System;
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
            if (rentedCars.Length == 0)
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
                for (int i = 0; i < rentedCars.Length; i++)
                {
                    StackPanel carPanel = AddCarToUI(rentedCars[i], i + 1);
                    RentedCarsStackPanel.Children.Add(carPanel);
                }
            }

            string[][] cars = dbQueries.GetAllCars();
            for (int i = 0; i < cars.Length; i++)
            {
                StackPanel carPanel = AddCarToUI(cars[i], i + 1);
                CarsStackPanel.Children.Add(carPanel);
            }
        }

        private StackPanel AddCarToUI(string[] car, int index)
        {
            StackPanel carPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 10),
                Background = new SolidColorBrush(Colors.LightGray)
            };

            Label indexLabel = new Label
            {
                Content = index.ToString() + ".",
                Foreground = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(10),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            carPanel.Children.Add(indexLabel);

            foreach (string attribute in car)
            {
                Label label = new Label
                {
                    Content = attribute,
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(10),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold
                };
                carPanel.Children.Add(label);
            }

            Border carBorder = new Border
            {
                BorderThickness = new Thickness(2),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Child = carPanel,
                Margin = new Thickness(5)
            };

            StackPanel outerPanel = new StackPanel();
            outerPanel.Children.Add(carBorder);

            return outerPanel;
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CarNumberTextBox.Text, out int carId) && int.TryParse(RentalPeriodTextBox.Text, out int rentalDays))
            {
                DateTime returnDate = DateTime.Now.AddDays(rentalDays);

                DatabaseQueries dbQueries = new DatabaseQueries();
                bool success = dbQueries.RentCar(App.UserId, carId, returnDate);

                if (success)
                {
                    MessageBox.Show("Samochód został wypożyczony!");
                    CarsStackPanel.Children.Clear();
                    RentedCarsStackPanel.Children.Clear();
                    LoadCars();
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas wypożyczania samochodu.");
                }
            }
            else
            {
                MessageBox.Show("Podaj poprawne wartości dla numeru samochodu i okresu wynajęcia.");
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.UserId = -1;
            App.UserFullName = "";
            this.Close();
        }
    }
}
