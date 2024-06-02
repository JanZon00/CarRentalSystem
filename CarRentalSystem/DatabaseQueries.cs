using System.Data.SQLite;

namespace CarRentalSystem
{
    internal class DatabaseQueries
    {
        public DatabaseQueries() { }

        public int Login(string table, string EmailTextBox, string PasswordTextBox)
        {
            int id = -1;

            string query = "SELECT rowid FROM " + table + " WHERE email = @email AND password = @password";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@email", EmailTextBox);
                command.Parameters.AddWithValue("@password", PasswordTextBox);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }

            return id;
        }

        public string[] GetFirstNameAndLastName(string table, int id)
        {
            string[] name = new string[2];

            string query = "SELECT first_name, last_name FROM " + table + " WHERE rowid = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name[0] = reader.GetString(0);
                        name[1] = reader.GetString(1);
                    }
                }
            }

            return name;
        }

        public bool CheckIfEmailExists(string table, string email)
        {
            string query = "SELECT EXISTS (SELECT 1 FROM " + table + " WHERE email = @email)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@email", email);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetBoolean(0);
                    }
                }
            }

            return false;
        }

        public int EmployeeLogin(string EmailTextBox, string PasswordTextBox)
        {
            return Login("employees", EmailTextBox, PasswordTextBox);
        }

        public int CustomerLogin(string EmailTextBox, string PasswordTextBox)
        {
            return Login("customers", EmailTextBox, PasswordTextBox);
        }

        public string[] GetFirstNameAndLastNameOfEmployee(int id)
        {
            return GetFirstNameAndLastName("employees", id);
        }

        public string[] GetFirstNameAndLastNameOfCustomer(int id)
        {
            return GetFirstNameAndLastName("customers", id);
        }

        public bool CheckIfEmailExistsEmployee(string email)
        {
            return CheckIfEmailExists("employees", email);
        }

        public bool CheckIfEmailExistsCustomer(string email)
        {
            return CheckIfEmailExists("customers", email);
        }
    }
}
