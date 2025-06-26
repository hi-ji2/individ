using System;
using System.Data;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TourBookingApp
{
    public partial class TourForm : Form
    {
        private readonly DatabaseHelper dbHelper;
        private readonly int? tourId;
        private DataTable countries;
        private DataTable cities;
        private DataTable hotels;
        private DataTable tourTypes;

        public TourForm(DatabaseHelper dbHelper, int? tourId = null)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            this.tourId = tourId;

            LoadData();

            if (tourId.HasValue)
            {
                Text = "Редактирование тура";
                btnSave.Text = "Обновить";
                LoadTourData();
            }
            else
            {
                Text = "Добавление нового тура";
                btnSave.Text = "Добавить";
            }
        }

        private void LoadData()
        {
            // Загрузка стран
            countries = dbHelper.GetCountries();
            cmbCountry.DisplayMember = "country_name";
            cmbCountry.ValueMember = "country_id";
            cmbCountry.DataSource = countries;

            // Загрузка типов туров
            tourTypes = dbHelper.GetTourTypes();
            cmbType.DisplayMember = "type_name";
            cmbType.ValueMember = "type_id";
            cmbType.DataSource = tourTypes;
        }

        private void LoadTourData()
        {
            if (!tourId.HasValue) return;

            var tourData = dbHelper.GetTourDetails(tourId.Value);
            if (tourData.Rows.Count == 0)
            {
                MessageBox.Show("Тур не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            var row = tourData.Rows[0];

            txtName.Text = row["tour_name"].ToString();
            cmbType.SelectedValue = row["type_id"];

            // Установка страны, города и отеля
            cmbCountry.SelectedValue = GetCountryIdByName(row["country_name"].ToString());
            cmbCity.SelectedValue = GetCityIdByName(row["city_name"].ToString());
            cmbHotel.SelectedValue = GetHotelIdByName(row["hotel_name"].ToString());

            dtpStartDate.Value = Convert.ToDateTime(row["start_date"]);
            dtpEndDate.Value = Convert.ToDateTime(row["end_date"]);
            numPrice.Value = Convert.ToDecimal(row["price"]);
            numSeats.Value = Convert.ToInt32(row["available_seats"]);
        }

        private int GetCountryIdByName(string name)
        {
            foreach (DataRow row in countries.Rows)
            {
                if (row["country_name"].ToString() == name)
                    return Convert.ToInt32(row["country_id"]);
            }
            return -1;
        }

        private int GetCityIdByName(string name)
        {
            foreach (DataRow row in cities.Rows)
            {
                if (row["city_name"].ToString() == name)
                    return Convert.ToInt32(row["city_id"]);
            }
            return -1;
        }

        private int GetHotelIdByName(string name)
        {
            foreach (DataRow row in hotels.Rows)
            {
                if (row["hotel_name"].ToString() == name)
                    return Convert.ToInt32(row["hotel_id"]);
            }
            return -1;
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCountry.SelectedValue == null) return;

            int countryId = Convert.ToInt32(cmbCountry.SelectedValue);
            cities = dbHelper.GetCities(countryId);
            cmbCity.DisplayMember = "city_name";
            cmbCity.ValueMember = "city_id";
            cmbCity.DataSource = cities;
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCity.SelectedValue == null) return;

            int cityId = Convert.ToInt32(cmbCity.SelectedValue);
            hotels = dbHelper.GetHotels(cityId);
            cmbHotel.DisplayMember = "hotel_name";
            cmbHotel.ValueMember = "hotel_id";
            cmbHotel.DataSource = hotels;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                string name = txtName.Text.Trim();
                int typeId = Convert.ToInt32(cmbType.SelectedValue);
                int hotelId = Convert.ToInt32(cmbHotel.SelectedValue);
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                decimal price = numPrice.Value;
                int seats = (int)numSeats.Value;

                if (tourId.HasValue)
                {
                    dbHelper.UpdateTour(tourId.Value, name, typeId, hotelId, startDate, endDate, price, seats, true);
                    MessageBox.Show("Тур успешно обновлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dbHelper.AddTour(name, typeId, hotelId, startDate, endDate, price, seats);
                    MessageBox.Show("Тур успешно добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название тура", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpEndDate.Value <= dtpStartDate.Value)
            {
                MessageBox.Show("Дата окончания должна быть позже даты начала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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