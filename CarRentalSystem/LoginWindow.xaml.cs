using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CarRentalSystem
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseQueries dbq = new DatabaseQueries();
            int id = dbq.EmployeeLogin(EmailTextBox.Text, PasswordBox.Password);
            if (id < 0)
            {
                id = dbq.CustomerLogin(EmailTextBox.Text, PasswordBox.Password);
                if (id < 0)
                {
                    MessageBox.Show("Niepoprawne dane logowania!");
                }
                else
                {
                    App.UserId = id;
                    string[] UserFullName = dbq.GetFirstNameAndLastNameOfCustomer(id);
                    App.UserFullName = UserFullName[0] + " " + UserFullName[1];
                    CustomerWindow cw = new CustomerWindow();
                    cw.Closed += ChildWindow_Closed;
                    this.Hide();
                    cw.Show();
                }
            }
            else
            {
                App.UserId = id;
                string[] UserFullName = dbq.GetFirstNameAndLastNameOfEmployee(id);
                App.UserFullName = UserFullName[0] + " " + UserFullName[1];
                EmployeeWindow cw = new EmployeeWindow();
                cw.Closed += ChildWindow_Closed;
                this.Hide();
                cw.Show();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseQueries dbq = new DatabaseQueries();
            bool emailInDB = dbq.CheckIfEmailExistsEmployee(EmailTextBox.Text);
            if (emailInDB)
            {
                MessageBox.Show("Email już zarejestrowany!");
            }
            else
            {
                emailInDB = dbq.CheckIfEmailExistsCustomer(EmailTextBox.Text);
                if (emailInDB)
                {
                    MessageBox.Show("Email już zarejestrowany!");
                }
                else
                {
                    DateTime birthDate = BirthDatePicker.SelectedDate ?? DateTime.MinValue;

                    string[] data = new string[]
                    {
                    _EmailTextBox.Text,
                    FirstNameTextBox.Text,
                    LastNameTextBox.Text,
                    AddressTextBox.Text,
                    PhoneTextBox.Text,
                    _PasswordBox.Password,
                    CardNumberTextBox.Text,
                    CardExpiryDateTextBox.Text,
                    CardCVVTextBox.Text,
                    birthDate.ToString("yyyy-MM-dd")
                    };

                    int result = dbq.CustomerRegister(data);

                    if (result > 0)
                    {
                        MessageBox.Show("Zarejestrowano pomyślnie!");
                    }
                    else
                    {
                        MessageBox.Show("Błąd w danych. Sprawdź wprowadzone wartości.");
                    }
                }
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
                case "FirstNameTextBox": return "Imię";
                case "LastNameTextBox": return "Nazwisko";
                case "AddressTextBox": return "Adres";
                case "PhoneTextBox": return "Telefon";
                case "EmailTextBox": return "Email";
                case "PasswordBox": return "Password";
                case "_EmailTextBox": return "Email";
                case "_PasswordBox": return "Password";
                case "CardNumberTextBox": return "Numer karty";
                case "CardExpiryDateTextBox": return "Data ważności karty (MM/YY)";
                case "CardCVVTextBox": return "Numer CVV";
                default: return "";
            }
        }

        private void CardNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text) || ((TextBox)sender).Text.Length >= 16;
        }

        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void CardCVVTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text) || ((TextBox)sender).Text.Length >= 3;
        }

        private void CardExpiryDateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text.Length > 5)
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Substring(0, 5);
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginButton_Click(null, null);
            }
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            EmailTextBox.Text = "Email";
            PasswordBox.Password = "Password";
            _EmailTextBox.Text = "Email";
            _PasswordBox.Password = "Password";
            FirstNameTextBox.Text = "Imię";
            LastNameTextBox.Text = "Nazwisko";
            AddressTextBox.Text = "Adres";
            PhoneTextBox.Text = "Telefon";
            CardNumberTextBox.Text = "Numer karty";
            CardExpiryDateTextBox.Text = "Data ważności karty (MM/YY)";
            CardCVVTextBox.Text = "Numer CVV";
            App.UserId = -1;
            App.UserFullName = "";
            this.Show();
        }
    }
}
