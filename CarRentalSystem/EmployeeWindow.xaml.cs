using System;
using System.Collections.Generic;
using System.Globalization;
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

            RefreshCarComboBox();
            RefreshEmployeeComboBox();

            if (dbq.GetEmployeePosition(App.UserId) == "mechanic")
            {
                addNewCarExpander.IsEnabled = false;
                addNewEmployeeExpander.IsEnabled = false;
                editCarExpander.IsEnabled = false;
                removeCarExpander.IsEnabled = false;
                cancelCarRentalExpander.IsEnabled = false;
                editEmployeeExpander.IsEnabled = false;
                removeEmployeeExpander.IsEnabled = false;
                removeCustomerExpander.IsEnabled = false;
            }
            else
            {
                startServiceExpander.IsEnabled = false;
                stopServiceExpander.IsEnabled = false;
                historyCarServiceExpander.IsEnabled = false;
            }
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            string brand = BrandTextBox.Text;
            string model = ModelTextBox.Text;
            string registrationNumber = RegistrationNumberTextBox.Text;
            string color = ColorTextBox.Text;
            string carType = CarTypeTextBox.Text;

            if (int.TryParse(ProductionYearTextBox.Text, out int productionYear) && decimal.TryParse(PricePerDayTextBox.Text, out decimal pricePerDay))
            {
                DatabaseQueries dbQueries = new DatabaseQueries();
                dbQueries.AddCar(brand, model, productionYear, registrationNumber, color, carType, pricePerDay);
                MessageBox.Show("Samochód został dodany pomyślnie.");
                RefreshCarComboBox();
            }
            else
            {
                MessageBox.Show("Błąd w danych. Sprawdź wprowadzone wartości.");
            }
        }

        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCarText = CarComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedCarText))
            {
                MessageBox.Show("Wybierz samochód do anulowania rezerwacji.");
                return;
            }
            string[] carInfoParts = selectedCarText.Split('.');
            if (carInfoParts.Length < 2)
            {
                MessageBox.Show("Nieprawidłowy format danych samochodu.");
                return;
            }

            if (int.TryParse(carInfoParts[0], out int carIndex))
            {
                int carId = carIndex;
                DatabaseQueries dbQueries = new DatabaseQueries();
                bool cancelSuccess = dbQueries.CancelReservation(carId);
                if (cancelSuccess)
                {
                    MessageBox.Show("Rezerwacja została anulowana pomyślnie.");
                    RefreshCarComboBox();
                }
                else
                {
                    MessageBox.Show("Samochód nie jest wynajęty.");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowy numer samochodu.");
            }
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCarText = EditCarComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedCarText))
            {
                MessageBox.Show("Wybierz samochód do edycji danych.");
                return;
            }
            string[] carInfoParts = selectedCarText.Split('.');
            if (carInfoParts.Length < 2)
            {
                MessageBox.Show("Nieprawidłowy format danych samochodu.");
                return;
            }

            if (int.TryParse(carInfoParts[0], out int carIndex))
            {
                int carId = carIndex;
                string brand = EditBrandTextBox.Text;
                string model = EditModelTextBox.Text;
                string registrationNumber = EditRegistrationNumberTextBox.Text;
                string color = EditColorTextBox.Text;
                string carType = EditCarTypeTextBox.Text;

                if (int.TryParse(EditProductionYearTextBox.Text, out int productionYear) && decimal.TryParse(EditPricePerDayTextBox.Text, out decimal pricePerDay))
                {
                    DatabaseQueries dbQueries = new DatabaseQueries();
                    dbQueries.EditCar(carId, brand, model, productionYear, registrationNumber, color, carType, pricePerDay);
                    MessageBox.Show("Dane samochodu zostały zmienione pomyślnie.");
                    RefreshCarComboBox();
                }
                else
                {
                    MessageBox.Show("Błąd w danych. Sprawdź wprowadzone wartości.");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowy numer samochodu.");
            }
        }

        private void RemoveCarButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCarText = RemoveCarComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedCarText))
            {
                MessageBox.Show("Wybierz samochód do usunięcia.");
                return;
            }
            string[] carInfoParts = selectedCarText.Split('.');
            if (carInfoParts.Length < 2)
            {
                MessageBox.Show("Nieprawidłowy format danych samochodu.");
                return;
            }

            if (int.TryParse(carInfoParts[0], out int carIndex))
            {
                int carId = carIndex;
                DatabaseQueries dbQueries = new DatabaseQueries();
                bool cancelSuccess = dbQueries.RemoveCar(carId);
                if (cancelSuccess)
                {
                    MessageBox.Show("Usunięto pojazd pomyślnie.");
                    RefreshCarComboBox();
                }
                else
                {
                    MessageBox.Show("Nie udało się usunąć danych pojazdu.");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowy numer samochodu.");
            }
        }

        private void RefreshCarComboBox()
        {
            CarComboBox.Items.Clear();
            RemoveCarComboBox.Items.Clear();
            EditCarComboBox.Items.Clear();
            HistoryCarRentalComboBox.Items.Clear();
            StartServiceComboBox.Items.Clear();
            StopServiceComboBox.Items.Clear();
            HistoryCarServiceComboBox.Items.Clear();

            DatabaseQueries dbq = new DatabaseQueries();
            (string carName, string registration, int status)[] allCarNamesWithStatus = dbq.GetAllCarNamesWithRegistrationAndStatus();
            int counter = 1;
            foreach (var (carName, registration, status) in allCarNamesWithStatus)
            {
                string carDisplayText = $"{counter}. {carName} - {registration}";
                switch (status)
                {
                    case 1:
                        carDisplayText += " (serwisowany)";
                        break;
                    case 2:
                        carDisplayText += " (wynajęty)";
                        break;
                }
                CarComboBox.Items.Add(carDisplayText);
                RemoveCarComboBox.Items.Add(carDisplayText);
                EditCarComboBox.Items.Add(carDisplayText);
                HistoryCarRentalComboBox.Items.Add(carDisplayText);
                StartServiceComboBox.Items.Add(carDisplayText);
                StopServiceComboBox.Items.Add(carDisplayText);
                HistoryCarServiceComboBox.Items.Add(carDisplayText);
                counter++;
            }
        }

        private void RefreshRentalGrid()
        {
            DatabaseQueries dbq = new DatabaseQueries();
            HistoryCarRentalGrid.ItemsSource = dbq.GetRentHistory(HistoryCarRentalComboBox.SelectedIndex + 1);
        }

        private void RefreshServiceGrid()
        {
            DatabaseQueries dbq = new DatabaseQueries();
            HistoryCarServiceGrid.ItemsSource = dbq.GetServiceHistory(HistoryCarServiceComboBox.SelectedIndex + 1);
        }

        private void RefreshEmployeeComboBox()
        {
            RemoveEmployeeComboBox.Items.Clear();
            EditEmployeeComboBox.Items.Clear();

            DatabaseQueries dbq = new DatabaseQueries();
            string[][] allEmployess = dbq.GetAllEmployees();
            int counter = 1;
            foreach (var employee in allEmployess)
            {
                string carDisplayText = $"{counter}. {employee[1]} {employee[2]} - {employee[0]}";
                RemoveEmployeeComboBox.Items.Add(carDisplayText);
                EditEmployeeComboBox.Items.Add(carDisplayText);
                counter++;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == GetPlaceholderText(textBox.Name))
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(SystemColors.ControlTextColor);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = GetPlaceholderText(textBox.Name);
                textBox.Foreground = new SolidColorBrush(SystemColors.GrayTextColor);
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && passwordBox.Password == GetPlaceholderText(passwordBox.Name))
            {
                passwordBox.Password = "";
                passwordBox.Foreground = new SolidColorBrush(SystemColors.ControlTextColor);
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && string.IsNullOrWhiteSpace(passwordBox.Password))
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
                case "EditBrandTextBox": return "Marka";
                case "EditModelTextBox": return "Model";
                case "EditProductionYearTextBox": return "Rok produkcji";
                case "EditRegistrationNumberTextBox": return "Numer rejestracyjny";
                case "EditColorTextBox": return "Kolor";
                case "EditCarTypeTextBox": return "Typ samochodu";
                case "EditPricePerDayTextBox": return "Cena za dzień";
                case "EditFirstNameTextBox": return "Imię";
                case "EditLastNameTextBox": return "Nazwisko";
                case "EditAddressTextBox": return "Adres";
                case "EditPhoneTextBox": return "Telefon";
                case "EditEmailTextBox": return "Email";
                case "EditPasswordBox": return "Password";
                case "RemoveEmailTextBox": return "Email";
                case "CostTextBox": return "Koszt";
                case "InfoTextBox": return "Uwagi";
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
            DateTime birthDate = BirthDatePicker.SelectedDate ?? DateTime.MinValue;

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
                RefreshEmployeeComboBox();
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

        private void EditCarComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EditCarComboBox.SelectedIndex > -1)
            {
                string[] car = (new DatabaseQueries()).GetCar(EditCarComboBox.SelectedIndex + 1);
                EditBrandTextBox.IsEnabled = true;
                EditBrandTextBox.Text = car[0];
                EditModelTextBox.IsEnabled = true;
                EditModelTextBox.Text = car[1];
                EditProductionYearTextBox.IsEnabled = true;
                EditProductionYearTextBox.Text = car[2];
                EditRegistrationNumberTextBox.IsEnabled = true;
                EditRegistrationNumberTextBox.Text = car[3];
                EditColorTextBox.IsEnabled = true;
                EditColorTextBox.Text = car[4];
                EditCarTypeTextBox.IsEnabled = true;
                EditCarTypeTextBox.Text = car[5];
                EditPricePerDayTextBox.IsEnabled = true;
                EditPricePerDayTextBox.Text = car[6];
                EditCarButton.IsEnabled = true;
            }
            else
            {
                EditBrandTextBox.IsEnabled = false;
                EditBrandTextBox.Text = "";
                EditModelTextBox.IsEnabled = false;
                EditModelTextBox.Text = "";
                EditProductionYearTextBox.IsEnabled = false;
                EditProductionYearTextBox.Text = "";
                EditRegistrationNumberTextBox.IsEnabled = false;
                EditRegistrationNumberTextBox.Text = "";
                EditColorTextBox.IsEnabled = false;
                EditColorTextBox.Text = "";
                EditCarTypeTextBox.IsEnabled = false;
                EditCarTypeTextBox.Text = "";
                EditPricePerDayTextBox.IsEnabled = false;
                EditPricePerDayTextBox.Text = "";
                EditCarButton.IsEnabled = false;
            }
        }

        private void RemoveCarComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RemoveCarComboBox.SelectedIndex > -1)
            {
                RemoveCarButton.IsEnabled = true;
            }
            else
            {
                RemoveCarButton.IsEnabled = false;
            }
        }

        private void HistoryCarRentalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HistoryCarRentalComboBox.SelectedIndex > -1)
            {
                HistoryCarRentalGrid.IsEnabled = true;
                RefreshRentalGrid();
            }
            else
            {
                HistoryCarRentalGrid.IsEnabled = false;
            }
        }

        private void CarComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarComboBox.SelectedIndex > -1)
            {
                CarButton.IsEnabled = true;
            }
            else
            {
                CarButton.IsEnabled = false;
            }
        }

        private void EditEmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EditEmployeeComboBox.SelectedIndex > -1)
            {
                string[] employee = (new DatabaseQueries()).GetEmployee(EditEmployeeComboBox.SelectedIndex + 1);
                EditEmailTextBox.IsEnabled = true;
                EditEmailTextBox.Text = employee[0];
                EditFirstNameTextBox.IsEnabled = true;
                EditFirstNameTextBox.Text = employee[1];
                EditLastNameTextBox.IsEnabled = true;
                EditLastNameTextBox.Text = employee[2];
                EditPositionComboBox.IsEnabled = true;
                if (employee[3] == "manager")
                {
                    EditPositionComboBox.SelectedIndex = 0;
                }
                else
                {
                    EditPositionComboBox.SelectedIndex = 1;
                }
                EditAddressTextBox.IsEnabled = true;
                EditAddressTextBox.Text = employee[4];
                EditPhoneTextBox.IsEnabled = true;
                EditPhoneTextBox.Text = employee[5];
                EditBirthDatePicker.IsEnabled = true;
                EditBirthDatePicker.DisplayDate = DateTime.ParseExact(employee[6], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                EditBirthDatePicker.Text = employee[6];
                EditEmployeeButton.IsEnabled = true;
            }
            else
            {
                EditEmailTextBox.IsEnabled = false;
                EditEmailTextBox.Text = "";
                EditFirstNameTextBox.IsEnabled = false;
                EditFirstNameTextBox.Text = "";
                EditLastNameTextBox.IsEnabled = false;
                EditLastNameTextBox.Text = "";
                EditPositionComboBox.IsEnabled = false;
                EditPositionComboBox.SelectedIndex = -1;
                EditAddressTextBox.IsEnabled = false;
                EditAddressTextBox.Text = "";
                EditPhoneTextBox.IsEnabled = false;
                EditPhoneTextBox.Text = "";
                EditBirthDatePicker.IsEnabled = false;
                EditBirthDatePicker.Text = "";
                EditEmployeeButton.IsEnabled = false;
            }
        }

        private void RemoveEmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RemoveEmployeeComboBox.SelectedIndex > -1)
            {
                RemoveEmployeeButton.IsEnabled = true;
            }
            else
            {
                RemoveEmployeeButton.IsEnabled = false;
            }
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedEmployeeText = EditEmployeeComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedEmployeeText))
            {
                MessageBox.Show("Wybierz konto do edycji danych.");
                return;
            }
            string[] employeeInfoParts = selectedEmployeeText.Split('.');
            if (employeeInfoParts.Length < 2)
            {
                MessageBox.Show("Nieprawidłowy format danych konta.");
                return;
            }

            if (int.TryParse(employeeInfoParts[0], out int employeeIndex))
            {
                DateTime birthDate = EditBirthDatePicker.SelectedDate ?? DateTime.MinValue;

                string[] data = new string[]
                {
                EditEmailTextBox.Text,
                EditFirstNameTextBox.Text,
                EditLastNameTextBox.Text,
                EditAddressTextBox.Text,
                EditPhoneTextBox.Text,
                EditPositionComboBox.Text,
                birthDate.ToString("yyyy-MM-dd")
                };

                switch (data[5])
                {
                    case "Manager":
                        data[5] = "manager";
                        break;
                    case "Mechanik":
                        data[5] = "mechanic";
                        break;
                }

                DatabaseQueries dbq = new DatabaseQueries();
                dbq.EditEmployee(employeeIndex, data);
                MessageBox.Show("Dane zmienione pomyślnie!");
                RefreshEmployeeComboBox();
            }
            else
            {
                MessageBox.Show("Nieprawidłowy numer konta.");
            }
        }

        private void RemoveEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedEmployeeText = RemoveEmployeeComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedEmployeeText))
            {
                MessageBox.Show("Wybierz konto do edycji danych.");
                return;
            }
            string[] employeeInfoParts = selectedEmployeeText.Split('.');
            if (employeeInfoParts.Length < 2)
            {
                MessageBox.Show("Nieprawidłowy format danych konta.");
                return;
            }

            if (int.TryParse(employeeInfoParts[0], out int employeeIndex))
            {
                DatabaseQueries dbQueries = new DatabaseQueries();
                bool cancelSuccess = dbQueries.RemoveEmployee(employeeIndex);
                if (cancelSuccess)
                {
                    MessageBox.Show("Usunięto konto pomyślnie.");
                    RefreshEmployeeComboBox();
                }
                else
                {
                    MessageBox.Show("Nie udało się usunąć danych pracownika.");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowy numer konta.");
            }
        }

        private void RemoveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseQueries dbq = new DatabaseQueries();
            if (dbq.CheckIfEmailExistsCustomer(RemoveEmailTextBox.Text))
            {
                dbq.RemoveCustomerByEmail(RemoveEmailTextBox.Text);
                RemoveEmailTextBox.Text = "";
                MessageBox.Show("Konto klienta usunięto pomyślnie.");
            }
            else
            {
                MessageBox.Show("Brak takiego adresu email w bazie.");
            }
        }

        private void StartServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartServiceComboBox.SelectedIndex > -1)
            {
                StartServiceButton.IsEnabled = true;
            }
            else
            {
                StartServiceButton.IsEnabled = false;
            }
        }

        private void StopServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StopServiceComboBox.SelectedIndex > -1)
            {
                StopServiceButton.IsEnabled = true;
                CostTextBox.IsEnabled = true;
                InfoTextBox.IsEnabled = true;
            }
            else
            {
                StopServiceButton.IsEnabled = false;
                CostTextBox.IsEnabled = false;
                InfoTextBox.IsEnabled = false;
            }
        }

        private void HistoryCarServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HistoryCarServiceComboBox.SelectedIndex > -1)
            {
                HistoryCarServiceGrid.IsEnabled = true;
                RefreshServiceGrid();
            }
            else
            {
                HistoryCarServiceGrid.IsEnabled = false;
            }
        }

        private void StartServiceButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseQueries dbq = new DatabaseQueries();
            if (dbq.CheckIfCarIsServiced(StartServiceComboBox.SelectedIndex + 1))
            {
                MessageBox.Show("Samochód jest już serwisowany."); 
                return;
            }
            if (dbq.CheckIfCarIsRented(StartServiceComboBox.SelectedIndex + 1))
            {
                MessageBox.Show("Samochód jest wynajęty.");
                return;
            }
            if (dbq.ServiceCar(StartServiceComboBox.SelectedIndex + 1))
            {
                MessageBox.Show("Rozpoczęto serwisowanie samochodu.");
                RefreshCarComboBox();
            }
            else
            {
                MessageBox.Show("Nie można uruchomić serwisowania dla tego samochodu.");
            }
        }

        private void StopServiceButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseQueries dbq = new DatabaseQueries();
            if (!dbq.CheckIfCarIsServiced(StopServiceComboBox.SelectedIndex + 1))
            {
                MessageBox.Show("Samochód nie jest serwisowany.");
                return;
            }
            if (double.TryParse(CostTextBox.Text, out double Cost))
            {
                if (dbq.EndServiceCar(StopServiceComboBox.SelectedIndex + 1, InfoTextBox.Text, Cost))
                {
                    MessageBox.Show("Zakończono serwisowanie samochodu.");
                    RefreshCarComboBox();
                }
                else
                {
                    MessageBox.Show("Nie można uruchomić serwisowania dla tego samochodu.");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowy format kosztu serwisu.");
            }
        }
    }
}
