namespace Lava_Fall
{
    partial class FrmClassification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClassification));
            this.lvClassification = new System.Windows.Forms.ListView();
            this.clNickname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clNpartite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clPunteggio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbClassifica = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvClassification
            // 
            this.lvClassification.BackColor = System.Drawing.Color.Brown;
            this.lvClassification.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clNickname,
            this.clNpartite,
            this.clData,
            this.clPunteggio});
            this.lvClassification.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvClassification.ForeColor = System.Drawing.SystemColors.Window;
            this.lvClassification.GridLines = true;
            this.lvClassification.HideSelection = false;
            this.lvClassification.Location = new System.Drawing.Point(111, 183);
            this.lvClassification.Name = "lvClassification";
            this.lvClassification.Size = new System.Drawing.Size(563, 416);
            this.lvClassification.TabIndex = 0;
            this.lvClassification.UseCompatibleStateImageBehavior = false;
            this.lvClassification.View = System.Windows.Forms.View.Details;
            // 
            // clNickname
            // 
            this.clNickname.Text = "Nickname";
            this.clNickname.Width = 147;
            // 
            // clNpartite
            // 
            this.clNpartite.Text = "Partita";
            this.clNpartite.Width = 150;
            // 
            // clData
            // 
            this.clData.Text = "Data";
            this.clData.Width = 134;
            // 
            // clPunteggio
            // 
            this.clPunteggio.Text = "Punteggio";
            this.clPunteggio.Width = 128;
            // 
            // lbClassifica
            // 
            this.lbClassifica.AutoSize = true;
            this.lbClassifica.BackColor = System.Drawing.Color.Transparent;
            this.lbClassifica.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClassifica.ForeColor = System.Drawing.Color.White;
            this.lbClassifica.Location = new System.Drawing.Point(237, 98);
            this.lbClassifica.Name = "lbClassifica";
            this.lbClassifica.Size = new System.Drawing.Size(314, 55);
            this.lbClassifica.TabIndex = 1;
            this.lbClassifica.Text = "CLASSIFICA";
            // 
            // FrmClassification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lava_Fall.Properties.Resources.sfondo;
            this.ClientSize = new System.Drawing.Size(794, 711);
            this.Controls.Add(this.lbClassifica);
            this.Controls.Add(this.lvClassification);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmClassification";
            this.Text = "Classifica";
            this.Load += new System.EventHandler(this.FrmClassifica_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvClassification;
        private System.Windows.Forms.ColumnHeader clNickname;
        private System.Windows.Forms.ColumnHeader clNpartite;
        private System.Windows.Forms.ColumnHeader clData;
        private System.Windows.Forms.ColumnHeader clPunteggio;
        private System.Windows.Forms.Label lbClassifica;
    }
}