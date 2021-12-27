namespace Lava_Fall
{
    partial class FormGioco
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
            this.components = new System.ComponentModel.Container();
            this.timerLava = new System.Windows.Forms.Timer(this.components);
            this.pbLava = new System.Windows.Forms.PictureBox();
            this.punteggio = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lbPunteggio = new System.Windows.Forms.Label();
            this.pbBase3 = new System.Windows.Forms.PictureBox();
            this.spostamento_basi = new System.Windows.Forms.Timer(this.components);
            this.pbBase2 = new System.Windows.Forms.PictureBox();
            this.pbBase1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLava)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerLava
            // 
            this.timerLava.Enabled = true;
            this.timerLava.Interval = 208;
            this.timerLava.Tick += new System.EventHandler(this.timerLava_Tick);
            // 
            // pbLava
            // 
            this.pbLava.BackColor = System.Drawing.Color.Transparent;
            this.pbLava.ErrorImage = null;
            this.pbLava.Image = global::Lava_Fall.Properties.Resources.A;
            this.pbLava.InitialImage = null;
            this.pbLava.Location = new System.Drawing.Point(-1, 40);
            this.pbLava.Name = "pbLava";
            this.pbLava.Size = new System.Drawing.Size(933, 694);
            this.pbLava.TabIndex = 0;
            this.pbLava.TabStop = false;
            // 
            // punteggio
            // 
            this.punteggio.Enabled = true;
            this.punteggio.Tick += new System.EventHandler(this.punteggio_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "SCORE";
            // 
            // lbPunteggio
            // 
            this.lbPunteggio.AutoSize = true;
            this.lbPunteggio.BackColor = System.Drawing.Color.Transparent;
            this.lbPunteggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPunteggio.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbPunteggio.Location = new System.Drawing.Point(90, 13);
            this.lbPunteggio.Name = "lbPunteggio";
            this.lbPunteggio.Size = new System.Drawing.Size(25, 25);
            this.lbPunteggio.TabIndex = 2;
            this.lbPunteggio.Text = "0";
            // 
            // pbBase3
            // 
            this.pbBase3.BackColor = System.Drawing.Color.Transparent;
            this.pbBase3.Image = global::Lava_Fall.Properties.Resources.piattaforma_normale;
            this.pbBase3.Location = new System.Drawing.Point(52, 89);
            this.pbBase3.Name = "pbBase3";
            this.pbBase3.Size = new System.Drawing.Size(193, 68);
            this.pbBase3.TabIndex = 3;
            this.pbBase3.TabStop = false;
            // 
            // spostamento_basi
            // 
            this.spostamento_basi.Enabled = true;
            this.spostamento_basi.Interval = 600;
            this.spostamento_basi.Tick += new System.EventHandler(this.spostamento_basi_Tick);
            // 
            // pbBase2
            // 
            this.pbBase2.BackColor = System.Drawing.Color.Transparent;
            this.pbBase2.Image = global::Lava_Fall.Properties.Resources.piattaforma_normale;
            this.pbBase2.Location = new System.Drawing.Point(496, 290);
            this.pbBase2.Name = "pbBase2";
            this.pbBase2.Size = new System.Drawing.Size(193, 68);
            this.pbBase2.TabIndex = 4;
            this.pbBase2.TabStop = false;
            // 
            // pbBase1
            // 
            this.pbBase1.BackColor = System.Drawing.Color.Transparent;
            this.pbBase1.Image = global::Lava_Fall.Properties.Resources.piattaforma_normale;
            this.pbBase1.Location = new System.Drawing.Point(278, 532);
            this.pbBase1.Name = "pbBase1";
            this.pbBase1.Size = new System.Drawing.Size(193, 68);
            this.pbBase1.TabIndex = 5;
            this.pbBase1.TabStop = false;
            // 
            // FormGioco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lava_Fall.Properties.Resources.Sfondo_mattoni;
            this.ClientSize = new System.Drawing.Size(794, 711);
            this.Controls.Add(this.pbBase1);
            this.Controls.Add(this.pbBase2);
            this.Controls.Add(this.pbBase3);
            this.Controls.Add(this.lbPunteggio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbLava);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormGioco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGioco";
            ((System.ComponentModel.ISupportInitialize)(this.pbLava)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerLava;
        private System.Windows.Forms.PictureBox pbLava;
        private System.Windows.Forms.Timer punteggio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbPunteggio;
        private System.Windows.Forms.PictureBox pbBase3;
        private System.Windows.Forms.Timer spostamento_basi;
        private System.Windows.Forms.PictureBox pbBase2;
        private System.Windows.Forms.PictureBox pbBase1;
    }
}