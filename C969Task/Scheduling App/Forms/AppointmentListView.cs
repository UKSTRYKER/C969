using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Scheduling_App
{
    public partial class AppointmentsForm : Form
    {
        private Form Main;

        public AppointmentsForm(Form main)
        {
            InitializeComponent();
            MainLabel.Text = "Appointments";
            Main = main;
            this.FormClosed += AppointmentsForm_FormClosed;
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime day = e.Start.Date;
            DateTime start = day;
            DateTime end = day.AddDays(1).AddTicks(-1);
            dateTimePickerStartDate.Value = start;
            dateTimePickerEndDate.Value = end;
            appointmentDataGridView.DataSource = getAppointmentsInTimePeriod(start, end);
        }

        private void AppointmentsForm_Load(object sender, EventArgs e)
        {
            displayThisMonth();
            populateComboBoxAppointmentType();
            populateSearchTypeComboBox();
        }

        public void RefreshAppointments()
        {
            DatabaseService.getAppointments();
            displayThisMonth();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RefreshAppointments();
        }

        private void AppointmentsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void formatDataGridView()
        {
            var dg = appointmentDataGridView;
            dg.AutoResizeColumns();
            dg.RowHeadersVisible = false;
            dg.ReadOnly = true;
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dg.MultiSelect = false;
            dg.ClearSelection();
        }

        private void appointmentDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            formatDataGridView();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var addForm = new AppointmentEditorView(this);
            addForm.Show();
            appointmentDataGridView.ClearSelection();
            Hide();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (appointmentDataGridView.SelectedRows.Count < 1)
                    throw new ApplicationException("You must select an appointment to edit.");

                var selectedRow = appointmentDataGridView.SelectedRows[0];
                int selectedId = Convert.ToInt32(selectedRow.Cells[0].Value);

                var editForm = new AppointmentEditorView(this, selectedId);
                editForm.Show();
                appointmentDataGridView.ClearSelection();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (appointmentDataGridView.SelectedRows.Count < 1)
                    throw new ApplicationException("You must select an appointment to delete.");

                var result = MessageBox.Show("Are you sure you want to delete the selected appointment?",
                    "Application Instruction", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var row = appointmentDataGridView.SelectedRows[0];
                    int id = Convert.ToInt32(row.Cells[0].Value);

                    var appointment = MainScreen.ListOfAppointments.FirstOrDefault(a => a.AppointmentId == id);
                    if (appointment != null)
                    {
                        DatabaseService.deleteAppointment(appointment);
                        RefreshAppointments();
                    }

                    appointmentDataGridView.ClearSelection();
                }
                else
                {
                    appointmentDataGridView.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Hide();
            Main.Show();
        }

        private void populateComboBoxAppointmentType()
        {
            comboBoxAppointmentType.DataSource = new[]
            {
                "Strategy Consultation",
                "Project Review Meeting",
                "Client Onboarding Session"
            };
            comboBoxAppointmentType.SelectedItem = null;
        }

        private void displayThisMonth()
        {
            dateTimePickerStartDate.Value = findBeginningOfMonth(DateTime.Now);
            dateTimePickerEndDate.Value = findEndOfMonth(DateTime.Now);

            appointmentDataGridView.DataSource =
                getAppointmentsInTimePeriod(dateTimePickerStartDate.Value, dateTimePickerEndDate.Value);
        }

        public void UpdateSelection()
        {
            RefreshAppointments();
        }

        private DateTime findBeginningOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        private DateTime findEndOfMonth(DateTime date)
        {
            return findBeginningOfMonth(date).AddMonths(1).AddMilliseconds(-1);
        }

        private BindingList<AppointmentModel> getAppointmentsInTimePeriod(DateTime begin, DateTime end)
        {
            return new BindingList<AppointmentModel>(
                MainScreen.ListOfAppointments
                    .Where(a => a.Start >= begin && a.End <= end)
                    .ToList());
        }

        private BindingList<AppointmentModel> getAppointmentsByCustomerId(int id)
        {
            return new BindingList<AppointmentModel>(
                MainScreen.ListOfAppointments
                    .Where(a => a.CustomerId == id)
                    .ToList());
        }

        private BindingList<AppointmentModel> getAppointmentsByAppointmentType(string type)
        {
            return new BindingList<AppointmentModel>(
                MainScreen.ListOfAppointments
                    .Where(a => a.Type == type)
                    .ToList());
        }

        private void populateSearchTypeComboBox()
        {
            searchTypeComboBox.Items.Clear();
            searchTypeComboBox.Items.Add("Customer ID");
            searchTypeComboBox.Items.Add("Appointment Type");
            searchTypeComboBox.Items.Add("Dates");
            searchTypeComboBox.SelectedIndex = 0;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            var mode = searchTypeComboBox.SelectedItem as string;

            if (mode == "Customer ID")
                updateViewOnCustomerID();
            else if (mode == "Appointment Type")
                updateViewOnAppointmentType();
            else if (mode == "Dates")
                updateViewOnDate();
        }

        private void searchTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mode = searchTypeComboBox.SelectedItem as string;

            bool byCustomer = mode == "Customer ID";
            bool byType = mode == "Appointment Type";
            bool byDates = mode == "Dates";

            CustomerIDlabel.Visible = byCustomer;
            CustomerIDtextbox.Visible = byCustomer;

            AppointmentTypeLabel.Visible = byType;
            comboBoxAppointmentType.Visible = byType;

            StartDateLabel.Visible = byDates;
            EndDateLabel.Visible = byDates;
            dateTimePickerStartDate.Visible = byDates;
            dateTimePickerEndDate.Visible = byDates;
        }

        private void updateViewOnCustomerID()
        {
            if (!int.TryParse(CustomerIDtextbox.Text, out int id) || id < 1)
            {
                MessageBox.Show("Please enter a valid customer ID.");
                return;
            }

            var appointments = getAppointmentsByCustomerId(id);

            if (appointments.Count == 0)
            {
                MessageBox.Show("No appointments found for this customer.");
                return;
            }

            appointmentDataGridView.DataSource = appointments;
        }

        private void updateViewOnAppointmentType()
        {
            var appts = getAppointmentsByAppointmentType(comboBoxAppointmentType.Text);

            if (appts.Count == 0)
            {
                MessageBox.Show("You have no appointments of this type.");
                return;
            }

            appointmentDataGridView.DataSource = appts;
        }

        private void updateViewOnDate()
        {
            var start = dateTimePickerStartDate.Value;
            var end = dateTimePickerEndDate.Value.AddMilliseconds(1);

            if (start > end)
            {
                MessageBox.Show("Start date cannot be after end date.");
                return;
            }

            var appts = getAppointmentsInTimePeriod(start, end);

            if (appts.Count == 0)
            {
                MessageBox.Show("No appointments found for this date range.");
                return;
            }

            appointmentDataGridView.DataSource = appts;
        }

        private void CustomerIDlabel_Click(object sender, EventArgs e)
        {

        }
    }
}
