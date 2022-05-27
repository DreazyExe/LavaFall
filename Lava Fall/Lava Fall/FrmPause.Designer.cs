namespace Lava_Fall
{
    partial class FrmPause
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPause));
            this.label1 = new System.Windows.Forms.Label();
            this.btRiprendi = new System.Windows.Forms.Button();
            this.btChiudi = new System.Windows.Forms.Button();
            this.lblActualPoints = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(82, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pausa";
            // 
            // btRiprendi
            // 
            this.btRiprendi.BackColor = System.Drawing.Color.Transparent;
            this.btRiprendi.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btRiprendi.Image = global::Lava_Fall.Properties.Resources.PlayLava;
            this.btRiprendi.Location = new System.Drawing.Point(21, 93);
            this.btRiprendi.Name = "btRiprendi";
            this.btRiprendi.Size = new System.Drawing.Size(100, 100);
            this.btRiprendi.TabIndex = 1;
            this.btRiprendi.UseVisualStyleBackColor = false;
            // 
            // btChiudi
            // 
            this.btChiudi.BackColor = System.Drawing.Color.Transparent;
            this.btChiudi.BackgroundImage = global::Lava_Fall.Properties.Resources.xLava;
            this.btChiudi.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btChiudi.Location = new System.Drawing.Point(158, 93);
            this.btChiudi.Name = "btChiudi";
            this.btChiudi.Size = new System.Drawing.Size(100, 100);
            this.btChiudi.TabIndex = 2;
            this.btChiudi.UseVisualStyleBackColor = false;
            // 
            // lblActualPoints
            // 
            this.lblActualPoints.AutoSize = true;
            this.lblActualPoints.BackColor = System.Drawing.Color.Transparent;
            this.lblActualPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualPoints.ForeColor = System.Drawing.SystemColors.Window;
            this.lblActualPoints.Location = new System.Drawing.Point(14, 48);
            this.lblActualPoints.Name = "lblActualPoints";
            this.lblActualPoints.Size = new System.Drawing.Size(44, 39);
            this.lblActualPoints.TabIndex = 3;
            this.lblActualPoints.Text = "...";
            // 
            // FrmPause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lava_Fall.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(279, 220);
            this.ControlBox = false;
            this.Controls.Add(this.lblActualPoints);
            this.Controls.Add(this.btChiudi);
            this.Controls.Add(this.btRiprendi);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPause";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lava Fall - Pausa";
            this.Load += new System.EventHandler(this.FrmPausa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btRiprendi;
        private System.Windows.Forms.Button btChiudi;
        private System.Windows.Forms.Label lblActualPoints;
    }
}