
namespace _202001359_ex1_basic
{
    partial class bookingcheck
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btDelete = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.comTime = new System.Windows.Forms.ComboBox();
            this.MovieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.Seat = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.BookingTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Theater = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.movieName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SeatNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.MovieChart)).BeginInit();
            this.SuspendLayout();
            // 
            // btDelete
            // 
            this.btDelete.Enabled = false;
            this.btDelete.Location = new System.Drawing.Point(460, 290);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(113, 112);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "취소하기";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.button2_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Enabled = false;
            this.btUpdate.Location = new System.Drawing.Point(635, 330);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(167, 49);
            this.btUpdate.TabIndex = 6;
            this.btUpdate.Text = "시간 변경하기";
            this.btUpdate.UseVisualStyleBackColor = true;
            this.btUpdate.Click += new System.EventHandler(this.button1_Click);
            // 
            // comTime
            // 
            this.comTime.Enabled = false;
            this.comTime.FormattingEnabled = true;
            this.comTime.Location = new System.Drawing.Point(653, 301);
            this.comTime.Name = "comTime";
            this.comTime.Size = new System.Drawing.Size(121, 23);
            this.comTime.TabIndex = 8;
            this.comTime.TextChanged += new System.EventHandler(this.comTime_TextChanged);
            // 
            // MovieChart
            // 
            chartArea6.Name = "ChartArea1";
            this.MovieChart.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.MovieChart.Legends.Add(legend6);
            this.MovieChart.Location = new System.Drawing.Point(53, 12);
            this.MovieChart.Name = "MovieChart";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series6.IsVisibleInLegend = false;
            series6.Legend = "Legend1";
            series6.MarkerSize = 1;
            series6.Name = "점유율";
            this.MovieChart.Series.Add(series6);
            this.MovieChart.Size = new System.Drawing.Size(390, 300);
            this.MovieChart.TabIndex = 9;
            title6.Name = "영화 예매 현황 차트";
            title6.Text = "영화 예매 현황 차트";
            this.MovieChart.Titles.Add(title6);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 46);
            this.button1.TabIndex = 10;
            this.button1.Text = "예매 현황 파일에 저장";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Seat
            // 
            this.Seat.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Seat.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Seat.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Seat.Location = new System.Drawing.Point(50, 327);
            this.Seat.Name = "Seat";
            this.Seat.Size = new System.Drawing.Size(210, 23);
            this.Seat.TabIndex = 24;
            this.Seat.Text = "제공 : 영화관입장권통합전산망";
            this.Seat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.BookingTime,
            this.Time,
            this.Theater,
            this.movieName,
            this.SeatNum});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(449, 22);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(598, 262);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // BookingTime
            // 
            this.BookingTime.Text = "관람일자";
            this.BookingTime.Width = 150;
            // 
            // Time
            // 
            this.Time.Text = "관람시간";
            this.Time.Width = 109;
            // 
            // Theater
            // 
            this.Theater.Text = "영화관";
            this.Theater.Width = 86;
            // 
            // movieName
            // 
            this.movieName.Text = "영화";
            // 
            // SeatNum
            // 
            this.SeatNum.Text = "좌석";
            // 
            // bookingcheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Seat);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MovieChart);
            this.Controls.Add(this.comTime);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btUpdate);
            this.Name = "bookingcheck";
            this.Text = "bookingcheck";
            ((System.ComponentModel.ISupportInitialize)(this.MovieChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.ComboBox comTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart MovieChart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Seat;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader BookingTime;
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader Theater;
        private System.Windows.Forms.ColumnHeader movieName;
        private System.Windows.Forms.ColumnHeader SeatNum;
    }
}