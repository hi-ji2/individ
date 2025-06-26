using System;
using System.Data;
using System.Windows.Forms;

namespace TourBookingApp
{
    public partial class MainForm : Form
    {
        private readonly DatabaseHelper dbHelper;

        // Конструктор с параметром DatabaseHelper
        public MainForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка данных в DataGridView
                dgvTours.DataSource = dbHelper.GetActiveTours();
                dgvClients.DataSource = dbHelper.GetAllClients();
                dgvBookings.DataSource = dbHelper.GetAllBookings();

                // Настройка внешнего вида таблиц
                ConfigureDataGridViews();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridViews()
        {
            // Настройка столбцов для таблицы туров
            if (dgvTours.Columns.Count > 0)
            {
                dgvTours.Columns["tour_id"].Visible = false;
                dgvTours.Columns["type_id"].Visible = false;
                dgvTours.Columns["hotel_id"].Visible = false;
            }

            // Настройка столбцов для таблицы клиентов
            if (dgvClients.Columns.Count > 0)
            {
                dgvClients.Columns["client_id"].Visible = false;
            }

            // Настройка столбцов для таблицы бронирований
            if (dgvBookings.Columns.Count > 0)
            {
                dgvBookings.Columns["booking_id"].Visible = false;
                dgvBookings.Columns["tour_id"].Visible = false;
                dgvBookings.Columns["client_id"].Visible = false;
            }
        }

        private void btnAddTour_Click(object sender, EventArgs e)
        {
            var form = new TourForm(dbHelper);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEditTour_Click(object sender, EventArgs e)
        {
            if (dgvTours.CurrentRow == null) return;

            int tourId = Convert.ToInt32(dgvTours.CurrentRow.Cells["tour_id"].Value);
            var form = new TourForm(dbHelper, tourId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDeleteTour_Click(object sender, EventArgs e)
        {
            if (dgvTours.CurrentRow == null) return;

            int tourId = Convert.ToInt32(dgvTours.CurrentRow.Cells["tour_id"].Value);
            if (MessageBox.Show("Удалить этот тур?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dbHelper.DeleteTour(tourId);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            var form = new ClientForm(dbHelper);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEditClient_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow == null) return;

            int clientId = Convert.ToInt32(dgvClients.CurrentRow.Cells["client_id"].Value);
            var form = new ClientForm(dbHelper, clientId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnAddBooking_Click(object sender, EventArgs e)
        {
            var form = new BookingForm(dbHelper);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null) return;

            int bookingId = Convert.ToInt32(dgvBookings.CurrentRow.Cells["booking_id"].Value);
            if (MessageBox.Show("Отменить это бронирование?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dbHelper.CancelBooking(bookingId);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отмены бронирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            var form = new ReportsForm(dbHelper);
            form.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}