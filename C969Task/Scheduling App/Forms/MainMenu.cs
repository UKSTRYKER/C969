using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Scheduling_App
{
    public partial class MainScreen : Form
    {
        public static User LoggedInUser;
        public static BindingList<CustomerModel> ListOfCustomers = new BindingList<CustomerModel>();
        public static BindingList<AppointmentModel> ListOfAppointments = new BindingList<AppointmentModel>();
        public static Dictionary<int, Address> AddressDictionary = new Dictionary<int, Address>();
        public static Dictionary<int, City> CityDictionary = new Dictionary<int, City>();
        public static Dictionary<int, Country> CountryDictionary = new Dictionary<int, Country>();

        public MainScreen(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            DatabaseService.getCustomers();
            DatabaseService.getAddresses();
            DatabaseService.getCities();
            DatabaseService.getCountries();
            DatabaseService.getAppointments();
        }

        private void customersButton_Click(object sender, EventArgs e)
        {
            new CustomerEditorView(this).Show();
            Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void appointmentsButton_Click(object sender, EventArgs e)
        {
            new AppointmentsForm(this).Show();
            Hide();
        }

        private void reportsButton_Click(object sender, EventArgs e)
        {
            new ReportsForm(this).Show();
            Hide();
        }

        private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainScreen_Shown(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var soon = ListOfAppointments
                .Where(appt =>
                {
                    var timeLeft = appt.Start - now;
                    return timeLeft > TimeSpan.Zero && timeLeft <= TimeSpan.FromMinutes(15);
                })
                .ToList();

            if (soon.Count > 0)
            {
                var appointment = soon.First();
                var customer = ListOfCustomers.Single(c => c.CustomerId == appointment.CustomerId);

                MessageBox.Show(
                    $"You have an appointment with {customer.CustomerName} at {appointment.Start:h:mm tt}.",
                    "Upcoming Appointment",
                    MessageBoxButtons.OK
                );
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
