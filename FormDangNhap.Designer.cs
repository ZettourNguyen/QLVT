namespace QLVT
{
    partial class FormDangNhap
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
            this.cmbCHINHANH = new System.Windows.Forms.ComboBox();
            this.txtMATKHAU = new System.Windows.Forms.TextBox();
            this.txtTAIKHOAN = new System.Windows.Forms.TextBox();
            this.btnTHOAT = new System.Windows.Forms.Button();
            this.btnDANGNHAP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbCHINHANH
            // 
            this.cmbCHINHANH.DisplayMember = "0";
            this.cmbCHINHANH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCHINHANH.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbCHINHANH.FormattingEnabled = true;
            this.cmbCHINHANH.Location = new System.Drawing.Point(310, 51);
            this.cmbCHINHANH.Name = "cmbCHINHANH";
            this.cmbCHINHANH.Size = new System.Drawing.Size(282, 33);
            this.cmbCHINHANH.TabIndex = 62;
            this.cmbCHINHANH.SelectedIndexChanged += new System.EventHandler(this.cmbCHINHANH_SelectedIndexChanged);
            // 
            // txtMATKHAU
            // 
            this.txtMATKHAU.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtMATKHAU.Location = new System.Drawing.Point(310, 153);
            this.txtMATKHAU.Name = "txtMATKHAU";
            this.txtMATKHAU.Size = new System.Drawing.Size(282, 30);
            this.txtMATKHAU.TabIndex = 60;
            // 
            // txtTAIKHOAN
            // 
            this.txtTAIKHOAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTAIKHOAN.Location = new System.Drawing.Point(310, 102);
            this.txtTAIKHOAN.Name = "txtTAIKHOAN";
            this.txtTAIKHOAN.Size = new System.Drawing.Size(282, 30);
            this.txtTAIKHOAN.TabIndex = 59;
            // 
            // btnTHOAT
            // 
            this.btnTHOAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnTHOAT.Location = new System.Drawing.Point(465, 233);
            this.btnTHOAT.Name = "btnTHOAT";
            this.btnTHOAT.Size = new System.Drawing.Size(124, 69);
            this.btnTHOAT.TabIndex = 58;
            this.btnTHOAT.Text = "Thoat";
            this.btnTHOAT.UseVisualStyleBackColor = true;
            this.btnTHOAT.Click += new System.EventHandler(this.btnTHOAT_Click);
            // 
            // btnDANGNHAP
            // 
            this.btnDANGNHAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDANGNHAP.Location = new System.Drawing.Point(310, 233);
            this.btnDANGNHAP.Name = "btnDANGNHAP";
            this.btnDANGNHAP.Size = new System.Drawing.Size(124, 69);
            this.btnDANGNHAP.TabIndex = 57;
            this.btnDANGNHAP.Text = "Dang Nhap";
            this.btnDANGNHAP.UseVisualStyleBackColor = true;
            this.btnDANGNHAP.Click += new System.EventHandler(this.btnDANGNHAP_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(168, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 56;
            this.label1.Text = "Mat khau:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(116, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 25);
            this.label2.TabIndex = 55;
            this.label2.Text = "Ten dang nhap:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbCHINHANH);
            this.Controls.Add(this.txtMATKHAU);
            this.Controls.Add(this.txtTAIKHOAN);
            this.Controls.Add(this.btnTHOAT);
            this.Controls.Add(this.btnDANGNHAP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "FormDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDangNhap";
            this.Load += new System.EventHandler(this.FormDangNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCHINHANH;
        private System.Windows.Forms.TextBox txtMATKHAU;
        private System.Windows.Forms.TextBox txtTAIKHOAN;
        private System.Windows.Forms.Button btnTHOAT;
        private System.Windows.Forms.Button btnDANGNHAP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}