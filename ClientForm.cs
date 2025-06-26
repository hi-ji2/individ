using System;
using System.Windows.Forms;

namespace TourBookingApp
{
    public partial class ClientForm : Form
    {
        private readonly DatabaseHelper dbHelper;
        private readonly int? clientId;

        public ClientForm(DatabaseHelper dbHelper, int? clientId = null)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.clientId = clientId;

            if (clientId.HasValue)
            {
                Text = "Редактирование клиента";
                btnSave.Text = "Обновить";
                LoadClientData();
            }
            else
            {
                Text = "Добавление нового клиента";
                btnSave.Text = "Добавить";
            }
        }

        private void LoadClientData()
        {
            if (!clientId.HasValue) return;

            var clientData = dbHelper.GetClientDetails(clientId.Value);
            if (clientData.Rows.Count == 0)
            {
                MessageBox.Show("Клиент не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            var row = clientData.Rows[0];

            txtFirstName.Text = row["first_name"].ToString();
            txtLastName.Text = row["last_name"].ToString();
            txtPassport.Text = row["passport_number"].ToString();
            txtPhone.Text = row["phone"].ToString();
            txtEmail.Text = row["email"].ToString();

            if (row["birth_date"] != DBNull.Value)
            {
                dtpBirthDate.Value = Convert.ToDateTime(row["birth_date"]);
            }
            else
            {
                dtpBirthDate.Checked = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string passport = txtPassport.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();
                DateTime? birthDate = dtpBirthDate.Checked ? dtpBirthDate.Value : (DateTime?)null;

                if (clientId.HasValue)
                {
                    dbHelper.UpdateClient(clientId.Value, firstName, lastName, passport, phone, email, birthDate);
                    MessageBox.Show("Данные клиента успешно обновлены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dbHelper.AddClient(firstName, lastName, passport, phone, email, birthDate);
                    MessageBox.Show("Клиент успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                MessageBox.Show("Введите номер паспорта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}