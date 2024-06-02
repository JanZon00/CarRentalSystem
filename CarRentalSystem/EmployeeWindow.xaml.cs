using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CarRentalSystem
{
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
            HelloLabel.Content = "Witaj " + App.UserFullName + "!";
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            string brand = BrandTextBox.Text;
            string model = ModelTextBox.Text;
            int productionYear;
            string registrationNumber = RegistrationNumberTextBox.Text;
            string color = ColorTextBox.Text;
            string carType = CarTypeTextBox.Text;
            decimal pricePerDay;

            if (int.TryParse(ProductionYearTextBox.Text, out productionYear) && decimal.TryParse(PricePerDayTextBox.Text, out pricePerDay))
            {
                DatabaseQueries dbQueries = new DatabaseQueries();
                dbQueries.AddCar(brand, model, productionYear, registrationNumber, color, carType, pricePerDay);
                MessageBox.Show("Samochód został dodany pomyślnie.");
            }
            else
            {
                MessageBox.Show("Błąd w danych. Sprawdź wprowadzone wartości.");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == GetPlaceholderText(textBox.Name))
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(SystemColors.ControlTextColor);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = GetPlaceholderText(textBox.Name);
                textBox.Foreground = new SolidColorBrush(SystemColors.GrayTextColor);
            }
        }

        private string GetPlaceholderText(string textBoxName)
        {
            switch (textBoxName)
            {
                case "BrandTextBox": return "Marka";
                case "ModelTextBox": return "Model";
                case "ProductionYearTextBox": return "Rok produkcji";
                case "RegistrationNumberTextBox": return "Numer rejestracyjny";
                case "ColorTextBox": return "Kolor";
                case "CarTypeTextBox": return "Typ samochodu";
                case "PricePerDayTextBox": return "Cena za dzień";
                default: return "";
            }
        }
    }
}
