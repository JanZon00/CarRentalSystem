using System.Windows;

namespace CarRentalSystem
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
            HelloLabel.Content = "Witaj " + App.UserFullName + "!";
        }
    }
}
