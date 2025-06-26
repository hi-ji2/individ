using System;
using System.Windows.Forms;

namespace TourBookingApp
{
    public partial class LoginForm : Form
    {
        private readonly DatabaseHelper dbHelper;

        public LoginForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (dbHelper.AuthenticateUser(username, password))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка аутентификации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm(dbHelper);
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Регистрация прошла успешно. Теперь вы можете войти.",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

