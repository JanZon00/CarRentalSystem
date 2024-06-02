using System.Data.SQLite;
using System.Collections.Generic;
using System;

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
    }
}
