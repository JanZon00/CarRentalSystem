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
                StackPanel emptyPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Label label = new Label
                {
                    Content = "Nie wypożyczono żadnego pojazdu.\n",
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(10, 0, 0, 0)
                };
                emptyPanel.Children.Add(label);
                RentedCarsStackPanel.Children.Add(emptyPanel);
            }
            else
            {
                for (int i = 0; i < rentedCars.Length; i++)
                {
                    Grid carPanel = AddCarToUI(rentedCars[i], i + 1);
                    RentedCarsStackPanel.Children.Add(carPanel);
                }
            }

            string[][] cars = dbQueries.GetAvailableCars();
            if (cars.Length == 0)
            {
                StackPanel emptyPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Label label = new Label
                {
                    Content = "Żaden pojazd nie jest obecnie dostępny.\n",
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(10, 0, 0, 0)
                };
                emptyPanel.Children.Add(label);
                CarsStackPanel.Children.Add(emptyPanel);
            }
            else
            {
                for (int i = 0; i < cars.Length; i++)
                {
                    Grid carPanel = AddCarToUI(cars[i], i + 1, true);
                    CarsStackPanel.Children.Add(carPanel);
                }
            }
            
            string[][] unavailableCars = dbQueries.GetUnavailableCars();
            if (unavailableCars.Length == 0)
            {
                StackPanel emptyPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Label label = new Label
                {
                    Content = "Wszystkie pojazdy są dostępne.\n",
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(10, 0, 0, 0)
                };
                emptyPanel.Children.Add(label);
                UnavailableCarsStackPanel.Children.Add(emptyPanel);
            }
            else
            {
                for (int i = 0; i < unavailableCars.Length; i++)
                {
                    Grid carPanel = AddCarToUI(unavailableCars[i], i + 1);
                    UnavailableCarsStackPanel.Children.Add(carPanel);
                }
            }
        }

        private Grid AddCarToUI(string[] car, int index, bool button = false)
        {
            Grid carGrid = new Grid
            {
                Margin = new Thickness(5),
                Background = new SolidColorBrush(Colors.LightGray)
            };

            ColumnDefinition column1 = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };

            ColumnDefinition column2 = new ColumnDefinition
            {
                Width = GridLength.Auto
            };

            carGrid.ColumnDefinitions.Add(column1);
            carGrid.ColumnDefinitions.Add(column2);

            StackPanel carPanel1 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.Transparent),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            StackPanel carPanel2 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.Transparent),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            Label indexLabel = new Label
            {
                Content = index.ToString() + ".",
                Foreground = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(6),
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            carPanel1.Children.Add(indexLabel);

            for (int i = 1; i < car.Length; i++)
            {
                Label label = new Label
                {
                    Content = car[i],
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(6),
                    FontSize = 12,
                    FontWeight = FontWeights.Bold
                };
                carPanel1.Children.Add(label);
            }

            if (button)
            {
                Button rentButton = new Button
                {
                    Content = "Wynajmij",
                    Tag = car[0],
                    Margin = new Thickness(6),
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Width = 100,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF163EBB")),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF163EBB")),
                    Foreground = new SolidColorBrush(Colors.White)
                };
                rentButton.Click += RentButton_Click;
                carPanel2.Children.Add(rentButton);
            }

            Grid.SetColumn(carPanel1, 0);
            Grid.SetColumn(carPanel2, 1);

            carGrid.Children.Add(carPanel1);
            carGrid.Children.Add(carPanel2);

            Border carBorder = new Border
            {
                BorderThickness = new Thickness(2),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Child = carGrid,
                Margin = new Thickness(5)
            };

            Grid outerPanel = new Grid();
            outerPanel.Children.Add(carBorder);

            return outerPanel;
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string tagValue = clickedButton.Tag.ToString();
            if (int.TryParse(tagValue, out int id))
            {
                RentalDatePicker rdp = new RentalDatePicker(this, id);
                rdp.Show();
            }
        }

        public void RentHandler(int carId, DateTime returnDate)
        {
            DatabaseQueries dbQueries = new DatabaseQueries();
            bool success = dbQueries.RentCar(App.UserId, carId, returnDate);

            if (success)
            {
                MessageBox.Show("Samochód został wypożyczony!");
                UnavailableCarsStackPanel.Children.Clear();
                CarsStackPanel.Children.Clear();
                RentedCarsStackPanel.Children.Clear();
                LoadCars();
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
