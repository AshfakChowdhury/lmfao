namespace Project_2
{
    partial class Form1
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
            this.Button_LoadStockData = new System.Windows.Forms.Button();
            this.DateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
            this.DateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
            this.Label_StartDate = new System.Windows.Forms.Label();
            this.Label_EndDate = new System.Windows.Forms.Label();
            this.LoadButtonOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // Button_LoadStockData
            // 
            this.Button_LoadStockData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_LoadStockData.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Button_LoadStockData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_LoadStockData.Location = new System.Drawing.Point(211, 97);
            this.Button_LoadStockData.Name = "Button_LoadStockData";
            this.Button_LoadStockData.Size = new System.Drawing.Size(226, 46);
            this.Button_LoadStockData.TabIndex = 11;
            this.Button_LoadStockData.Text = "Load CandleStick";
            this.Button_LoadStockData.UseVisualStyleBackColor = false;
            this.Button_LoadStockData.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // DateTimePicker_EndDate
            // 
            this.DateTimePicker_EndDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.DateTimePicker_EndDate.Location = new System.Drawing.Point(211, 53);
            this.DateTimePicker_EndDate.Name = "DateTimePicker_EndDate";
            this.DateTimePicker_EndDate.Size = new System.Drawing.Size(254, 22);
            this.DateTimePicker_EndDate.TabIndex = 12;
            this.DateTimePicker_EndDate.Value = new System.DateTime(2024, 11, 15, 0, 0, 0, 0);
            // 
            // DateTimePicker_StartDate
            // 
            this.DateTimePicker_StartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DateTimePicker_StartDate.Location = new System.Drawing.Point(211, 14);
            this.DateTimePicker_StartDate.Name = "DateTimePicker_StartDate";
            this.DateTimePicker_StartDate.Size = new System.Drawing.Size(252, 22);
            this.DateTimePicker_StartDate.TabIndex = 13;
            this.DateTimePicker_StartDate.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // Label_StartDate
            // 
            this.Label_StartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_StartDate.AutoSize = true;
            this.Label_StartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_StartDate.Location = new System.Drawing.Point(41, 14);
            this.Label_StartDate.Name = "Label_StartDate";
            this.Label_StartDate.Size = new System.Drawing.Size(138, 20);
            this.Label_StartDate.TabIndex = 14;
            this.Label_StartDate.Text = "Select Start Date";
            // 
            // Label_EndDate
            // 
            this.Label_EndDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label_EndDate.AutoSize = true;
            this.Label_EndDate.BackColor = System.Drawing.Color.Transparent;
            this.Label_EndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_EndDate.Location = new System.Drawing.Point(41, 55);
            this.Label_EndDate.Name = "Label_EndDate";
            this.Label_EndDate.Size = new System.Drawing.Size(131, 20);
            this.Label_EndDate.TabIndex = 15;
            this.Label_EndDate.Text = "Select End Date";
            // 
            // LoadButtonOpenFileDialog
            // 
            this.LoadButtonOpenFileDialog.FileName = "openFileDialog1";
            this.LoadButtonOpenFileDialog.Filter = "All Stock files| *.csv| Daily Stocks|*-Day.csv| Weekly Stocks|*-Week.csv|Monthly " +
    "Stocks|*-Month.csv";
            this.LoadButtonOpenFileDialog.InitialDirectory = "C:\\Users";
            this.LoadButtonOpenFileDialog.Multiselect = true;
            this.LoadButtonOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadButtonOpenFileDialog_FileOk);
            // 
            // FormEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 155);
            this.Controls.Add(this.Label_EndDate);
            this.Controls.Add(this.Label_StartDate);
            this.Controls.Add(this.DateTimePicker_StartDate);
            this.Controls.Add(this.DateTimePicker_EndDate);
            this.Controls.Add(this.Button_LoadStockData);
            this.Name = "FormEntry";
            this.Text = "Stock Data/Chart Analysis";
            this.Load += new System.EventHandler(this.FormEntry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button_LoadStockData;
        private System.Windows.Forms.DateTimePicker DateTimePicker_EndDate;
        private System.Windows.Forms.DateTimePicker DateTimePicker_StartDate;
        private System.Windows.Forms.Label Label_StartDate;
        private System.Windows.Forms.Label Label_EndDate;
        private System.Windows.Forms.OpenFileDialog LoadButtonOpenFileDialog;
    }
}