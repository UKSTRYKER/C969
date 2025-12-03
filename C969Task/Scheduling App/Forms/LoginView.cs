using System;
using System.Globalization;
using System.Windows.Forms;

namespace Scheduling_App
{
    public partial class LoginView : Form
    {
        private enum LoginError
        {
            None,
            MissingCredentials,
            InvalidCredentials
        }

        private string currentLanguage = "en";
        private string regionName = "";
        private LoginError lastError = LoginError.None;

        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            var culture = CultureInfo.CurrentCulture;
            var twoLetter = culture.TwoLetterISOLanguageName;
            currentLanguage = twoLetter == "es" ? "es" : "en";
            var region = new RegionInfo(culture.Name);
            regionName = region.EnglishName;
            LanguageComboBox.Items.Clear();
            LanguageComboBox.Items.Add("English");
            LanguageComboBox.Items.Add("Español");
            LanguageComboBox.SelectedIndex = currentLanguage == "es" ? 1 : 0;
            UpdateLocalizedTexts();
            UpdateErrorLabel();
        }

        private void UpdateLocalizedTexts()
        {
            if (currentLanguage == "es")
            {
                LoginScreenLabel.Text = "Inicio de sesión";
                UsernameLabel.Text = "Usuario";
                PwdLabel.Text = "Contraseña";
                LoginBtn.Text = "Iniciar sesión";
                ExitBtn.Text = "Salir";
                LanguageLabel.Text = "Idioma";
                LocationLabel.Text = "Ubicación: " + regionName;
            }
            else
            {
                LoginScreenLabel.Text = "Consulting Login";
                UsernameLabel.Text = "Username";
                PwdLabel.Text = "Password";
                LoginBtn.Text = "Login";
                ExitBtn.Text = "Exit";
                LanguageLabel.Text = "Language";
                LocationLabel.Text = "Location: " + regionName;
            }
        }

        private void UpdateErrorLabel()
        {
            switch (lastError)
            {
                case LoginError.None:
                    ErrorLabel.Text = string.Empty;
                    break;
                case LoginError.MissingCredentials:
                    ErrorLabel.Text = currentLanguage == "es"
                        ? "Debe ingresar un usuario y una contraseña."
                        : "You must enter a username and password.";
                    break;
                case LoginError.InvalidCredentials:
                    ErrorLabel.Text = currentLanguage == "es"
                        ? "El usuario y la contraseña no coinciden."
                        : "The username and password do not match.";
                    break;
            }
        }

        private void LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LanguageComboBox.SelectedIndex == 1)
            {
                currentLanguage = "es";
            }
            else
            {
                currentLanguage = "en";
            }
            UpdateLocalizedTexts();
            UpdateErrorLabel();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            lastError = LoginError.None;
            var userName = UnameTextBox.Text.Trim();
            var password = PwdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                lastError = LoginError.MissingCredentials;
                UpdateErrorLabel();
                return;
            }

            if (userName != "test" || password != "test")
            {
                lastError = LoginError.InvalidCredentials;
                UpdateErrorLabel();
                return;
            }

            lastError = LoginError.None;
            UpdateErrorLabel();

            var now = DateTime.UtcNow;
            var user = new User(1, "test", "test", 1, now, "system", now, "system");
            LoginActivityLogger.logActivity(user);
            var mainScreen = new MainScreen(user);
            mainScreen.Show();
            Hide();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UnameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoginScreenLabel_Click(object sender, EventArgs e)
        {
        }
    }
}
