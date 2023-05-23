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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatAppearance.BorderSize = 5;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(248, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 82);
            this.button1.TabIndex = 8;
            this.button1.Text = "TAOTAIKHOAN";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button2.FlatAppearance.BorderSize = 5;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(503, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(220, 82);
            this.button2.TabIndex = 9;
            this.button2.Text = "THOAT";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // QuanLyTk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DANGNHAP);
            this.Name = "QuanLyTk";
            this.Size = new System.Drawing.Size(1179, 711);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DANGNHAP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
