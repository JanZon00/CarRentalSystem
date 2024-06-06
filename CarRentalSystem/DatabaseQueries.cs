﻿using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Linq;

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
                        car[0] = "Brand: " + reader.GetString(0);
                        car[1] = "Model: " + reader.GetString(1);
                        car[2] = "Production Year: " + reader.GetInt32(2).ToString();
                        car[3] = "Registration Number: " + reader.GetString(3);
                        car[4] = "Color: " + reader.GetString(4);
                        car[5] = "Car Type: " + reader.GetString(5);
                        car[6] = "Price per day: " + reader.GetDecimal(6).ToString() + "zł";
                        carsList.Add(car);
                    }
                }
            }

            return carsList.ToArray();
        }

        public string[][] GetRentedCars(int id)
        {
            List<string[]> carsList = new List<string[]>();

            string query = "SELECT * FROM cars WHERE rowid IN (SELECT car_id FROM rentals WHERE customer_id = @id AND (return_date IS NULL OR return_date >= CURRENT_DATE))";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] car = new string[7];
                        car[0] = "Brand: " + reader.GetString(0);
                        car[1] = "Model: " + reader.GetString(1);
                        car[2] = "Production Year: " + reader.GetInt32(2).ToString();
                        car[3] = "Registration Number: " + reader.GetString(3);
                        car[4] = "Color: " + reader.GetString(4);
                        car[5] = "Car Type: " + reader.GetString(5);
                        car[6] = "Price per day: " + reader.GetDecimal(6).ToString() + "zł";
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

        public bool RentCar(int customerId, int carId, DateTime returnDate)
        {
            string query = "INSERT INTO rentals (customer_id, car_id, rental_date, return_date, status) VALUES (@CustomerId, @CarId, @RentalDate, @ReturnDate, 'rented')";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@CarId", carId);
                command.Parameters.AddWithValue("@RentalDate", DateTime.Now);
                command.Parameters.AddWithValue("@ReturnDate", returnDate);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error renting car: {ex.Message}");
                    Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return false;
                }
            }
        }

        public bool CancelReservation(int carId)
        {
            string query = "DELETE FROM rentals WHERE car_id = @CarId";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@CarId", carId);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error canceling reservation: {ex.Message}");
                    Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return false;
                }
            }
        }

        public (string carName, bool isRented)[] GetAllCarNamesWithStatus()
        {
            List<(string carName, bool isRented)> carNamesWithStatus = new List<(string, bool)>();
            Dictionary<string, int> carOrder = new Dictionary<string, int>();

            string query = "SELECT rowid, brand, model FROM cars";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int order = 0;
                    while (reader.Read())
                    {
                        string carName = reader.GetString(1) + " " + reader.GetString(2);
                        carNamesWithStatus.Add((carName, false));
                        carOrder[carName] = order++;
                    }
                }
            }

            query = "SELECT c.brand, c.model FROM cars c WHERE c.rowid IN (SELECT r.car_id FROM rentals r WHERE r.return_date IS NULL OR r.return_date >= CURRENT_DATE)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string carName = reader.GetString(0) + " " + reader.GetString(1);
                        var existingCar = carNamesWithStatus.FirstOrDefault(x => x.carName == carName);
                        if (existingCar != default)
                        {
                            carNamesWithStatus.Remove(existingCar);
                            carNamesWithStatus.Add((carName, true));
                        }
                        else
                        {
                            carNamesWithStatus.Add((carName, true));
                        }
                    }
                }
            }

            var sortedCarNamesWithStatus = carNamesWithStatus.OrderBy(x => carOrder[x.carName]).ToArray();
            return sortedCarNamesWithStatus;
        }
    }
}
