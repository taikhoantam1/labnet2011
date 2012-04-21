using System.Windows.Forms;
namespace LabnetSerialCommunication
{
    partial class MainForm
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
            
            bool isValid = true;
            //if(isConnectionAvailable())
            //{
                for (int i = 0; i < lstValidInstruments.Count; i++)
                {
                    if(lstValidInstruments[i] == true)
                    isValid = false;
                }
                if(isValid){
                    if (disposing && (components != null))
                    {
                        components.Dispose();
                    }
                    base.Dispose(disposing);
                }
            //}
            if(!isValid)
            {
                MessageBox.Show("Vui lòng đóng tất cả các cổng trước khi đóng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.serialPort_AU600 = new System.IO.Ports.SerialPort(this.components);
            this.lblTop = new System.Windows.Forms.Label();
            this.dataGridInstrumentTable = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.InstrumentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Config = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnInstall = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstrumentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.checkAutomaticUpdate = new System.Windows.Forms.CheckBox();
            this.serialPort_CellDyn1700 = new System.IO.Ports.SerialPort(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerPeriod = new System.Windows.Forms.Timer(this.components);
            this.timerConnect = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInstrumentTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort_AU600
            // 
            this.serialPort_AU600.BaudRate = 4800;
            this.serialPort_AU600.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_AU600_DataReceived);
            // 
            // lblTop
            // 
            this.lblTop.BackColor = System.Drawing.SystemColors.Control;
            this.lblTop.Font = new System.Drawing.Font("Brush Script MT", 30F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                            | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTop.Location = new System.Drawing.Point(284, 9);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(411, 70);
            this.lblTop.TabIndex = 0;
            this.lblTop.Text = "PHÂN HỆ KẾT NỐI";
            this.lblTop.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGridInstrumentTable
            // 
            this.dataGridInstrumentTable.AllowUserToAddRows = false;
            this.dataGridInstrumentTable.AllowUserToDeleteRows = false;
            this.dataGridInstrumentTable.AllowUserToResizeColumns = false;
            this.dataGridInstrumentTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridInstrumentTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridInstrumentTable.ColumnHeadersHeight = 25;
            this.dataGridInstrumentTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.InstrumentName,
            this.Config,
            this.btnInstall,
            this.Status,
            this.InstrumentId});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridInstrumentTable.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridInstrumentTable.EnableHeadersVisualStyles = false;
            this.dataGridInstrumentTable.Location = new System.Drawing.Point(8, 131);
            this.dataGridInstrumentTable.Name = "dataGridInstrumentTable";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridInstrumentTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridInstrumentTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.dataGridInstrumentTable.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridInstrumentTable.Size = new System.Drawing.Size(862, 168);
            this.dataGridInstrumentTable.TabIndex = 1;
            this.dataGridInstrumentTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridInstrumentTable_CellClick);
            // 
            // Check
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.NullValue = false;
            this.Check.DefaultCellStyle = dataGridViewCellStyle2;
            this.Check.FillWeight = 250F;
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            this.Check.Width = 50;
            // 
            // InstrumentName
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.InstrumentName.DefaultCellStyle = dataGridViewCellStyle3;
            this.InstrumentName.FillWeight = 250F;
            this.InstrumentName.HeaderText = "Máy Xét Nghiệm";
            this.InstrumentName.Name = "InstrumentName";
            this.InstrumentName.ReadOnly = true;
            this.InstrumentName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.InstrumentName.Width = 291;
            // 
            // Config
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Config.DefaultCellStyle = dataGridViewCellStyle4;
            this.Config.FillWeight = 250F;
            this.Config.HeaderText = "Cấu Hình";
            this.Config.Name = "Config";
            this.Config.ReadOnly = true;
            this.Config.Width = 330;
            // 
            // btnInstall
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnInstall.DefaultCellStyle = dataGridViewCellStyle5;
            this.btnInstall.FillWeight = 250F;
            this.btnInstall.HeaderText = "Cài Đặt";
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Width = 60;
            // 
            // Status
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Status.DefaultCellStyle = dataGridViewCellStyle6;
            this.Status.FillWeight = 250F;
            this.Status.HeaderText = "Trạng Thái";
            this.Status.Name = "Status";
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Status.Width = 88;
            // 
            // InstrumentId
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.InstrumentId.DefaultCellStyle = dataGridViewCellStyle7;
            this.InstrumentId.FillWeight = 250F;
            this.InstrumentId.HeaderText = "";
            this.InstrumentId.Name = "InstrumentId";
            this.InstrumentId.Visible = false;
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnOpenPort.Location = new System.Drawing.Point(43, 322);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(97, 27);
            this.btnOpenPort.TabIndex = 2;
            this.btnOpenPort.Text = "Mở Cổng";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnClosePort
            // 
            this.btnClosePort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClosePort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnClosePort.Location = new System.Drawing.Point(191, 322);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(87, 27);
            this.btnClosePort.TabIndex = 3;
            this.btnClosePort.Text = "Đóng Cổng";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // checkAutomaticUpdate
            // 
            this.checkAutomaticUpdate.AutoSize = true;
            this.checkAutomaticUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAutomaticUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkAutomaticUpdate.Location = new System.Drawing.Point(334, 322);
            this.checkAutomaticUpdate.Name = "checkAutomaticUpdate";
            this.checkAutomaticUpdate.Size = new System.Drawing.Size(153, 20);
            this.checkAutomaticUpdate.TabIndex = 4;
            this.checkAutomaticUpdate.Text = "Cập Nhật Tự Động";
            this.checkAutomaticUpdate.UseVisualStyleBackColor = true;
            this.checkAutomaticUpdate.CheckedChanged += new System.EventHandler(this.checkAutomaticUpdate_CheckedChanged);
            // 
            // serialPort_CellDyn1700
            // 
            this.serialPort_CellDyn1700.PortName = "COM2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LabnetSerialCommunication.Properties.Resources.Banner;
            this.pictureBox1.Location = new System.Drawing.Point(8, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(270, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // timerPeriod
            // 
            this.timerPeriod.Interval = 10000;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 368);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkAutomaticUpdate);
            this.Controls.Add(this.btnClosePort);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.dataGridInstrumentTable);
            this.Controls.Add(this.lblTop);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LABNet";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInstrumentTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort_AU600;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.DataGridView dataGridInstrumentTable;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Button btnClosePort;
        private System.Windows.Forms.CheckBox checkAutomaticUpdate;
        private System.IO.Ports.SerialPort serialPort_CellDyn1700;
        private DataGridViewCheckBoxColumn Check;
        private DataGridViewTextBoxColumn InstrumentName;
        private DataGridViewTextBoxColumn Config;
        private DataGridViewButtonColumn btnInstall;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn InstrumentId;
        private PictureBox pictureBox1;
        private Timer timerPeriod;
        private Timer timerConnect;
    }
}

