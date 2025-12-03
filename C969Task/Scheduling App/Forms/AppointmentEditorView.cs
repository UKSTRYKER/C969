using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Scheduling_App
{
    public partial class AppointmentEditorView : Form
    {
        private AppointmentsForm PreviousForm;
        private int SelectedAppointmentID = -1;

        public AppointmentEditorView(AppointmentsForm prevForm, int appointmentId)
        {
            InitializeComponent();
            PreviousForm = prevForm;
            SelectedAppointmentID = appointmentId;
        }

        public AppointmentEditorView(AppointmentsForm prevForm)
        {
            InitializeComponent();
            PreviousForm = prevForm;
        }

        private void GetLocalBusinessBounds(DateTime date, out TimeSpan minTime, out TimeSpan maxTime)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            DateTime estOpen = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            DateTime estClose = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0);

            DateTime localOpen = TimeZoneInfo.ConvertTime(estOpen, estZone, localZone);
            DateTime localClose = TimeZoneInfo.ConvertTime(estClose, estZone, localZone);

            minTime = localOpen.TimeOfDay;
            maxTime = localClose.TimeOfDay;
        }

        private string FormatTime(TimeSpan time)
        {
            return DateTime.Today.Date.Add(time).ToShortTimeString();
        }

        private List<string> GenerateTimeSlots(DateTime date)
        {
            TimeSpan minTime;
            TimeSpan maxTime;
            GetLocalBusinessBounds(date, out minTime, out maxTime);

            var slots = new List<string>();
            DateTime current = date.Date.Add(minTime);
            DateTime end = date.Date.Add(maxTime);

            while (current <= end)
            {
                slots.Add(current.ToShortTimeString());
                current = current.AddMinutes(15);
            }

            return slots;
        }

        private void PopulateStartTimeComboBox(DateTime date)
        {
            var slots = GenerateTimeSlots(date);
            startTimeComboBox.DataSource = slots;
        }

        private void PopulateEndTimeComboBox(DateTime date, TimeSpan? minStart)
        {
            var slots = GenerateTimeSlots(date);

            if (minStart.HasValue)
            {
                string minStartText = FormatTime(minStart.Value);
                int index = slots.FindIndex(s => s == minStartText);
                if (index >= 0 && index < slots.Count - 1)
                {
                    slots = slots.Skip(index + 1).ToList();
                }
            }

            endTimeComboBox.DataSource = slots;
        }

        private TimeSpan ParseTime(string timeText)
        {
            if (string.IsNullOrWhiteSpace(timeText))
            {
                throw new ApplicationException("You must select both a start and end time.");
            }

            DateTime dt;
            if (!DateTime.TryParse(timeText, out dt))
            {
                throw new ApplicationException("Invalid time selection.");
            }

            return dt.TimeOfDay;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerComboBox.SelectedValue == null)
                {
                    throw new ApplicationException("A customer must be selected.");
                }

                if (typeComboBox.SelectedItem == null)
                {
                    throw new ApplicationException("You must select an appointment Type.");
                }

                int selectedCustomerId = Convert.ToInt32(customerComboBox.SelectedValue);
                string selectedType = typeComboBox.SelectedItem.ToString();

                DateTime selectedDate = datePicker.Value.Date;

                if (startTimeComboBox.SelectedItem == null || endTimeComboBox.SelectedItem == null)
                {
                    throw new ApplicationException("You must select both a start and end time.");
                }

                TimeSpan startTime = ParseTime(startTimeComboBox.SelectedItem.ToString());
                TimeSpan endTime = ParseTime(endTimeComboBox.SelectedItem.ToString());

                DateTime selectedStart = selectedDate.Add(startTime);
                DateTime selectedEnd = selectedDate.Add(endTime);

                if (selectedStart >= selectedEnd)
                {
                    throw new ApplicationException("The end time must be after the start time.");
                }

                TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime startEst = TimeZoneInfo.ConvertTime(selectedStart, estZone);
                DateTime endEst = TimeZoneInfo.ConvertTime(selectedEnd, estZone);

                TimeSpan businessOpen = new TimeSpan(9, 0, 0);
                TimeSpan businessClose = new TimeSpan(17, 0, 0);

                if (startEst.DayOfWeek == DayOfWeek.Saturday || startEst.DayOfWeek == DayOfWeek.Sunday ||
                    endEst.DayOfWeek == DayOfWeek.Saturday || endEst.DayOfWeek == DayOfWeek.Sunday)
                {
                    throw new ApplicationException("Appointments must be scheduled Monday through Friday (Eastern Time).");
                }

                if (startEst.Date != endEst.Date)
                {
                    throw new ApplicationException("Appointments must start and end on the same business day.");
                }

                if (startEst.TimeOfDay < businessOpen || endEst.TimeOfDay > businessClose)
                {
                    throw new ApplicationException("You cannot schedule an appointment outside of business hours, 9:00 a.m. – 5:00 p.m. Eastern Time.");
                }

                bool overlapping = MainScreen.ListOfAppointments.Any(appt =>
                    appt.UserId == MainScreen.LoggedInUser.UserID &&
                    appt.AppointmentId != SelectedAppointmentID &&
                    selectedStart < appt.End &&
                    selectedEnd > appt.Start);

                if (overlapping)
                {
                    throw new ApplicationException("You cannot schedule an appointment that overlaps another appointment.");
                }

                if (SelectedAppointmentID >= 0)
                {
                    var appointment = MainScreen.ListOfAppointments
                        .Single(appt => appt.AppointmentId == SelectedAppointmentID);

                    DatabaseService.updateAppointment(appointment, selectedCustomerId, selectedType, selectedStart, selectedEnd);
                }
                else
                {
                    DatabaseService.addAppointment(selectedCustomerId, selectedType, selectedStart, selectedEnd);
                }

                DatabaseService.getAppointments();
                Close();
            }
            catch (ApplicationException err)
            {
                MessageBox.Show(err.Message, "Instructions", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = datePicker.Value.Date;
            PopulateStartTimeComboBox(date);
            if (startTimeComboBox.Items.Count > 0)
            {
                startTimeComboBox.SelectedIndex = 0;
            }

            if (startTimeComboBox.SelectedItem != null)
            {
                TimeSpan startTime = ParseTime(startTimeComboBox.SelectedItem.ToString());
                PopulateEndTimeComboBox(date, startTime);
                if (endTimeComboBox.Items.Count > 0)
                {
                    endTimeComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                PopulateEndTimeComboBox(date, null);
            }
        }

        private void startTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startTimeComboBox.SelectedItem == null)
            {
                return;
            }

            DateTime date = datePicker.Value.Date;
            TimeSpan startTime = ParseTime(startTimeComboBox.SelectedItem.ToString());
            object previousEndSelection = endTimeComboBox.SelectedItem;

            PopulateEndTimeComboBox(date, startTime);

            if (previousEndSelection != null && endTimeComboBox.Items.Contains(previousEndSelection))
            {
                endTimeComboBox.SelectedItem = previousEndSelection;
            }
            else if (endTimeComboBox.Items.Count > 0)
            {
                endTimeComboBox.SelectedIndex = 0;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AppointmentAddEditForm_Load(object sender, EventArgs e)
        {
            customerComboBox.DataSource = null;
            customerComboBox.DisplayMember = "CustomerName";
            customerComboBox.ValueMember = "CustomerId";
            customerComboBox.DataSource = MainScreen.ListOfCustomers;
            customerComboBox.SelectedItem = null;

            typeComboBox.DataSource = new[]
            {
                "Strategy Consultation",
                "Project Review Meeting",
                "Client Onboarding Session"
            };
            typeComboBox.SelectedItem = null;

            if (SelectedAppointmentID >= 0)
            {
                titleLabel.Text = "Edit Appointment";

                var appointment = MainScreen.ListOfAppointments
                    .Single(appt => appt.AppointmentId == SelectedAppointmentID);

                idTextBox.Text = appointment.AppointmentId.ToString();

                customerComboBox.SelectedValue = appointment.CustomerId;
                typeComboBox.SelectedItem = appointment.Type;

                DateTime date = appointment.Start.Date;
                datePicker.Value = date;

                PopulateStartTimeComboBox(date);

                string startText = FormatTime(appointment.Start.TimeOfDay);
                if (startTimeComboBox.Items.Contains(startText))
                {
                    startTimeComboBox.SelectedItem = startText;
                }
                else if (startTimeComboBox.Items.Count > 0)
                {
                    startTimeComboBox.SelectedIndex = 0;
                }

                TimeSpan startTime = ParseTime(startTimeComboBox.SelectedItem.ToString());
                PopulateEndTimeComboBox(date, startTime);

                string endText = FormatTime(appointment.End.TimeOfDay);
                if (endTimeComboBox.Items.Contains(endText))
                {
                    endTimeComboBox.SelectedItem = endText;
                }
                else if (endTimeComboBox.Items.Count > 0)
                {
                    endTimeComboBox.SelectedIndex = endTimeComboBox.Items.Count - 1;
                }
            }
            else
            {
                DateTime today = DateTime.Today;
                datePicker.Value = today;

                PopulateStartTimeComboBox(today);
                if (startTimeComboBox.Items.Count > 0)
                {
                    startTimeComboBox.SelectedIndex = 0;
                }

                if (startTimeComboBox.SelectedItem != null)
                {
                    TimeSpan startTime = ParseTime(startTimeComboBox.SelectedItem.ToString());
                    PopulateEndTimeComboBox(today, startTime);
                    if (endTimeComboBox.Items.Count > 0)
                    {
                        endTimeComboBox.SelectedIndex = 0;
                    }
                }
                else
                {
                    PopulateEndTimeComboBox(today, null);
                }
            }
        }

        private void AppointmentAddEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PreviousForm.UpdateSelection();
            PreviousForm.Show();
        }

        private void customerComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void typeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
