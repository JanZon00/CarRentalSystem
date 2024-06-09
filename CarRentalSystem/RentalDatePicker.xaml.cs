using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRentalSystem
{
    /// <summary>
    /// Logika interakcji dla klasy RentalDatePicker.xaml
    /// </summary>
    public partial class RentalDatePicker : Window
    {
        public int CarId { get; set; }
        public double Cost { get; set; }

        private readonly CustomerWindow cw;

        public RentalDatePicker(CustomerWindow win, int _CarId)
        {
            InitializeComponent();

            cw = win;
            CarId = _CarId;

            DatabaseQueries dbq = new DatabaseQueries();
            Cost = dbq.GetCarDailyCost(CarId);

            datePicker.SelectedDate = DateTime.Today;
            datePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;

            CostLabel.Content = $"Koszt wynajmu: {Cost}";
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DateTime selectedDate = datePicker.SelectedDate.Value;
            DateTime today = DateTime.Today;
            TimeSpan difference = selectedDate - today;
            int daysDifference = (int)difference.TotalDays;
            daysDifference++;
            double ppd = daysDifference * Cost;
            if (ppd <= 0)
            {
                datePicker.SelectedDate = DateTime.Today;
                CostLabel.Content = $"Koszt wynajmu: {Cost}";
                return;
            }
            CostLabel.Content = $"Koszt wynajmu: {ppd}";
        }

        private void CostButton_Click(object sender, RoutedEventArgs e)
        {
            cw.RentHandler(CarId, datePicker.SelectedDate.Value);
            this.Close();
        }
    }
}
