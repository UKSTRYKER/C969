namespace Scheduling_App
{
    partial class LoginView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label LoginScreenLabel;
        private System.Windows.Forms.TextBox UnameTextBox;
        private System.Windows.Forms.TextBox PwdTextBox;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label PwdLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.ComboBox LanguageComboBox;
        private System.Windows.Forms.Label LanguageLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.LoginScreenLabel = new System.Windows.Forms.Label();
            this.UnameTextBox = new System.Windows.Forms.TextBox();
            this.PwdTextBox = new System.Windows.Forms.TextBox();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PwdLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.LanguageComboBox = new System.Windows.Forms.ComboBox();
            this.LanguageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoginScreenLabel
            // 
            this.LoginScreenLabel.AutoSize = true;
            this.LoginScreenLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginScreenLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginScreenLabel.Location = new System.Drawing.Point(40, 40);
            this.LoginScreenLabel.Name = "LoginScreenLabel";
            this.LoginScreenLabel.Size = new System.Drawing.Size(216, 31);
            this.LoginScreenLabel.TabIndex = 0;
            this.LoginScreenLabel.Text = "Consulting Login";
            this.LoginScreenLabel.Click += new System.EventHandler(this.LoginScreenLabel_Click);
            // 
            // UnameTextBox
            // 
            this.UnameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnameTextBox.Location = new System.Drawing.Point(40, 140);
            this.UnameTextBox.Name = "UnameTextBox";
            this.UnameTextBox.Size = new System.Drawing.Size(220, 20);
            this.UnameTextBox.TabIndex = 1;
            this.UnameTextBox.TextChanged += new System.EventHandler(this.UnameTextBox_TextChanged);
            // 
            // PwdTextBox
            // 
            this.PwdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PwdTextBox.Location = new System.Drawing.Point(40, 200);
            this.PwdTextBox.Name = "PwdTextBox";
            this.PwdTextBox.PasswordChar = '*';
            this.PwdTextBox.Size = new System.Drawing.Size(220, 20);
            this.PwdTextBox.TabIndex = 2;
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(40, 268);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(90, 30);
            this.LoginBtn.TabIndex = 3;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(170, 268);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(90, 30);
            this.ExitBtn.TabIndex = 4;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(37, 120);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 5;
            this.UsernameLabel.Text = "Username";
            // 
            // PwdLabel
            // 
            this.PwdLabel.AutoSize = true;
            this.PwdLabel.Location = new System.Drawing.Point(37, 180);
            this.PwdLabel.Name = "PwdLabel";
            this.PwdLabel.Size = new System.Drawing.Size(53, 13);
            this.PwdLabel.TabIndex = 6;
            this.PwdLabel.Text = "Password";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(37, 300);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.ErrorLabel.TabIndex = 7;
            // 
            // LocationLabel
            // 
            this.LocationLabel.AutoSize = true;
            this.LocationLabel.Location = new System.Drawing.Point(37, 90);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(57, 13);
            this.LocationLabel.TabIndex = 8;
            this.LocationLabel.Text = "Location: -";
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageComboBox.FormattingEnabled = true;
            this.LanguageComboBox.Location = new System.Drawing.Point(110, 235);
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.Size = new System.Drawing.Size(150, 21);
            this.LanguageComboBox.TabIndex = 0;
            this.LanguageComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_SelectedIndexChanged);
            // 
            // LanguageLabel
            // 
            this.LanguageLabel.AutoSize = true;
            this.LanguageLabel.Location = new System.Drawing.Point(37, 239);
            this.LanguageLabel.Name = "LanguageLabel";
            this.LanguageLabel.Size = new System.Drawing.Size(55, 13);
            this.LanguageLabel.TabIndex = 9;
            this.LanguageLabel.Text = "Language";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 341);
            this.Controls.Add(this.LanguageLabel);
            this.Controls.Add(this.LanguageComboBox);
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.PwdLabel);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.PwdTextBox);
            this.Controls.Add(this.UnameTextBox);
            this.Controls.Add(this.LoginScreenLabel);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
