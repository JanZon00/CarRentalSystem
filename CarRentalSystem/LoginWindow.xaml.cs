using System;
using System.Windows;

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
            int id = dbq.EmployeeLogin(EmailTextBox.Text, PasswordTextBox.Password);
            if (id < 0)
            {
                id = dbq.CustomerLogin(EmailTextBox.Text, PasswordTextBox.Password);
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
                    //TODO registration + text correctness
                }
            }
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            EmailTextBox.Text = "Email";
            PasswordTextBox.Password = "Password";
            App.UserId = -1;
            App.UserFullName = "";
            this.Show();
        }
    }
}
