using System.Windows;

namespace CarRentalSystem
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
            HelloLabel.Content = "Witaj " + App.UserFullName + "!";
        }
    }
}
