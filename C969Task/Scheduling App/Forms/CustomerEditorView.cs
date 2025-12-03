using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Scheduling_App
{
    public partial class CustomerEditorView : Form
    {
        private Form Main;

        public CustomerEditorView(Form main)
        {
            InitializeComponent();

            if (MainScreen.ListOfCustomers.Count == 0)
            {
                DatabaseService.getCustomers();
            }

            if (MainScreen.AddressDictionary.Count == 0)
            {
                DatabaseService.getAddresses();
            }

            if (MainScreen.CityDictionary.Count == 0)
            {
                DatabaseService.getCities();
            }

            if (MainScreen.CountryDictionary.Count == 0)
            {
                DatabaseService.getCountries();
            }

            customerDataGridView.DataSource = MainScreen.ListOfCustomers;
            Main = main;
        }

        private void mainLabel_Click(object sender, EventArgs e)
        {
        }

        private void customerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void address2TextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void countryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void zipTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void phoneTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            customerDataGridView.ClearSelection();
            clearInputs();

            var cityList = MainScreen.CityDictionary
                .Select(dict => new KeyValuePair<int, string>(dict.Key, dict.Value.CityName))
                .ToList();

            cityComboBox.DataSource = null;
            cityComboBox.DisplayMember = "Value";
            cityComboBox.ValueMember = "Key";
            cityComboBox.DataSource = cityList;
            cityComboBox.SelectedItem = null;

            ToggleActiveInputs(true);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = nameTextBox.Text.Trim();
                string address1 = addressTextBox.Text.Trim();
                string address2 = address2TextBox.Text.Trim();
                string postalCode = zipTextBox.Text.Trim();
                string phone = phoneTextBox.Text.Trim();

                if (string.IsNullOrEmpty(customerName))
                {
                    throw new ApplicationException("A customer must have a name.");
                }

                if (string.IsNullOrEmpty(address1))
                {
                    throw new ApplicationException("A customer must have an address.");
                }

                if (cityComboBox.SelectedItem == null && string.IsNullOrWhiteSpace(cityComboBox.Text))
                {
                    throw new ApplicationException("You must select or enter a city.");
                }

                if (countryComboBox.SelectedItem == null && string.IsNullOrWhiteSpace(countryComboBox.Text))
                {
                    throw new ApplicationException("You must select or enter a country.");
                }

                if (string.IsNullOrEmpty(postalCode))
                {
                    throw new ApplicationException("A customer must have a zip code.");
                }

                if (string.IsNullOrEmpty(phone))
                {
                    throw new ApplicationException("A customer must have a phone number.");
                }

                if (!phone.All(c => char.IsDigit(c) || c == '-'))
                {
                    throw new ApplicationException("The phone number may contain only digits and dashes.");
                }


                int countryId;

                if (countryComboBox.SelectedItem != null)
                {
                    countryId = Convert.ToInt32(countryComboBox.SelectedValue);
                }
                else
                {
                    string countryName = countryComboBox.Text.Trim();
                    countryId = -1;

                    foreach (var kvp in MainScreen.CountryDictionary)
                    {
                        if (string.Equals(kvp.Value.CountryName, countryName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            countryId = kvp.Key;
                            break;
                        }
                    }

                    if (countryId == -1)
                    {
                        countryId = DatabaseService.addCountry(countryName, MainScreen.LoggedInUser.UserName);
                    }

                    var countryList = MainScreen.CountryDictionary
                        .Select(d => new KeyValuePair<int, string>(d.Key, d.Value.CountryName))
                        .ToList();

                    countryComboBox.DataSource = null;
                    countryComboBox.DisplayMember = "Value";
                    countryComboBox.ValueMember = "Key";
                    countryComboBox.DataSource = countryList;
                    countryComboBox.SelectedValue = countryId;
                }

                int cityID;

                if (cityComboBox.SelectedItem != null)
                {
                    cityID = Convert.ToInt32(cityComboBox.SelectedValue);
                }
                else
                {
                    string cityName = cityComboBox.Text.Trim();
                    cityID = -1;

                    foreach (var kvp in MainScreen.CityDictionary)
                    {
                        if (kvp.Value.CountryId == countryId &&
                            string.Equals(kvp.Value.CityName, cityName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            cityID = kvp.Key;
                            break;
                        }
                    }

                    if (cityID == -1)
                    {
                        cityID = DatabaseService.addCity(cityName, countryId, MainScreen.LoggedInUser.UserName);
                    }

                    var newCityList = MainScreen.CityDictionary
                        .Where(dict => dict.Value.CountryId == countryId)
                        .Select(dict => new KeyValuePair<int, string>(dict.Key, dict.Value.CityName))
                        .ToList();

                    cityComboBox.DataSource = null;
                    cityComboBox.DisplayMember = "Value";
                    cityComboBox.ValueMember = "Key";
                    cityComboBox.DataSource = newCityList;
                    cityComboBox.SelectedValue = cityID;
                }

                int customerID;
                bool isEditMode = customerDataGridView.SelectedRows.Count > 0;

                if (!isEditMode)
                {
                    int addressID = DatabaseService.addAddress(
                        address1,
                        address2,
                        cityID,
                        postalCode,
                        phone,
                        MainScreen.LoggedInUser.UserName);

                    customerID = DatabaseService.addCustomer(
                        customerName,
                        addressID,
                        MainScreen.LoggedInUser.UserName);

                    idTextBox.Text = customerID.ToString();
                }
                else
                {
                    if (!int.TryParse(idTextBox.Text, out customerID))
                    {
                        var row = customerDataGridView.SelectedRows[0];
                        customerID = Convert.ToInt32(row.Cells[0].Value);
                        idTextBox.Text = customerID.ToString();
                    }

                    CustomerModel currentCustomer = MainScreen.ListOfCustomers
                        .Single(c => c.CustomerId == customerID);

                    Address address = MainScreen.AddressDictionary[currentCustomer.AddressId];

                    DatabaseService.updateCustomer(currentCustomer, customerName, MainScreen.LoggedInUser.UserName);
                    DatabaseService.updateAddress(address, address1, address2, cityID, postalCode, phone, MainScreen.LoggedInUser.UserName);
                }

                ToggleActiveInputs(false);

                customerDataGridView.Rows
                    .Cast<DataGridViewRow>()
                    .Single(row => Convert.ToInt32(row.Cells[0].Value) == customerID)
                    .Selected = true;
            }
            catch (ApplicationException error)
            {
                MessageBox.Show(error.Message, "Instructions", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerDataGridView.SelectedRows.Count < 1)
                {
                    throw new ApplicationException("You must select a customer to edit.");
                }

                ToggleActiveInputs(true);

                var selectedCountryKey = Convert.ToInt32(countryComboBox.SelectedValue);

                var newCityList = MainScreen.CityDictionary
                    .Where(dict => dict.Value.CountryId == selectedCountryKey)
                    .Select(dict => new KeyValuePair<int, string>(dict.Key, dict.Value.CityName))
                    .ToList();

                cityComboBox.DataSource = null;
                cityComboBox.DisplayMember = "Value";
                cityComboBox.ValueMember = "Key";
                cityComboBox.DataSource = newCityList;
                cityComboBox.SelectedItem = null;
            }
            catch (ApplicationException error)
            {
                MessageBox.Show(error.Message, "Instructions", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            customerDataGridView.ClearSelection();
            clearInputs();
            ToggleActiveInputs(false);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerDataGridView.SelectedRows.Count < 1)
                {
                    throw new ApplicationException("You must select a customer to delete.");
                }

                DialogResult confirmDelete = MessageBox.Show(
                    "Are you sure you want to delete the selected customer?",
                    "Application Instruction",
                    MessageBoxButtons.YesNo);

                if (confirmDelete == DialogResult.Yes)
                {
                    var selectedRow = customerDataGridView.SelectedRows[0];
                    int selectedCustomerId = Convert.ToInt32(selectedRow.Cells[0].Value);

                    bool hasScheduledAppointments = MainScreen.ListOfAppointments
                        .Any(appt => appt.CustomerId == selectedCustomerId);

                    if (hasScheduledAppointments)
                    {
                        throw new ApplicationException("You cannont delete a customer with scheduled appointments.");
                    }

                    CustomerModel selectedCustomer = MainScreen.ListOfCustomers
                        .Single(customer => customer.CustomerId == selectedCustomerId);

                    DatabaseService.deleteCustomer(selectedCustomer);
                    clearInputs();
                }
                else
                {
                    customerDataGridView.ClearSelection();
                    clearInputs();
                }
            }
            catch (ApplicationException error)
            {
                MessageBox.Show(error.Message, "Instructions", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomerRecords_Load(object sender, EventArgs e)
        {
            mainLabel.Text = "Customer Records";

            var countryList = MainScreen.CountryDictionary
                .Select(dict => new KeyValuePair<int, string>(dict.Key, dict.Value.CountryName))
                .ToList();

            countryComboBox.DataSource = null;
            countryComboBox.DisplayMember = "Value";
            countryComboBox.ValueMember = "Key";
            countryComboBox.DataSource = countryList;
            countryComboBox.SelectedItem = null;

            var cityList = MainScreen.CityDictionary
                .Select(dict => new KeyValuePair<int, string>(dict.Key, dict.Value.CityName))
                .ToList();

            cityComboBox.DataSource = null;
            cityComboBox.DisplayMember = "Value";
            cityComboBox.ValueMember = "Key";
            cityComboBox.DataSource = cityList;
            cityComboBox.SelectedItem = null;

            ToggleActiveInputs(false);
        }

        private void CustomerRecords_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.Show();
        }

        private void customDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            formatDataGridView();
        }

        private void formatDataGridView()
        {
            var dataGridView = customerDataGridView;
            dataGridView.AutoResizeColumns();
            dataGridView.RowHeadersVisible = false;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ClearSelection();
        }

        private void clearInputs()
        {
            nameTextBox.Text = "";
            idTextBox.Text = "";
            addressTextBox.Text = "";
            address2TextBox.Text = "";
            zipTextBox.Text = "";
            phoneTextBox.Text = "";
            cityComboBox.Text = "";
            countryComboBox.Text = "";
        }

        private void ToggleActiveInputs(bool active)
        {
            nameTextBox.Enabled = active;
            addressTextBox.Enabled = active;
            address2TextBox.Enabled = active;
            countryComboBox.Enabled = active;
            cityComboBox.Enabled = active;
            zipTextBox.Enabled = active;
            phoneTextBox.Enabled = active;
            cancelButton.Visible = active;
            saveButton.Visible = active;
            addButton.Visible = !active;
            editButton.Visible = !active;
            deleteButton.Visible = !active;
            backButton.Visible = !active;
            customerDataGridView.Enabled = !active;
        }

        private void customDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void customDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var selectedRow = customerDataGridView.SelectedRows[0];
            int selectedCustomerId = Convert.ToInt32(selectedRow.Cells[0].Value);

            CustomerModel selectedCustomer = MainScreen.ListOfCustomers
                .Single(customer => customer.CustomerId == selectedCustomerId);

            int selectedAddressId = selectedCustomer.AddressId;
            int selectedCityId = MainScreen.AddressDictionary[selectedAddressId].CityId;
            int selectedCountryId = MainScreen.CityDictionary[selectedCityId].CountryId;

            nameTextBox.Text = selectedCustomer.CustomerName;
            idTextBox.Text = selectedCustomer.CustomerId.ToString();
            addressTextBox.Text = MainScreen.AddressDictionary[selectedAddressId].AddressLine;
            address2TextBox.Text = MainScreen.AddressDictionary[selectedAddressId].AddressLine2;
            zipTextBox.Text = MainScreen.AddressDictionary[selectedAddressId].PostalCode;
            phoneTextBox.Text = MainScreen.AddressDictionary[selectedAddressId].Phone;
            cityComboBox.Text = MainScreen.CityDictionary[selectedCityId].CityName;
            countryComboBox.Text = MainScreen.CountryDictionary[selectedCountryId].CountryName;
        }

        private void cityComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (countryComboBox.Text == "")
            {
                var selectedCityKey = cityComboBox.SelectedValue;
                int selectedCountryKey = MainScreen.CityDictionary[Convert.ToInt32(selectedCityKey)].CountryId;
                countryComboBox.Text = MainScreen.CountryDictionary[selectedCountryKey].CountryName;
            }
        }

        private void countryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var selectedCountryKey = Convert.ToInt32(countryComboBox.SelectedValue);

            var newCityNameDictionary = MainScreen.CityDictionary
                .Where(dict => dict.Value.CountryId == selectedCountryKey)
                .ToDictionary(dict => dict.Key, dict => dict.Value.CityName);

            cityComboBox.DataSource = new BindingSource(newCityNameDictionary, null);
            cityComboBox.DisplayMember = "Value";
            cityComboBox.ValueMember = "Key";
            cityComboBox.SelectedItem = null;
        }

        private void countryComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cityComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }
    }
}
