namespace Niilo22Muistipeli
{
    partial class Pisteikkuna
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
            this.dgvPisteet = new System.Windows.Forms.DataGridView();
            this.pelialustaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPisteet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelialustaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPisteet
            // 
            this.dgvPisteet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPisteet.Location = new System.Drawing.Point(12, 12);
            this.dgvPisteet.Name = "dgvPisteet";
            this.dgvPisteet.Size = new System.Drawing.Size(371, 237);
            this.dgvPisteet.TabIndex = 0;
            // 
            // pelialustaBindingSource
            // 
            this.pelialustaBindingSource.DataSource = typeof(Niilo22Muistipeli.Pelialusta);
            // 
            // Pisteikkuna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 261);
            this.Controls.Add(this.dgvPisteet);
            this.Name = "Pisteikkuna";
            this.Text = "Pisteikkuna";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPisteet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelialustaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPisteet;
        private System.Windows.Forms.BindingSource pelialustaBindingSource;
    }
}