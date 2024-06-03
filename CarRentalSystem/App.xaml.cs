using System;
using System.Windows;
using System.Data.SQLite;

namespace CarRentalSystem
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SQLiteConnection Connection { get; private set; }
        public static int UserId { get; set; }
        public static string UserFullName { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeDatabase();

            UserId = -1;
            UserFullName = "";
        }

        private void InitializeDatabase()
        {
            try
            {
                string connectionString = "Data Source=../../database.db;Version=3;";
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas inicjalizacji bazy danych: " + ex.Message);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
    }
}
