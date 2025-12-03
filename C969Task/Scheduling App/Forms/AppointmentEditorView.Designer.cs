namespace Scheduling_App
{
    partial class AppointmentEditorView
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox customerComboBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox startTimeComboBox;
        private System.Windows.Forms.ComboBox endTimeComboBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.customerLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.customerComboBox = new System.Windows.Forms.ComboBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.startTimeComboBox = new System.Windows.Forms.ComboBox();
            this.endTimeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.titleLabel.Location = new System.Drawing.Point(64, 21);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(197, 29);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Add Appointment";
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(28, 75);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(18, 13);
            this.idLabel.TabIndex = 1;
            this.idLabel.Text = "ID";
            this.customerLabel.AutoSize = true;
            this.customerLabel.Location = new System.Drawing.Point(28, 119);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(51, 13);
            this.customerLabel.TabIndex = 2;
            this.customerLabel.Text = "Customer";
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(28, 170);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(31, 13);
            this.typeLabel.TabIndex = 3;
            this.typeLabel.Text = "Type";
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(28, 225);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(29, 13);
            this.startLabel.TabIndex = 4;
            this.startLabel.Text = "Start";
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(28, 275);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(26, 13);
            this.endLabel.TabIndex = 5;
            this.endLabel.Text = "End";
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(90, 72);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 6;
            this.saveButton.Location = new System.Drawing.Point(130, 366);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(95, 27);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button1_Click);
            this.cancelButton.Location = new System.Drawing.Point(195, 411);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(95, 27);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.Location = new System.Drawing.Point(89, 111);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(200, 21);
            this.customerComboBox.TabIndex = 13;
            this.customerComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.customerComboBox_KeyPress);
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(89, 167);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(201, 21);
            this.typeComboBox.TabIndex = 14;
            this.typeComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.typeComboBox_KeyPress);
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(90, 319);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 20);
            this.datePicker.TabIndex = 4;
            this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 319);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Date";
            this.startTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startTimeComboBox.FormattingEnabled = true;
            this.startTimeComboBox.Location = new System.Drawing.Point(89, 218);
            this.startTimeComboBox.Name = "startTimeComboBox";
            this.startTimeComboBox.Size = new System.Drawing.Size(200, 21);
            this.startTimeComboBox.TabIndex = 2;
            this.startTimeComboBox.SelectedIndexChanged += new System.EventHandler(this.startTimeComboBox_SelectedIndexChanged);
            this.endTimeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endTimeComboBox.FormattingEnabled = true;
            this.endTimeComboBox.Location = new System.Drawing.Point(90, 269);
            this.endTimeComboBox.Name = "endTimeComboBox";
            this.endTimeComboBox.Size = new System.Drawing.Size(200, 21);
            this.endTimeComboBox.TabIndex = 3;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 450);
            this.Controls.Add(this.endTimeComboBox);
            this.Controls.Add(this.startTimeComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.customerComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.customerLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "AppointmentAddEditForm";
            this.Text = "Add or Edit Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AppointmentAddEditForm_FormClosed);
            this.Load += new System.EventHandler(this.AppointmentAddEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
