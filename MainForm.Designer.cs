namespace TourBookingApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTours = new System.Windows.Forms.TabPage();
            this.btnDeleteTour = new System.Windows.Forms.Button();
            this.btnEditTour = new System.Windows.Forms.Button();
            this.btnAddTour = new System.Windows.Forms.Button();
            this.dgvTours = new System.Windows.Forms.DataGridView();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.btnEditClient = new System.Windows.Forms.Button();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.tabBookings = new System.Windows.Forms.TabPage();
            this.btnCancelBooking = new System.Windows.Forms.Button();
            this.btnAddBooking = new System.Windows.Forms.Button();
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabTours.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTours)).BeginInit();
            this.tabClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            this.tabBookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabTours);
            this.tabControl.Controls.Add(this.tabClients);
            this.tabControl.Controls.Add(this.tabBookings);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 400);
            this.tabControl.TabIndex = 0;
            // 
            // tabTours
            // 
            this.tabTours.Controls.Add(this.btnDeleteTour);
            this.tabTours.Controls.Add(this.btnEditTour);
            this.tabTours.Controls.Add(this.btnAddTour);
            this.tabTours.Controls.Add(this.dgvTours);
            this.tabTours.Location = new System.Drawing.Point(4, 22);
            this.tabTours.Name = "tabTours";
            this.tabTours.Padding = new System.Windows.Forms.Padding(3);
            this.tabTours.Size = new System.Drawing.Size(792, 374);
            this.tabTours.TabIndex = 0;
            this.tabTours.Text = "Туры";
            this.tabTours.UseVisualStyleBackColor = true;
            // 
            // btnDeleteTour
            // 
            this.btnDeleteTour.Location = new System.Drawing.Point(174, 6);
            this.btnDeleteTour.Name = "btnDeleteTour";
            this.btnDeleteTour.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTour.TabIndex = 3;
            this.btnDeleteTour.Text = "Удалить";
            this.btnDeleteTour.UseVisualStyleBackColor = true;
            // 
            // btnEditTour
            // 
            this.btnEditTour.Location = new System.Drawing.Point(93, 6);
            this.btnEditTour.Name = "btnEditTour";
            this.btnEditTour.Size = new System.Drawing.Size(75, 23);
            this.btnEditTour.TabIndex = 2;
            this.btnEditTour.Text = "Изменить";
            this.btnEditTour.UseVisualStyleBackColor = true;
            // 
            // btnAddTour
            // 
            this.btnAddTour.Location = new System.Drawing.Point(6, 6);
            this.btnAddTour.Name = "btnAddTour";
            this.btnAddTour.Size = new System.Drawing.Size(75, 23);
            this.btnAddTour.TabIndex = 1;
            this.btnAddTour.Text = "Добавить";
            this.btnAddTour.UseVisualStyleBackColor = true;
            // 
            // dgvTours
            // 
            this.dgvTours.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTours.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTours.Location = new System.Drawing.Point(6, 35);
            this.dgvTours.Name = "dgvTours";
            this.dgvTours.Size = new System.Drawing.Size(780, 333);
            this.dgvTours.TabIndex = 0;
            // 
            // tabClients
            // 
            this.tabClients.Controls.Add(this.btnEditClient);
            this.tabClients.Controls.Add(this.btnAddClient);
            this.tabClients.Controls.Add(this.dgvClients);
            this.tabClients.Location = new System.Drawing.Point(4, 22);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(792, 374);
            this.tabClients.TabIndex = 1;
            this.tabClients.Text = "Клиенты";
            this.tabClients.UseVisualStyleBackColor = true;
            // 
            // btnEditClient
            // 
            this.btnEditClient.Location = new System.Drawing.Point(93, 6);
            this.btnEditClient.Name = "btnEditClient";
            this.btnEditClient.Size = new System.Drawing.Size(75, 23);
            this.btnEditClient.TabIndex = 2;
            this.btnEditClient.Text = "Изменить";
            this.btnEditClient.UseVisualStyleBackColor = true;
            // 
            // btnAddClient
            // 
            this.btnAddClient.Location = new System.Drawing.Point(6, 6);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(75, 23);
            this.btnAddClient.TabIndex = 1;
            this.btnAddClient.Text = "Добавить";
            this.btnAddClient.UseVisualStyleBackColor = true;
            // 
            // dgvClients
            // 
            this.dgvClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.Location = new System.Drawing.Point(6, 35);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.Size = new System.Drawing.Size(780, 333);
            this.dgvClients.TabIndex = 0;
            // 
            // tabBookings
            // 
            this.tabBookings.Controls.Add(this.btnCancelBooking);
            this.tabBookings.Controls.Add(this.btnAddBooking);
            this.tabBookings.Controls.Add(this.dgvBookings);
            this.tabBookings.Location = new System.Drawing.Point(4, 22);
            this.tabBookings.Name = "tabBookings";
            this.tabBookings.Size = new System.Drawing.Size(792, 374);
            this.tabBookings.TabIndex = 2;
            this.tabBookings.Text = "Бронирования";
            this.tabBookings.UseVisualStyleBackColor = true;
            // 
            // btnCancelBooking
            // 
            this.btnCancelBooking.Location = new System.Drawing.Point(93, 6);
            this.btnCancelBooking.Name = "btnCancelBooking";
            this.btnCancelBooking.Size = new System.Drawing.Size(75, 23);
            this.btnCancelBooking.TabIndex = 2;
            this.btnCancelBooking.Text = "Отменить";
            this.btnCancelBooking.UseVisualStyleBackColor = true;
            // 
            // btnAddBooking
            // 
            this.btnAddBooking.Location = new System.Drawing.Point(6, 6);
            this.btnAddBooking.Name = "btnAddBooking";
            this.btnAddBooking.Size = new System.Drawing.Size(75, 23);
            this.btnAddBooking.TabIndex = 1;
            this.btnAddBooking.Text = "Добавить";
            this.btnAddBooking.UseVisualStyleBackColor = true;
            // 
            // dgvBookings
            // 
            this.dgvBookings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookings.Location = new System.Drawing.Point(6, 35);
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.Size = new System.Drawing.Size(780, 333);
            this.dgvBookings.TabIndex = 0;
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(12, 406);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(100, 23);
            this.btnReports.TabIndex = 1;
            this.btnReports.Text = "Отчеты";
            this.btnReports.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(118, 406);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 441);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.Text = "Учет туристических путевок";
            this.tabControl.ResumeLayout(false);
            this.tabTours.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTours)).EndInit();
            this.tabClients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            this.tabBookings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabTours;
        private System.Windows.Forms.TabPage tabClients;
        private System.Windows.Forms.TabPage tabBookings;
        private System.Windows.Forms.DataGridView dgvTours;
        private System.Windows.Forms.Button btnAddTour;
        private System.Windows.Forms.Button btnEditTour;
        private System.Windows.Forms.Button btnDeleteTour;
        private System.Windows.Forms.DataGridView dgvClients;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.Button btnEditClient;
        private System.Windows.Forms.DataGridView dgvBookings;
        private System.Windows.Forms.Button btnAddBooking;
        private System.Windows.Forms.Button btnCancelBooking;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnRefresh;
    }
}