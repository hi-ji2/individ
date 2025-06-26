using System;
using System.Data;
using System.Windows.Forms;

namespace TourBookingApp
{
    public partial class BookingForm : Form
    {
        private readonly DatabaseHelper dbHelper;
        private DataTable tours;
        private DataTable clients;

        public BookingForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка активных туров
            tours = dbHelper.GetActiveTours();
            cmbTour.DisplayMember = "tour_name";
            cmbTour.ValueMember = "tour_id";
            cmbTour.DataSource = tours;

            // Загрузка клиентов
            clients = dbHelper.GetAllClients();
            cmbClient.DisplayMember = "full_name";
            cmbClient.ValueMember = "client_id";

            // Создаем вычисляемое поле для отображения полного имени
            clients.Columns.Add("full_name", typeof(string), "first_name + ' ' + last_name");
            cmbClient.DataSource = clients;

            // Установка начальных значений
            numSeats.Minimum = 1;
            numSeats.Value = 1;
        }

        private void cmbTour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTour.SelectedValue == null) return;

            int tourId = Convert.ToInt32(cmbTour.SelectedValue);
            var tourData = dbHelper.GetTourDetails(tourId);
            if (tourData.Rows.Count > 0)
            {
                var row = tourData.Rows[0];
                lblTourDetails.Text = $"{row["type_name"]} в {row["city_name"]}, {row["country_name"]}\n" +
                                    $"Отель: {row["hotel_name"]}\n" +
                                    $"Даты: {Convert.ToDateTime(row["start_date"]).ToShortDateString()} - {Convert.ToDateTime(row["end_date"]).ToShortDateString()}\n" +
                                    $"Цена за место: {Convert.ToDecimal(row["price"]):C}";

                // Обновляем доступное количество мест
                numSeats.Maximum = Convert.ToInt32(row["available_seats"]);
                UpdateTotalPrice();
            }
        }

        private void numSeats_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            if (cmbTour.SelectedValue == null) return;

            int tourId = Convert.ToInt32(cmbTour.SelectedValue);
            var tourData = dbHelper.GetTourDetails(tourId);
            if (tourData.Rows.Count > 0)
            {
                decimal pricePerSeat = Convert.ToDecimal(tourData.Rows[0]["price"]);
                numTotalPrice.Value = pricePerSeat * numSeats.Value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                int tourId = Convert.ToInt32(cmbTour.SelectedValue);
                int clientId = Convert.ToInt32(cmbClient.SelectedValue);
                int seats = (int)numSeats.Value;
                decimal totalPrice = numTotalPrice.Value;

                dbHelper.AddBooking(tourId, clientId, seats, totalPrice);
                MessageBox.Show("Бронирование успешно создано", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания бронирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cmbTour.SelectedValue == null)
            {
                MessageBox.Show("Выберите тур", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numSeats.Value <= 0)
            {
                MessageBox.Show("Количество мест должно быть больше нуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}