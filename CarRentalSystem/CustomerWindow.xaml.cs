using System.Windows;
using System.Windows.Controls;
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
            string[][] cars = dbQueries.GetAllCars();

            foreach (string[] car in cars)
            {
                AddCarToUI(car);
            }
        }

        private void AddCarToUI(string[] car)
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

            CarsStackPanel.Children.Add(carPanel);
        }
    }
}
