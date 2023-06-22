namespace QLVT.pnXprt
{
    partial class PnXprtSelection
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pnXprtInput = new DevExpress.XtraEditors.PanelControl();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnXprtInput)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.button4);
            this.panelControl1.Controls.Add(this.button2);
            this.panelControl1.Controls.Add(this.pnXprtInput);
            this.panelControl1.Controls.Add(this.button1);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1199, 655);
            this.panelControl1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 15F);
            this.button4.Location = new System.Drawing.Point(3, 136);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(386, 72);
            this.button4.TabIndex = 4;
            this.button4.Text = "Danh sách các đơn đặt hàng chưa có phiếu nhập";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 15F);
            this.button2.Location = new System.Drawing.Point(5, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(386, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "Danh mục vật tư";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pnXprtInput
            // 
            this.pnXprtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnXprtInput.Appearance.BackColor = System.Drawing.Color.White;
            this.pnXprtInput.Appearance.Options.UseBackColor = true;
            this.pnXprtInput.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnXprtInput.Location = new System.Drawing.Point(456, 46);
            this.pnXprtInput.Name = "pnXprtInput";
            this.pnXprtInput.Size = new System.Drawing.Size(707, 508);
            this.pnXprtInput.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 15F);
            this.button1.Location = new System.Drawing.Point(5, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(386, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Danh sách nhân viên";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PnXprtSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "PnXprtSelection";
            this.Size = new System.Drawing.Size(1202, 655);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnXprtInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraEditors.PanelControl pnXprtInput;
        private System.Windows.Forms.Button button4;
    }
}
