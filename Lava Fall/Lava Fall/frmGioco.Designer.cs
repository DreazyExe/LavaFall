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
            this.punteggio = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lbPunteggio = new System.Windows.Forms.Label();
            this.spostamento_basi = new System.Windows.Forms.Timer(this.components);
            this.pbBase1 = new System.Windows.Forms.PictureBox();
            this.pbBase2 = new System.Windows.Forms.PictureBox();
            this.pbBase3 = new System.Windows.Forms.PictureBox();
            this.pbPersonaggio = new System.Windows.Forms.PictureBox();
            this.pbLava = new System.Windows.Forms.PictureBox();
            this.pbBasePrincipale = new System.Windows.Forms.PictureBox();
            this.lblCountDown = new System.Windows.Forms.Label();
            this.countDown = new System.Windows.Forms.Timer(this.components);
            this.pbBase4 = new System.Windows.Forms.PictureBox();
            this.spostamento_pg = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbBase1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonaggio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLava)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasePrincipale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase4)).BeginInit();
            this.SuspendLayout();
            // 
            // timerLava
            // 
            this.timerLava.Enabled = true;
            this.timerLava.Interval = 208;
            this.timerLava.Tick += new System.EventHandler(this.timerLava_Tick);
            // 
            // punteggio
            // 
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
            // spostamento_basi
            // 
            this.spostamento_basi.Interval = 300;
            this.spostamento_basi.Tick += new System.EventHandler(this.spostamento_basi_Tick);
            // 
            // pbBase1
            // 
            this.pbBase1.BackColor = System.Drawing.Color.Transparent;
            this.pbBase1.Image = global::Lava_Fall.Properties.Resources.piattaforma_normale;
            this.pbBase1.Location = new System.Drawing.Point(95, 287);
            this.pbBase1.Name = "pbBase1";
            this.pbBase1.Size = new System.Drawing.Size(193, 68);
            this.pbBase1.TabIndex = 5;
            this.pbBase1.TabStop = false;
            this.pbBase1.Tag = "Base";
            this.pbBase1.Click += new System.EventHandler(this.pbBase1_Click);
            // 
            // pbBase2
            // 
            this.pbBase2.BackColor = System.Drawing.Color.Transparent;
            this.pbBase2.Image = global::Lava_Fall.Properties.Resources.piattaforma_normale;
            this.pbBase2.Location = new System.Drawing.Point(605, 287);
            this.pbBase2.Name = "pbBase2";
            this.pbBase2.Size = new System.Drawing.Size(193, 68);
            this.pbBase2.TabIndex = 4;
            this.pbBase2.TabStop = false;
            this.pbBase2.Tag = "Base";
            // 
            // pbBase3
            // 
            this.pbBase3.BackColor = System.Drawing.Color.Transparent;
            this.pbBase3.Image = global::Lava_Fall.Properties.Resources.piattaforma_normale;
            this.pbBase3.Location = new System.Drawing.Point(258, 56);
            this.pbBase3.Name = "pbBase3";
            this.pbBase3.Size = new System.Drawing.Size(193, 68);
            this.pbBase3.TabIndex = 3;
            this.pbBase3.TabStop = false;
            this.pbBase3.Tag = "Base";
            this.pbBase3.Click += new System.EventHandler(this.pbBase3_Click);
            // 
            // pbPersonaggio
            // 
            this.pbPersonaggio.BackColor = System.Drawing.Color.Transparent;
            this.pbPersonaggio.Image = global::Lava_Fall.Properties.Resources.Asset_17_4x1;
            this.pbPersonaggio.Location = new System.Drawing.Point(336, 391);
            this.pbPersonaggio.Name = "pbPersonaggio";
            this.pbPersonaggio.Size = new System.Drawing.Size(145, 142);
            this.pbPersonaggio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonaggio.TabIndex = 6;
            this.pbPersonaggio.TabStop = false;
            this.pbPersonaggio.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pbLava
            // 
            this.pbLava.BackColor = System.Drawing.Color.Transparent;
            this.pbLava.ErrorImage = null;
            this.pbLava.Image = global::Lava_Fall.Properties.Resources.A;
            this.pbLava.InitialImage = null;
            this.pbLava.Location = new System.Drawing.Point(-25, 618);
            this.pbLava.Name = "pbLava";
            this.pbLava.Size = new System.Drawing.Size(851, 233);
            this.pbLava.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLava.TabIndex = 0;
            this.pbLava.TabStop = false;
            // 
            // pbBasePrincipale
            // 
            this.pbBasePrincipale.BackColor = System.Drawing.Color.Transparent;
            this.pbBasePrincipale.Image = global::Lava_Fall.Properties.Resources.Base;
            this.pbBasePrincipale.Location = new System.Drawing.Point(-25, 422);
            this.pbBasePrincipale.Name = "pbBasePrincipale";
            this.pbBasePrincipale.Size = new System.Drawing.Size(851, 246);
            this.pbBasePrincipale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBasePrincipale.TabIndex = 7;
            this.pbBasePrincipale.TabStop = false;
            this.pbBasePrincipale.Tag = "BasePrincipale";
            // 
            // lblCountDown
            // 
            this.lblCountDown.AutoSize = true;
            this.lblCountDown.BackColor = System.Drawing.Color.Transparent;
            this.lblCountDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 129.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDown.ForeColor = System.Drawing.Color.Transparent;
            this.lblCountDown.Location = new System.Drawing.Point(184, 268);
            this.lblCountDown.Name = "lblCountDown";
            this.lblCountDown.Size = new System.Drawing.Size(461, 195);
            this.lblCountDown.TabIndex = 8;
            this.lblCountDown.Text = "   3   ";
            this.lblCountDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // countDown
            // 
            this.countDown.Enabled = true;
            this.countDown.Interval = 1000;
            this.countDown.Tick += new System.EventHandler(this.countDown_Tick);
            // 
            // pbBase4
            // 
            this.pbBase4.BackColor = System.Drawing.Color.Transparent;
            this.pbBase4.Image = global::Lava_Fall.Properties.Resources.piattaforma_normale;
            this.pbBase4.Location = new System.Drawing.Point(336, 525);
            this.pbBase4.Name = "pbBase4";
            this.pbBase4.Size = new System.Drawing.Size(193, 68);
            this.pbBase4.TabIndex = 9;
            this.pbBase4.TabStop = false;
            this.pbBase4.Tag = "Base";
            // 
            // spostamento_pg
            // 
            this.spostamento_pg.Enabled = true;
            this.spostamento_pg.Interval = 50;
            this.spostamento_pg.Tick += new System.EventHandler(this.personaggio_pg);
            // 
            // FormGioco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.BackgroundImage = global::Lava_Fall.Properties.Resources.Sfondo_mattoni;
            this.ClientSize = new System.Drawing.Size(794, 711);
            this.Controls.Add(this.pbPersonaggio);
            this.Controls.Add(this.pbLava);
            this.Controls.Add(this.pbBase4);
            this.Controls.Add(this.lbPunteggio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbBase1);
            this.Controls.Add(this.pbBase2);
            this.Controls.Add(this.pbBase3);
            this.Controls.Add(this.pbBasePrincipale);
            this.Controls.Add(this.lblCountDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormGioco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGioco";
            this.Load += new System.EventHandler(this.FormGioco_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbBase1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonaggio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLava)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasePrincipale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBase4)).EndInit();
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
        private System.Windows.Forms.PictureBox pbPersonaggio;
        private System.Windows.Forms.PictureBox pbBasePrincipale;
        private System.Windows.Forms.Label lblCountDown;
        private System.Windows.Forms.Timer countDown;
        private System.Windows.Forms.PictureBox pbBase4;
        private System.Windows.Forms.Timer spostamento_pg;
    }
}