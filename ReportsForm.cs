using System.Data;
using System.Windows.Forms;

namespace TourBookingApp
{
    public partial class ReportsForm : Form
    {
        private readonly DatabaseHelper dbHelper;

        public ReportsForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            this.dbHelper = dbHelper;
            LoadReports();
        }

        private void LoadReports()
        {
            // Загрузка отчета по доходам по странам
            var revenueByCountry = dbHelper.GetRevenueByCountry();
            dgvRevenueByCountry.DataSource = revenueByCountry;

            // Настройка внешнего вида таблиц
            ConfigureDataGridViews();
        }

        private void ConfigureDataGridViews()
        {
            // Настройка столбцов для таблицы доходов по странам
            if (dgvRevenueByCountry.Columns.Count > 0)
            {
                dgvRevenueByCountry.Columns["country_name"].HeaderText = "Страна";
                dgvRevenueByCountry.Columns["total_revenue"].HeaderText = "Общий доход";
                dgvRevenueByCountry.Columns["total_revenue"].DefaultCellStyle.Format = "C";
            }
        }
    }
}