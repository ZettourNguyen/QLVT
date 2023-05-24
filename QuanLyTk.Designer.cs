namespace QLVT
{
    partial class QuanLyTk
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DANGNHAP = new System.Windows.Forms.Button();
            this.TAOTAIKHOAN = new System.Windows.Forms.Button();
            this.THOAT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DANGNHAP
            // 
            this.DANGNHAP.BackColor = System.Drawing.Color.LightSkyBlue;
            this.DANGNHAP.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.DANGNHAP.FlatAppearance.BorderSize = 5;
            this.DANGNHAP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.DANGNHAP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.DANGNHAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DANGNHAP.Location = new System.Drawing.Point(0, 0);
            this.DANGNHAP.Margin = new System.Windows.Forms.Padding(0);
            this.DANGNHAP.Name = "DANGNHAP";
            this.DANGNHAP.Size = new System.Drawing.Size(220, 82);
            this.DANGNHAP.TabIndex = 7;
            this.DANGNHAP.Text = "DangNhap";
            this.DANGNHAP.UseVisualStyleBackColor = false;
            this.DANGNHAP.Click += new System.EventHandler(this.DANGNHAP_Click);
            // 
            // TAOTAIKHOAN
            // 
            this.TAOTAIKHOAN.BackColor = System.Drawing.Color.LightSkyBlue;
            this.TAOTAIKHOAN.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.TAOTAIKHOAN.FlatAppearance.BorderSize = 5;
            this.TAOTAIKHOAN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.TAOTAIKHOAN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.TAOTAIKHOAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TAOTAIKHOAN.Location = new System.Drawing.Point(248, 0);
            this.TAOTAIKHOAN.Margin = new System.Windows.Forms.Padding(0);
            this.TAOTAIKHOAN.Name = "TAOTAIKHOAN";
            this.TAOTAIKHOAN.Size = new System.Drawing.Size(220, 82);
            this.TAOTAIKHOAN.TabIndex = 8;
            this.TAOTAIKHOAN.Text = "TAOTAIKHOAN";
            this.TAOTAIKHOAN.UseVisualStyleBackColor = false;
            this.TAOTAIKHOAN.Click += new System.EventHandler(this.TAOTAIKHOAN_Click);
            // 
            // THOAT
            // 
            this.THOAT.BackColor = System.Drawing.Color.LightSkyBlue;
            this.THOAT.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.THOAT.FlatAppearance.BorderSize = 5;
            this.THOAT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.THOAT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.THOAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.THOAT.Location = new System.Drawing.Point(503, 0);
            this.THOAT.Margin = new System.Windows.Forms.Padding(0);
            this.THOAT.Name = "THOAT";
            this.THOAT.Size = new System.Drawing.Size(220, 82);
            this.THOAT.TabIndex = 9;
            this.THOAT.Text = "THOAT";
            this.THOAT.UseVisualStyleBackColor = false;
            this.THOAT.Click += new System.EventHandler(this.THOAT_Click);
            // 
            // QuanLyTk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.THOAT);
            this.Controls.Add(this.TAOTAIKHOAN);
            this.Controls.Add(this.DANGNHAP);
            this.Name = "QuanLyTk";
            this.Size = new System.Drawing.Size(1236, 714);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DANGNHAP;
        private System.Windows.Forms.Button TAOTAIKHOAN;
        private System.Windows.Forms.Button THOAT;
    }
}
