using System.Data.SQLite;
using System.Collections.Generic;

namespace CarRentalSystem
{
    internal class DatabaseQueries
    {
        public DatabaseQueries() { }

        public int Login(string table, string EmailTextBox, string PasswordTextBox)
        {
            int id = -1;

            string query = "SELECT rowid FROM " + table + " WHERE email = @Email AND password = @Password";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Email", EmailTextBox);
                command.Parameters.AddWithValue("@Password", PasswordTextBox);
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

        public int Register(string table, string[] data)
        {
            if (CheckIfEmailExistsEmployee(data[0]) || CheckIfEmailExistsCustomer(data[0]))
            {
                return -1;
            }

            int result = 0;

            if (table == "employees")
            {
                string query = "INSERT INTO " + table + " (email, password, first_name, last_name, address, phone, position, date_of_birth) " +
                "VALUES (@Email, @Password, @FirstName, @LastName, @Address, @Phone, @Position, @Date_of_Birth)";
                using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
                {
                    command.Parameters.AddWithValue("@Email", data[0]);
                    command.Parameters.AddWithValue("@Password", data[5]);
                    command.Parameters.AddWithValue("@FirstName", data[1]);
                    command.Parameters.AddWithValue("@LastName", data[2]);
                    command.Parameters.AddWithValue("@Address", data[3]);
                    command.Parameters.AddWithValue("@Phone", data[4]);
                    command.Parameters.AddWithValue("@Position", data[6]);
                    command.Parameters.AddWithValue("@Date_of_Birth", data[7]);

                    result = command.ExecuteNonQuery();
                }

                return result;
            }
            else
            {
                int data_6, data_8;
                if (!int.TryParse(data[6], out data_6) || !int.TryParse(data[8], out data_8))
                {
                    return - 1;
                }
                
                string query = "INSERT INTO " + table + " (email, password, first_name, last_name, address, phone, date_of_birth, card_number, card_expiry_date, card_cvv)" +
                "VALUES (@Email, @Password, @FirstName, @LastName, @Address, @Phone, @Date_of_Birth, @Card_number, @Card_expiry_date, @Card_cvv)";
                using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
                {
                    command.Parameters.AddWithValue("@Email", data[0]);
                    command.Parameters.AddWithValue("@Password", data[5]);
                    command.Parameters.AddWithValue("@FirstName", data[1]);
                    command.Parameters.AddWithValue("@LastName", data[2]);
                    command.Parameters.AddWithValue("@Address", data[3]);
                    command.Parameters.AddWithValue("@Phone", data[4]);
                    command.Parameters.AddWithValue("@Date_of_Birth", data[9]);
                    command.Parameters.AddWithValue("@Card_number", data_6);
                    command.Parameters.AddWithValue("@Card_expiry_date", data[7]);
                    command.Parameters.AddWithValue("@Card_cvv", data_8);

                    result = command.ExecuteNonQuery();
                }

                return result;
            }
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
            string query = "SELECT EXISTS (SELECT 1 FROM " + table + " WHERE email = @Email)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Email", email);
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

        public int EmployeeRegister(string[] data)
        {
            return Register("employees", data);
        }

        public int CustomerRegister(string[] data)
        {
            return Register("customers", data);
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

        public void AddCar(string brand, string model, int productionYear, string registrationNumber, string color, string carType, decimal pricePerDay)
        {
            string query = "INSERT INTO cars (brand, model, production_year, registration_number, color, car_type, price_per_day) " +
                           "VALUES (@Brand, @Model, @ProductionYear, @RegistrationNumber, @Color, @CarType, @PricePerDay)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Brand", brand);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@ProductionYear", productionYear);
                command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                command.Parameters.AddWithValue("@Color", color);
                command.Parameters.AddWithValue("@CarType", carType);
                command.Parameters.AddWithValue("@PricePerDay", pricePerDay);
                command.ExecuteNonQuery();
            }
        }

        public string[][] GetAllCars()
        {
            List<string[]> carsList = new List<string[]>();

            string query = "SELECT * FROM cars";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] car = new string[7];
                        car[0] = reader.GetString(0);
                        car[1] = reader.GetString(1);
                        car[2] = reader.GetInt32(2).ToString();
                        car[3] = reader.GetString(3);
                        car[4] = reader.GetString(4);
                        car[5] = reader.GetString(5);
                        car[6] = reader.GetDecimal(6).ToString();
                        carsList.Add(car);
                    }
                }
            }

            return carsList.ToArray();
        }

        public string[][] GetRentedCars(int id)
        {
            List<string[]> carsList = new List<string[]>();

            string query = "SELECT * FROM cars WHERE rowid IN (SELECT car_id FROM rentals WHERE customer_id = @id AND return_date IS NULL)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] car = new string[7];
                        car[0] = reader.GetString(0);
                        car[1] = reader.GetString(1);
                        car[2] = reader.GetInt32(2).ToString();
                        car[3] = reader.GetString(3);
                        car[4] = reader.GetString(4);
                        car[5] = reader.GetString(5);
                        car[6] = reader.GetDecimal(6).ToString();
                        carsList.Add(car);
                    }
                }
            }

            return carsList.ToArray();
        }

        public string GetEmployeePosition(int id)
        {
            string query = "SELECT position FROM employees WHERE rowid = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }

            return "";
        }
    }
}
