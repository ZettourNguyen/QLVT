﻿namespace QLVT
{
    partial class FormTaoTk
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbNv = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textPass = new System.Windows.Forms.TextBox();
            this.texRePass = new System.Windows.Forms.TextBox();
            this.roleGroup = new System.Windows.Forms.GroupBox();
            this.roleBtn2 = new System.Windows.Forms.RadioButton();
            this.role1Btn = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.exitbtn = new System.Windows.Forms.Button();
            this.usTable = new System.Windows.Forms.Label();
            this.usTextBox = new System.Windows.Forms.TextBox();
            this.roleGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(215, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tạo tài khoản";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã Nhân viên : ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cbbNv
            // 
            this.cbbNv.FormattingEnabled = true;
            this.cbbNv.Location = new System.Drawing.Point(275, 126);
            this.cbbNv.Name = "cbbNv";
            this.cbbNv.Size = new System.Drawing.Size(190, 21);
            this.cbbNv.TabIndex = 2;
            this.cbbNv.SelectedIndexChanged += new System.EventHandler(this.cbbNv_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(100, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Mật khẩu :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(100, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Xác nhận mật khẩu: ";
            // 
            // textPass
            // 
            this.textPass.Location = new System.Drawing.Point(275, 195);
            this.textPass.Multiline = true;
            this.textPass.Name = "textPass";
            this.textPass.Size = new System.Drawing.Size(190, 30);
            this.textPass.TabIndex = 5;
            // 
            // texRePass
            // 
            this.texRePass.Location = new System.Drawing.Point(275, 232);
            this.texRePass.Multiline = true;
            this.texRePass.Name = "texRePass";
            this.texRePass.Size = new System.Drawing.Size(190, 30);
            this.texRePass.TabIndex = 6;
            // 
            // roleGroup
            // 
            this.roleGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.roleGroup.Controls.Add(this.roleBtn2);
            this.roleGroup.Controls.Add(this.role1Btn);
            this.roleGroup.ForeColor = System.Drawing.SystemColors.MenuText;
            this.roleGroup.Location = new System.Drawing.Point(275, 285);
            this.roleGroup.Name = "roleGroup";
            this.roleGroup.Size = new System.Drawing.Size(190, 40);
            this.roleGroup.TabIndex = 7;
            this.roleGroup.TabStop = false;
            // 
            // roleBtn2
            // 
            this.roleBtn2.AutoSize = true;
            this.roleBtn2.Location = new System.Drawing.Point(105, 17);
            this.roleBtn2.Name = "roleBtn2";
            this.roleBtn2.Size = new System.Drawing.Size(47, 17);
            this.roleBtn2.TabIndex = 1;
            this.roleBtn2.TabStop = true;
            this.roleBtn2.Text = "User";
            this.roleBtn2.UseVisualStyleBackColor = true;
            this.roleBtn2.CheckedChanged += new System.EventHandler(this.roleBtn2_CheckedChanged);
            // 
            // role1Btn
            // 
            this.role1Btn.AutoSize = true;
            this.role1Btn.Checked = true;
            this.role1Btn.Location = new System.Drawing.Point(7, 17);
            this.role1Btn.Name = "role1Btn";
            this.role1Btn.Size = new System.Drawing.Size(73, 17);
            this.role1Btn.TabIndex = 0;
            this.role1Btn.TabStop = true;
            this.role1Btn.Text = "Chi nhánh";
            this.role1Btn.UseVisualStyleBackColor = true;
            this.role1Btn.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(109, 299);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Vai trò ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.confirmButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.confirmButton.Location = new System.Drawing.Point(184, 370);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(88, 37);
            this.confirmButton.TabIndex = 9;
            this.confirmButton.Text = "Tạo";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // exitbtn
            // 
            this.exitbtn.BackColor = System.Drawing.Color.IndianRed;
            this.exitbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitbtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.exitbtn.Location = new System.Drawing.Point(326, 370);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(88, 37);
            this.exitbtn.TabIndex = 11;
            this.exitbtn.Text = "Thoát";
            this.exitbtn.UseVisualStyleBackColor = false;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // usTable
            // 
            this.usTable.AutoSize = true;
            this.usTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usTable.Location = new System.Drawing.Point(100, 168);
            this.usTable.Name = "usTable";
            this.usTable.Size = new System.Drawing.Size(113, 20);
            this.usTable.TabIndex = 12;
            this.usTable.Text = "Tên tài khoản :";
            this.usTable.Click += new System.EventHandler(this.label6_Click);
            // 
            // usTextBox
            // 
            this.usTextBox.Location = new System.Drawing.Point(275, 156);
            this.usTextBox.Multiline = true;
            this.usTextBox.Name = "usTextBox";
            this.usTextBox.Size = new System.Drawing.Size(190, 30);
            this.usTextBox.TabIndex = 13;
            // 
            // FormTaoTk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 450);
            this.Controls.Add(this.usTextBox);
            this.Controls.Add(this.usTable);
            this.Controls.Add(this.exitbtn);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.roleGroup);
            this.Controls.Add(this.texRePass);
            this.Controls.Add(this.textPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbNv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormTaoTk";
            this.Text = "FormTaoTk";
            this.Load += new System.EventHandler(this.FormTaoTk_Load);
            this.roleGroup.ResumeLayout(false);
            this.roleGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbNv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textPass;
        private System.Windows.Forms.TextBox texRePass;
        private System.Windows.Forms.RadioButton roleBtn2;
        private System.Windows.Forms.RadioButton role1Btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.Label usTable;
        private System.Windows.Forms.TextBox usTextBox;
        private System.Windows.Forms.GroupBox roleGroup;
    }
}