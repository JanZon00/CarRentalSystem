using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Controls;
using System.Windows.Media;
using System.Security.Cryptography;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Windows.Controls.Primitives;
using System.Runtime.Remoting.Messaging;
using System.Windows.Documents;

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
                if (!int.TryParse(data[6], out int data_6) || !int.TryParse(data[8], out int data_8))
                {
                    return -1;
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

        public string GetEmail(string table, int id)
        {
            string name = "";

            string query = "SELECT email FROM " + table + " WHERE rowid = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = reader.GetString(0);
                    }
                }
            }

            return name;
        }

        public string GetPhone(string table, int id)
        {
            string name = "";

            string query = "SELECT phone FROM " + table + " WHERE rowid = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = reader.GetString(0);
                    }
                }
            }

            return name;
        }

        public string GetAddress(string table, int id)
        {
            string name = "";

            string query = "SELECT address FROM " + table + " WHERE rowid = @id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = reader.GetString(0);
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

        public string GetEmailOfEmployee(int id)
        {
            return GetEmail("employees", id);
        }

        public string GetEmailOfCustomer(int id)
        {
            return GetEmail("customers", id);
        }

        public string GetPhoneOfEmployee(int id)
        {
            return GetPhone("employees", id);
        }

        public string GetPhoneOfCustomer(int id)
        {
            return GetPhone("customers", id);
        }

        public string GetAddressOfEmployee(int id)
        {
            return GetAddress("employees", id);
        }

        public string GetAddressOfCustomer(int id)
        {
            return GetAddress("customers", id);
        }

        public bool CheckIfEmailExistsEmployee(string email)
        {
            return CheckIfEmailExists("employees", email);
        }

        public bool CheckIfEmailExistsCustomer(string email)
        {
            return CheckIfEmailExists("customers", email);
        }

        public void EditEmployee(int employeeIndex, string[] data)
        {
            string query = "UPDATE employees SET first_name = @FN, last_name = @LN, address = @AD, phone = @PH, email = @EM, position = @PS, date_of_birth = @DB WHERE rowid = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Id", employeeIndex);
                command.Parameters.AddWithValue("@FN", data[1]);
                command.Parameters.AddWithValue("@LN", data[2]);
                command.Parameters.AddWithValue("@AD", data[3]);
                command.Parameters.AddWithValue("@PH", data[4]);
                command.Parameters.AddWithValue("@EM", data[0]);
                command.Parameters.AddWithValue("@PS", data[5]);
                command.Parameters.AddWithValue("@DB", data[6]);
                command.ExecuteNonQuery();
            }
        }

        public bool RemoveCustomerByEmail(string email)
        {
            string query = "DELETE FROM customers WHERE email = @Email";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error removing customer account: {ex.Message}");
                    Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return false;
                }
            }
        }

        public bool RemoveEmployee(int id)
        {
            string query = "DELETE FROM employees WHERE rowid = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error removing employee account: {ex.Message}");
                    Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return false;
                }
            }
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

        public void EditCar(int carId, string brand, string model, int productionYear, string registrationNumber, string color, string carType, decimal pricePerDay)
        {
            string query = "UPDATE cars SET brand = @Brand, model = @Model, production_year = @ProductionYear, registration_number = @RegistrationNumber, color = @Color, car_type = @CarType, price_per_day = @PricePerDay WHERE rowid = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Id", carId);
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

        public bool RemoveCar(int id)
        {
            string query = "DELETE FROM cars WHERE rowid = @CarId";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@CarId", id);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error removing car data: {ex.Message}");
                    Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                    return false;
                }
            }
        }

        public double GetCarDailyCost(int id)
        {
            double cost = 0.00;

            string query = "SELECT price_per_day FROM cars WHERE rowid = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cost = reader.GetDouble(0);
                    }
                }
            }

            return cost;
        }

        public string[] GetCar(int id)
        {
            string[] car = new string[7];

            string query = "SELECT * FROM cars WHERE rowid = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        car[0] = reader.GetString(0);
                        car[1] = reader.GetString(1);
                        car[2] = reader.GetInt32(2).ToString();
                        car[3] = reader.GetString(3);
                        car[4] = reader.GetString(4);
                        car[5] = reader.GetString(5);
                        car[6] = reader.GetDecimal(6).ToString();
                    }
                }
            }

            return car;
        }

        public string[][] GetAllCars()
        {
            List<string[]> carsList = new List<string[]>();

            string query = "SELECT rowid, * FROM cars";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] car = new string[8];
                        car[0] = reader.GetInt16(0).ToString();
                        car[1] = "Marka: " + reader.GetString(1);
                        car[2] = "Model: " + reader.GetString(2);
                        car[3] = "Rok produkcji: " + reader.GetInt32(3).ToString();
                        car[4] = "Numer rejestracyjny: " + reader.GetString(4);
                        car[5] = "Kolor: " + reader.GetString(5);
                        car[6] = "Typ: " + reader.GetString(6);
                        car[7] = "Cena za dobę: " + reader.GetDecimal(7).ToString() + "zł";
                        carsList.Add(car);
                    }
                }
            }

            return carsList.ToArray();
        }

        public string[][] GetRentedCars(int id)
        {
            List<string[]> carsList = new List<string[]>();

            string query = "SELECT rowid, * FROM cars WHERE rowid IN (SELECT car_id FROM rentals WHERE customer_id = @id AND return_date >= CURRENT_DATE AND status = '')";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] car = new string[8];
                        car[0] = reader.GetInt16(0).ToString();
                        car[1] = "Marka: " + reader.GetString(1);
                        car[2] = "Model: " + reader.GetString(2);
                        car[3] = "Rok produkcji: " + reader.GetInt32(3).ToString();
                        car[4] = "Numer rejestracyjny: " + reader.GetString(4);
                        car[5] = "Kolor: " + reader.GetString(5);
                        car[6] = "Typ: " + reader.GetString(6);
                        car[7] = "Cena za dobę: " + reader.GetDecimal(7).ToString() + "zł";
                        carsList.Add(car);
                    }
                }
            }

            return carsList.ToArray();
        }

        public string[][] GetAvailableCars()
        {
            List<string[]> carsList = new List<string[]>();

            string query = "SELECT rowid, * FROM cars WHERE rowid NOT IN (SELECT car_id FROM rentals WHERE return_date >= CURRENT_DATE AND status = '' UNION SELECT car_id FROM services WHERE end_date = '')";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] car = new string[8];
                        car[0] = reader.GetInt16(0).ToString();
                        car[1] = "Marka: " + reader.GetString(1);
                        car[2] = "Model: " + reader.GetString(2);
                        car[3] = "Rok produkcji: " + reader.GetInt32(3).ToString();
                        car[4] = "Numer rejestracyjny: " + reader.GetString(4);
                        car[5] = "Kolor: " + reader.GetString(5);
                        car[6] = "Typ: " + reader.GetString(6);
                        car[7] = "Cena za dobę: " + reader.GetDecimal(7).ToString() + "zł";
                        carsList.Add(car);
                    }
                }
            }

            return carsList.ToArray();
        }

        public string[][] GetUnavailableCars()
        {
            List<string[]> carsList = new List<string[]>();

            string query = "SELECT rowid, * FROM cars WHERE rowid IN (SELECT car_id FROM rentals WHERE return_date >= CURRENT_DATE AND status = '' UNION SELECT car_id FROM services WHERE end_date = '')";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] car = new string[8];
                        car[0] = reader.GetInt16(0).ToString();
                        car[1] = "Marka: " + reader.GetString(1);
                        car[2] = "Model: " + reader.GetString(2);
                        car[3] = "Rok produkcji: " + reader.GetInt32(3).ToString();
                        car[4] = "Numer rejestracyjny: " + reader.GetString(4);
                        car[5] = "Kolor: " + reader.GetString(5);
                        car[6] = "Typ: " + reader.GetString(6);
                        car[7] = "Cena za dobę: " + reader.GetDecimal(7).ToString() + "zł";
                        carsList.Add(car);
                    }
                }
            }

            return carsList.ToArray();
        }

        public List<Rental> GetRentHistory(int carId)
        {
            List<Rental> rentalList = new List<Rental>();

            string query = "SELECT * FROM rentals WHERE car_id = @carId";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@carId", carId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int counter = 0;
                    while (reader.Read())
                    {
                        int id = reader.GetInt16(0);
                        string[] name = GetFirstNameAndLastNameOfCustomer(id);
                        string email = GetEmailOfCustomer(id);
                        string phone = GetPhoneOfCustomer(id);
                        string address = GetAddressOfCustomer(id);
                        Rental r = new Rental(++counter, name[0] + " " + name[1], email, phone, address, reader.GetDateTime(2).ToString("yyyy-MM-dd"), reader.GetDateTime(3).ToString("yyyy-MM-dd"), reader.GetString(4));
                        rentalList.Add(r);
                    }
                }
            }

            return rentalList;
        }

        public List<Service> GetServiceHistory(int carId)
        {
            List<Service> rentalList = new List<Service>();

            string query = "SELECT * FROM services WHERE car_id = @carId";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@carId", carId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int counter = 0;
                    while (reader.Read())
                    {
                        int id = reader.GetInt16(1);
                        string[] name = GetFirstNameAndLastNameOfEmployee(id);
                        string email = GetEmailOfEmployee(id);
                        string cost = "";
                        if (reader.GetDouble(5) >= 0.00)
                        {
                            cost = reader.GetDouble(5).ToString();
                        }
                        Service s = new Service(++counter, name[0] + " " + name[1], email, reader.GetString(2), reader.GetString(3), reader.GetString(4), cost);
                        rentalList.Add(s);
                    }
                }
            }

            return rentalList;
        }

        public string[] GetEmployee(int id)
        {
            string[] employee = new string[8];

            string query = "SELECT * FROM employees WHERE rowid = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee[1] = reader.GetString(0);
                        employee[2] = reader.GetString(1);
                        employee[4] = reader.GetString(2);
                        employee[5] = reader.GetString(3);
                        employee[0] = reader.GetString(4);
                        employee[7] = reader.GetString(5);
                        employee[3] = reader.GetString(6);
                        employee[6] = reader.GetString(7);
                    }
                }
            }

            return employee;
        }

        public string[][] GetAllEmployees()
        {
            List<string[]> employeesList = new List<string[]>();

            string query = "SELECT * FROM employees";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] employee = new string[8];
                        employee[1] = reader.GetString(0);
                        employee[2] = reader.GetString(1);
                        employee[4] = reader.GetString(2);
                        employee[5] = reader.GetString(3);
                        employee[0] = reader.GetString(4);
                        employee[3] = reader.GetString(5);
                        employee[6] = reader.GetString(6);
                        employee[7] = reader.GetString(7);
                        employeesList.Add(employee);
                    }
                }
            }

            return employeesList.ToArray();
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

        public bool ServiceCar(int carId)
        {
            string query = "INSERT INTO services (employee_id, car_id, start_date, end_date, description, cost) VALUES (@EmployeeId, @CarId, @StartDate, '', '', -0.01)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@EmployeeId", App.UserId);
                command.Parameters.AddWithValue("@CarId", carId);
                command.Parameters.AddWithValue("@StartDate", DateTime.Now.ToString("yyyy-MM-dd"));

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

        public bool EndServiceCar(int carId, string status, double cost)
        {
            string query = "UPDATE services SET end_date = @EndDate, description = @status, cost = @cost WHERE rowid = (SELECT rowid FROM services WHERE car_id = @CarId AND end_date = '' ORDER BY rowid DESC LIMIT 1)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@EndDate", DateTime.Now.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@cost", cost);
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

        public bool CheckIfCarIsServiced(int carId)
        {
            string query = "SELECT EXISTS (SELECT 1 FROM services WHERE car_id = @carId AND end_date = '')";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@carId", carId);
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

        public bool CheckIfCarIsRented(int carId)
        {
            string query = "SELECT EXISTS (SELECT 1 FROM rentals WHERE car_id = @carId AND return_date != '' AND status = '')";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@carId", carId);
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

        public bool RentCar(int customerId, int carId, DateTime returnDate)
        {
            string query = "INSERT INTO rentals (customer_id, car_id, rental_date, return_date, status) VALUES (@CustomerId, @CarId, @RentalDate, @ReturnDate, '')";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@CarId", carId);
                command.Parameters.AddWithValue("@RentalDate", DateTime.Now.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@ReturnDate", returnDate.ToString("yyyy-MM-dd"));

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
            if(!CheckIfCarIsRented(carId) || CheckIfCarIsServiced(carId))
            {
                return false;
            }
            
            string query = "UPDATE rentals SET status = @status WHERE rowid = (SELECT rowid FROM rentals WHERE car_id = @CarId ORDER BY rowid DESC LIMIT 1)";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                command.Parameters.AddWithValue("@status", "Anulowano");
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

        public (string carName, string registration, int status)[] GetAllCarNamesWithRegistrationAndStatus()
        {
            List<(string carName, string registration, int status)> carNamesWithStatus = new List<(string, string, int)>();
            Dictionary<string, int> carOrder = new Dictionary<string, int>();

            string query = "SELECT rowid, brand, model, registration_number FROM cars";
            using (SQLiteCommand command = new SQLiteCommand(query, App.Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    int order = 0;
                    while (reader.Read())
                    {
                        string carName = reader.GetString(1) + " " + reader.GetString(2);
                        carNamesWithStatus.Add((carName, reader.GetString(3), 0));
                        carOrder[carName] = order++;
                    }
                }
            }

            query = "SELECT c.brand, c.model, c.registration_number FROM cars c WHERE c.rowid IN (SELECT car_id FROM services WHERE end_date = '')";
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
                            carNamesWithStatus.Add((carName, reader.GetString(2), 1));
                        }
                        else
                        {
                            carNamesWithStatus.Add((carName, reader.GetString(2), 1));
                        }
                    }
                }
            }

            query = "SELECT c.brand, c.model, c.registration_number FROM cars c WHERE c.rowid IN (SELECT car_id FROM rentals WHERE return_date >= CURRENT_DATE AND status = '')";
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
                            carNamesWithStatus.Add((carName, reader.GetString(2), 2));
                        }
                        else
                        {
                            carNamesWithStatus.Add((carName, reader.GetString(2), 2));
                        }
                    }
                }
            }

            var sortedCarNamesWithStatus = carNamesWithStatus.OrderBy(x => carOrder[x.carName]).ToArray();
            return sortedCarNamesWithStatus;
        }
    }

    public class Rental
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string RentalDate { get; set; }
        public string ReturnDate { get; set; }
        public string Status { get; set; }
        public Rental(int i, string cn, string ce, string cp, string ca, string rd1, string rd2, string s)
        {
            Id = i;
            CustomerName = cn;
            CustomerEmail = ce;
            CustomerPhone = cp;
            CustomerAddress = ca;
            RentalDate = rd1;
            ReturnDate = rd2;
            Status = s;
        }
    }

    public class Service
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string Cost { get; set; }
        public Service(int i, string cn, string ce, string sd, string ed, string s, string c)
        {
            Id = i;
            EmployeeName = cn;
            EmployeeEmail = ce;
            StartDate = sd;
            EndDate = ed;
            Status = s;
            Cost = c;
        }
    }
}
