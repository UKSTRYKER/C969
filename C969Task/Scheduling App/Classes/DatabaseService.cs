using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scheduling_App
{
    class DatabaseService
    {
        public static string connString = @"Server=127.0.0.1;Database=client_schedule;Uid=sqlUser;Pwd=Passw0rd!;";
        public static MySqlConnection conn = new MySqlConnection(connString);

        public static List<User> getAllUsers()
        {
            List<User> listOfUsers = new List<User>();
            string query = "SELECT * FROM user";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int userID = Convert.ToInt32(dataReader[0]);
                string userName = dataReader[1].ToString();
                string password = dataReader[2].ToString();
                int active = Convert.ToInt32(dataReader[3]);
                DateTime createDate = Convert.ToDateTime(dataReader[4]).ToLocalTime();
                string createdBy = dataReader[5].ToString();
                DateTime lastUpdate = Convert.ToDateTime(dataReader[6]).ToLocalTime();
                string lastUpdateBy = dataReader[7].ToString();

                listOfUsers.Add(new User(userID, userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy));
            }

            conn.Close();

            return listOfUsers;
        }

        public static void deleteCity(int cityId)
        {
            conn.Open();
            string query = $"DELETE FROM city WHERE cityId={cityId};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MainScreen.CityDictionary.Remove(cityId);
        }

        public static void deleteCountry(int countryId)
        {
            conn.Open();
            string query = $"DELETE FROM country WHERE countryId={countryId};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MainScreen.CountryDictionary.Remove(countryId);
        }

        public static void getAppointments()
        {
            MainScreen.ListOfAppointments.Clear();

            string query = $"select * from appointment WHERE userId={MainScreen.LoggedInUser.UserID}";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int appointmentId = Convert.ToInt32(dataReader[0]);
                int customerId = Convert.ToInt32(dataReader[1]);
                int userId = Convert.ToInt32(dataReader[2]);
                string type = dataReader[7].ToString();
                DateTime start = Convert.ToDateTime(dataReader[9]).ToLocalTime();
                DateTime end = Convert.ToDateTime(dataReader[10]).ToLocalTime();
                DateTime createDate = Convert.ToDateTime(dataReader[11]).ToLocalTime();
                string createdBy = dataReader[12].ToString();
                DateTime lastUpdate = Convert.ToDateTime(dataReader[13]).ToLocalTime();
                string lastUpdateBy = dataReader[14].ToString();

                MainScreen.ListOfAppointments.Add(
                    new AppointmentModel(appointmentId, customerId, userId, type, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)
                );
            }

            conn.Close();
        }

        public static void addAppointment(int customerId, string type, DateTime start, DateTime end)
        {
            DateTime now = DateTime.Now;
            var addedAppointment = new AppointmentModel(customerId, MainScreen.LoggedInUser.UserID, type, start, end, now, MainScreen.LoggedInUser.UserName, now, MainScreen.LoggedInUser.UserName);

            conn.Open();
            string query =
                $"INSERT INTO `appointment` VALUES ({addedAppointment.AppointmentId},{addedAppointment.CustomerId},{addedAppointment.UserId},'not needed','not needed','not needed','not needed','{addedAppointment.Type}','not needed','{addedAppointment.Start.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}','{addedAppointment.End.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}','{addedAppointment.CreateDate.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}','{addedAppointment.CreatedBy}','{addedAppointment.LastUpdate.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}','{addedAppointment.LastUpdateBy}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MainScreen.ListOfAppointments.Add(addedAppointment);
        }

        public static void deleteAppointment(AppointmentModel appointment)
        {
            conn.Open();
            string query = $"DELETE FROM appointment WHERE appointmentId={appointment.AppointmentId};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MainScreen.ListOfAppointments.Remove(appointment);
        }

        public static void updateAppointment(AppointmentModel appointment, int customerId, string type, DateTime start, DateTime end)
        {
            DateTime now = DateTime.Now;
            string nowString = now.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            conn.Open();
            string query =
                $"UPDATE appointment SET customerId={customerId},userId={MainScreen.LoggedInUser.UserID},type='{type}',start='{start.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}',end='{end.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}',lastUpdate='{nowString}',lastUpdateBy='{MainScreen.LoggedInUser.UserName}' WHERE appointmentId={appointment.AppointmentId};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            AppointmentModel updatedAppointment = new AppointmentModel(appointment.AppointmentId, customerId, MainScreen.LoggedInUser.UserID, type, start, end, appointment.CreateDate, appointment.CreatedBy, now, MainScreen.LoggedInUser.UserName);
            int indexOfAppointmentList = MainScreen.ListOfAppointments.IndexOf(appointment);
            MainScreen.ListOfAppointments.RemoveAt(indexOfAppointmentList);
            MainScreen.ListOfAppointments.Insert(indexOfAppointmentList, updatedAppointment);
        }

        public static void getCustomers()
        {
            string query = "select * from customer";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int customerID = Convert.ToInt32(dataReader[0]);
                string customerName = dataReader[1].ToString();
                int addressID = Convert.ToInt32(dataReader[2]);
                int active = Convert.ToInt32(dataReader[3]);
                DateTime createDate = Convert.ToDateTime(dataReader[4]).ToLocalTime();
                string createdBy = dataReader[5].ToString();
                DateTime lastUpdate = Convert.ToDateTime(dataReader[6]).ToLocalTime();
                string lastUpdateBy = dataReader[7].ToString();

                MainScreen.ListOfCustomers.Add(new CustomerModel(customerID, customerName, addressID, active, createDate, createdBy, lastUpdate, lastUpdateBy));
            }

            conn.Close();
        }

        public static int addCustomer(string customerName, int addressID, string user)
        {
            DateTime now = DateTime.Now;
            var addedCustomer = new CustomerModel(customerName, addressID, 1, now, user, now, user);

            conn.Open();
            string query =
                $"INSERT INTO `customer` VALUES ({addedCustomer.CustomerId}, '{addedCustomer.CustomerName}', {addedCustomer.AddressId}, {addedCustomer.Active}, '{addedCustomer.CreateDate.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}', '{addedCustomer.CreatedBy}', '{addedCustomer.LastUpdate.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}', '{addedCustomer.LastUpdateBy}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MainScreen.ListOfCustomers.Add(addedCustomer);
            return addedCustomer.CustomerId;
        }

        public static void deleteCustomer(CustomerModel customer)
        {
            conn.Open();
            string query = $"DELETE FROM customer WHERE customerId={customer.CustomerId};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MainScreen.ListOfCustomers.Remove(customer);
            deleteAddress(customer.AddressId);
        }

        public static void updateCustomer(CustomerModel customer, string customerName, string user)
        {
            DateTime now = DateTime.Now;
            string nowString = now.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            conn.Open();
            string query =
                $"UPDATE customer SET customerName='{customerName}', lastUpdate='{nowString}', lastUpdateBy='{user}' WHERE customerId={customer.CustomerId};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            CustomerModel updatedCustomer = new CustomerModel(customer.CustomerId, customerName, customer.AddressId, customer.Active, customer.CreateDate, customer.CreatedBy, now, user);
            int indexOfCustomerList = MainScreen.ListOfCustomers.IndexOf(customer);
            MainScreen.ListOfCustomers.RemoveAt(indexOfCustomerList);
            MainScreen.ListOfCustomers.Insert(indexOfCustomerList, updatedCustomer);
        }

        public static void getAddresses()
        {
            string query = "select * from address";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int addressID = Convert.ToInt32(dataReader[0]);
                string address1 = dataReader[1].ToString();
                string address2 = dataReader[2].ToString();
                int cityID = Convert.ToInt32(dataReader[3]);
                string postalCode = dataReader[4].ToString();
                string phone = dataReader[5].ToString();
                DateTime createDate = Convert.ToDateTime(dataReader[6]).ToLocalTime();
                string createdBy = dataReader[7].ToString();
                DateTime lastUpdate = Convert.ToDateTime(dataReader[8]).ToLocalTime();
                string lastUpdateBy = dataReader[9].ToString();

                MainScreen.AddressDictionary.Add(addressID, new Address(addressID, address1, address2, cityID, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy));
            }

            conn.Close();
        }

        public static int addAddress(string address1, string address2, int cityId, string postalCode, string phone, string userName)
        {
            DateTime now = DateTime.Now;
            var addedAddress = new Address(address1, address2, cityId, postalCode, phone, now, userName, now, userName);

            conn.Open();
            string query =
                $"INSERT INTO `address` VALUES ({addedAddress.AddressId}, '{addedAddress.AddressLine}', '{addedAddress.AddressLine2}', {addedAddress.CityId}, '{addedAddress.PostalCode}', '{addedAddress.Phone}', '{addedAddress.CreateDate.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}', '{addedAddress.CreatedBy}', '{addedAddress.LastUpdate.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo)}', '{addedAddress.LastUpdateBy}')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MainScreen.AddressDictionary.Add(addedAddress.AddressId, addedAddress);
            return addedAddress.AddressId;
        }

        public static void deleteAddress(int addressID)
        {
            if (!MainScreen.AddressDictionary.TryGetValue(addressID, out var address))
            {
                conn.Open();
                string fallbackQuery = $"DELETE FROM address WHERE addressId={addressID};";
                MySqlCommand fallbackCmd = new MySqlCommand(fallbackQuery, conn);
                fallbackCmd.ExecuteNonQuery();
                conn.Close();
                return;
            }

            int cityId = address.CityId;
            int countryId = -1;

            if (MainScreen.CityDictionary.TryGetValue(cityId, out var city))
            {
                countryId = city.CountryId;
            }

            conn.Open();
            string query = $"DELETE FROM address WHERE addressId={addressID};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MainScreen.AddressDictionary.Remove(addressID);

            bool cityStillUsed = MainScreen.AddressDictionary.Values.Any(a => a.CityId == cityId);
            if (!cityStillUsed && MainScreen.CityDictionary.ContainsKey(cityId))
            {
                deleteCity(cityId);
            }

            if (countryId != -1)
            {
                bool countryStillUsed = MainScreen.CityDictionary.Values.Any(c => c.CountryId == countryId);
                if (!countryStillUsed && MainScreen.CountryDictionary.ContainsKey(countryId))
                {
                    deleteCountry(countryId);
                }
            }
        }

        public static void updateAddress(Address address, string address1, string address2, int cityId, string postalCode, string phone, string user)
        {
            DateTime now = DateTime.Now;
            string nowString = now.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            conn.Open();
            string query =
                $"UPDATE address SET address='{address1}', address2='{address2}', cityId={cityId}, postalCode='{postalCode}', phone='{phone}', lastUpdate='{nowString}', lastUpdateBy='{user}' WHERE addressId={address.AddressId};";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MainScreen.AddressDictionary[address.AddressId] = new Address(address.AddressId, address1, address2, cityId, postalCode, phone, address.CreateDate, address.CreatedBy, now, user);
        }

        public static void getCities()
        {
            string query = "select * from city";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int cityID = Convert.ToInt32(dataReader[0]);
                string city = dataReader[1].ToString();
                int countryID = Convert.ToInt32(dataReader[2]);
                DateTime createDate = Convert.ToDateTime(dataReader[3]).ToLocalTime();
                string createdBy = dataReader[4].ToString();
                DateTime lastUpdate = Convert.ToDateTime(dataReader[5]).ToLocalTime();
                string lastUpdateBy = dataReader[6].ToString();

                MainScreen.CityDictionary.Add(cityID, new City(cityID, city, countryID, createDate, createdBy, lastUpdate, lastUpdateBy));
            }

            conn.Close();
        }

        public static void getCountries()
        {
            string query = "select * from country";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int countryID = Convert.ToInt32(dataReader[0]);
                string country = dataReader[1].ToString();
                DateTime createDate = Convert.ToDateTime(dataReader[2]).ToLocalTime();
                string createdBy = dataReader[3].ToString();
                DateTime lastUpdate = Convert.ToDateTime(dataReader[4]).ToLocalTime();
                string lastUpdateBy = dataReader[5].ToString();

                MainScreen.CountryDictionary.Add(countryID, new Country(countryID, country, createDate, createdBy, lastUpdate, lastUpdateBy));
            }

            conn.Close();
        }

        public static int addCountry(string countryName, string user)
        {
            DateTime now = DateTime.Now;
            string nowString = now.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);

            int newCountryId = 1;
            if (MainScreen.CountryDictionary.Count > 0)
            {
                newCountryId = MainScreen.CountryDictionary.Keys.Max() + 1;
            }

            conn.Open();
            string query = $"INSERT INTO country VALUES ({newCountryId}, '{countryName}', '{nowString}', '{user}', '{nowString}', '{user}');";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            var addedCountry = new Country(newCountryId, countryName, now, user, now, user);
            MainScreen.CountryDictionary.Add(newCountryId, addedCountry);

            return newCountryId;
        }

        public static int addCity(string cityName, int countryId, string user)
        {
            DateTime now = DateTime.Now;
            string nowString = now.ToUniversalTime().ToString("yy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);

            int newCityId = 1;
            if (MainScreen.CityDictionary.Count > 0)
            {
                newCityId = MainScreen.CityDictionary.Keys.Max() + 1;
            }

            conn.Open();
            string query = $"INSERT INTO city VALUES ({newCityId}, '{cityName}', {countryId}, '{nowString}', '{user}', '{nowString}', '{user}');";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            var addedCity = new City(newCityId, cityName, countryId, now, user, now, user);
            MainScreen.CityDictionary.Add(newCityId, addedCity);

            return newCityId;
        }
    }
}
