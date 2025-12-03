using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Scheduling_App
{
    public partial class ReportsForm : Form
    {
        private readonly Form Main;
        private List<User> ListOfUsers;

        private class AppointmentTypeByMonthRow
        {
            public string Month { get; set; }
            public string Type { get; set; }
            public int Count { get; set; }
        }

        private class ConsultantScheduleRow
        {
            public string Consultant { get; set; }
            public string Customer { get; set; }
            public DateTime Start { get; set; }
        }

        private class CountryCustomerCountRow
        {
            public string Country { get; set; }
            public int CustomerCount { get; set; }
        }

        public ReportsForm(Form form)
        {
            InitializeComponent();
            Main = form;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReportsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.Show();
        }

        private void RunAppointmentTypeReport()
        {
            var from = dateFromPicker.Value.Date;
            var to = dateToPicker.Value.Date.AddDays(1).AddTicks(-1);

            var text = new StringBuilder();
            text.AppendLine();

            var groupedByMonthList = MainScreen.ListOfAppointments
                .OrderBy(appt => appt.Start)
                .Where(appt => appt.Start >= from && appt.Start <= to)
                .GroupBy(appt => appt.Start.ToString("MMMM yyyy"));

            var rows = new List<AppointmentTypeByMonthRow>();

            foreach (var group in groupedByMonthList)
            {
                text.AppendLine(group.Key + ":");
                var groupedByTypeList = group.GroupBy(appt => appt.Type);

                foreach (var list in groupedByTypeList)
                {
                    var count = list.Count();
                    text.AppendLine("\t" + list.Key + ": " + count);
                    rows.Add(new AppointmentTypeByMonthRow
                    {
                        Month = group.Key,
                        Type = list.Key,
                        Count = count
                    });
                }

                text.AppendLine();
            }

            reportTextBox.Text = text.ToString();
            reportDataGridView.DataSource = rows;
        }

        private void RunConsultantScheduleReport()
        {
            var from = dateFromPicker.Value.Date;
            var to = dateToPicker.Value.Date.AddDays(1).AddTicks(-1);

            var text = new StringBuilder();
            text.AppendLine();

            var groupedByConsultantList = MainScreen.ListOfAppointments
                .Where(appt => appt.Start >= from && appt.Start <= to)
                .GroupBy(appt => appt.UserId);

            var rows = new List<ConsultantScheduleRow>();

            foreach (var consultantList in groupedByConsultantList)
            {
                var user = ListOfUsers.FirstOrDefault(u => u.UserID == consultantList.Key);
                var userName = user != null ? user.UserName : "User " + consultantList.Key;

                text.AppendLine(userName + "'s Schedule:");
                foreach (var appt in consultantList.OrderBy(appt => appt.Start))
                {
                    var customer = MainScreen.ListOfCustomers.FirstOrDefault(c => c.CustomerId == appt.CustomerId);
                    var customerName = customer != null ? customer.CustomerName : "Customer " + appt.CustomerId;

                    text.AppendLine(customerName + " - \t" + appt.Start.ToString("dddd M/d/yyyy h:mm tt") + ".");
                    rows.Add(new ConsultantScheduleRow
                    {
                        Consultant = userName,
                        Customer = customerName,
                        Start = appt.Start
                    });
                }

                text.AppendLine();
            }

            reportTextBox.Text = text.ToString();
            reportDataGridView.DataSource = rows;
        }

        private void RunCountriesWithMostCustomersReport()
        {
            var text = new StringBuilder();

            var countryCounts = MainScreen.ListOfCustomers
                .Select(cust =>
                {
                    if (!MainScreen.AddressDictionary.TryGetValue(cust.AddressId, out var address))
                        return null;

                    if (!MainScreen.CityDictionary.TryGetValue(address.CityId, out var city))
                        return null;

                    if (!MainScreen.CountryDictionary.TryGetValue(city.CountryId, out var country))
                        return new { CountryName = "Unknown", CustomerId = cust.CustomerId };

                    return new { CountryName = country.CountryName, CustomerId = cust.CustomerId };
                })
                .Where(x => x != null)
                .GroupBy(x => x.CountryName)
                .Select(g => new { CountryName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            if (countryCounts.Count == 0)
            {
                text.AppendLine("No customers found.");
            }
            else
            {
                foreach (var item in countryCounts)
                {
                    text.AppendLine(item.CountryName + ": " + item.Count + " customers");
                }
            }

            reportTextBox.Text = text.ToString();

            var rows = countryCounts
                .Select(item => new CountryCustomerCountRow
                {
                    Country = item.CountryName,
                    CustomerCount = item.Count
                })
                .ToList();

            reportDataGridView.DataSource = rows;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            ListOfUsers = DatabaseService.getAllUsers();

            var beginningOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endOfMonth = beginningOfMonth.AddMonths(1).AddDays(-1);

            dateFromPicker.Value = beginningOfMonth;
            dateToPicker.Value = endOfMonth;

            reportTypeComboBox.Items.Clear();
            reportTypeComboBox.Items.Add("Number of Appointment Types by Month");
            reportTypeComboBox.Items.Add("Schedule by Consultant");
            reportTypeComboBox.Items.Add("Countries with the Most Customers");

            reportTypeComboBox.SelectedIndex = 0;
            RunCurrentReport();
        }

        private void RunCurrentReport()
        {
            if (reportTypeComboBox.SelectedItem == null)
            {
                reportTextBox.Clear();
                reportDataGridView.DataSource = null;
                return;
            }

            var selection = reportTypeComboBox.SelectedItem.ToString();

            if (selection == "Number of Appointment Types by Month")
            {
                RunAppointmentTypeReport();
            }
            else if (selection == "Schedule by Consultant")
            {
                RunConsultantScheduleReport();
            }
            else if (selection == "Countries with the Most Customers")
            {
                RunCountriesWithMostCustomersReport();
            }
        }

        private void reportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RunCurrentReport();
        }

        private void dateRangePicker_ValueChanged(object sender, EventArgs e)
        {
            RunCurrentReport();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(reportTextBox.Text))
            {
                Clipboard.SetText(reportTextBox.Text);
                MessageBox.Show("Report copied to clipboard.", "Report", MessageBoxButtons.OK);
            }
        }
    }
}
