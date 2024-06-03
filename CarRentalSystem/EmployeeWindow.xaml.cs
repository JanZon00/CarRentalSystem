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
            DatabaseQueries dbq = new DatabaseQueries();
            if (dbq.GetEmployeePosition(App.UserId) == "mechanic")
            {
                addNewCarExpander.IsEnabled = false;
                addNewEmployeeExpander.IsEnabled = false;
            }
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

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && passwordBox.Password == GetPlaceholderText(passwordBox.Name))
            {
                passwordBox.Password = "";
                passwordBox.Foreground = new SolidColorBrush(SystemColors.ControlTextColor);
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null && string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Password = GetPlaceholderText(passwordBox.Name);
                passwordBox.Foreground = new SolidColorBrush(SystemColors.GrayTextColor);
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
                case "FirstNameTextBox": return "Imię";
                case "LastNameTextBox": return "Nazwisko";
                case "AddressTextBox": return "Adres";
                case "PhoneTextBox": return "Telefon";
                case "EmailTextBox": return "Email";
                case "PasswordBox": return "Password";
                default: return "";
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.UserId = -1;
            App.UserFullName = "";
            this.Close();
        }

        private void RegisterEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime birthDate = BirthDatePicker.SelectedDate.HasValue ? BirthDatePicker.SelectedDate.Value : DateTime.MinValue; 
            
            string[] data = new string[]
            {
                EmailTextBox.Text,
                FirstNameTextBox.Text,
                LastNameTextBox.Text,
                AddressTextBox.Text,
                PhoneTextBox.Text,
                PasswordBox.Password,
                PositionComboBox.Text,
                birthDate.ToString("yyyy-MM-dd")
            };

            switch (data[6])
            {
                case "Manager":
                    data[6] = "manager";
                    break;
                case "Mechanik":
                    data[6] = "mechanic";
                    break;
            }

            DatabaseQueries dbq = new DatabaseQueries();
            int result = dbq.EmployeeRegister(data);

            if (result > 0)
            {
                MessageBox.Show("Zarejestrowano pomyślnie!");
            }
            else if (result < 0)
            {
                MessageBox.Show("Email już zarejestrowany!");
            }
            else
            {
                MessageBox.Show("Błąd w danych. Sprawdź wprowadzone wartości.");
            }
        }
    }
}
