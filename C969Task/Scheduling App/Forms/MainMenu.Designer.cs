namespace Scheduling_App
{
    partial class MainScreen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.customersButton = new System.Windows.Forms.Button();
            this.appointmentsButton = new System.Windows.Forms.Button();
            this.reportsButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(71, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Main Menu";
            this.label1.Click += new System.EventHandler(this.label1_Click);

            this.customersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customersButton.Font = new System.Drawing.Font("Arial", 8.25F);
            this.customersButton.Location = new System.Drawing.Point(34, 115);
            this.customersButton.Name = "customersButton";
            this.customersButton.Size = new System.Drawing.Size(214, 52);
            this.customersButton.TabIndex = 1;
            this.customersButton.Text = "Customers";
            this.customersButton.UseVisualStyleBackColor = true;
            this.customersButton.Click += new System.EventHandler(this.customersButton_Click);

            this.appointmentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appointmentsButton.Font = new System.Drawing.Font("Arial", 8.25F);
            this.appointmentsButton.Location = new System.Drawing.Point(34, 187);
            this.appointmentsButton.Name = "appointmentsButton";
            this.appointmentsButton.Size = new System.Drawing.Size(214, 52);
            this.appointmentsButton.TabIndex = 1;
            this.appointmentsButton.Text = "Appointments";
            this.appointmentsButton.UseVisualStyleBackColor = true;
            this.appointmentsButton.Click += new System.EventHandler(this.appointmentsButton_Click);

            this.reportsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportsButton.Font = new System.Drawing.Font("Arial", 8.25F);
            this.reportsButton.Location = new System.Drawing.Point(34, 265);
            this.reportsButton.Name = "reportsButton";
            this.reportsButton.Size = new System.Drawing.Size(214, 52);
            this.reportsButton.TabIndex = 1;
            this.reportsButton.Text = "Reports";
            this.reportsButton.UseVisualStyleBackColor = true;
            this.reportsButton.Click += new System.EventHandler(this.reportsButton_Click);

            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Arial", 8.25F);
            this.exitButton.Location = new System.Drawing.Point(34, 344);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(214, 52);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(285, 450);

            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.reportsButton);
            this.Controls.Add(this.appointmentsButton);
            this.Controls.Add(this.customersButton);
            this.Controls.Add(this.label1);

            this.Name = "MainScreen";
            this.Text = "Main Menu Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainScreen_FormClosed);
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.Shown += new System.EventHandler(this.MainScreen_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button customersButton;
        private System.Windows.Forms.Button appointmentsButton;
        private System.Windows.Forms.Button reportsButton;
        private System.Windows.Forms.Button exitButton;
    }
}
