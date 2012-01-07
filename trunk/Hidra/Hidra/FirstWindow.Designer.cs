namespace Hidra
{
    partial class FirstWindow
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
            this.btn_neander = new System.Windows.Forms.Button();
            this.btn_queops = new System.Windows.Forms.Button();
            this.btn_ramses = new System.Windows.Forms.Button();
            this.btn_cromag = new System.Windows.Forms.Button();
            this.btn_pericles = new System.Windows.Forms.Button();
            this.btn_pitagoras = new System.Windows.Forms.Button();
            this.btn_ahmes = new System.Windows.Forms.Button();
            this.btn_volta = new System.Windows.Forms.Button();
            this.btn_reg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Escolha o programa:";
            // 
            // btn_neander
            // 
            this.btn_neander.Location = new System.Drawing.Point(94, 28);
            this.btn_neander.Name = "btn_neander";
            this.btn_neander.Size = new System.Drawing.Size(75, 23);
            this.btn_neander.TabIndex = 3;
            this.btn_neander.Text = "Neander";
            this.btn_neander.UseVisualStyleBackColor = true;
            this.btn_neander.Click += new System.EventHandler(this.btn_neander_Click);
            // 
            // btn_queops
            // 
            this.btn_queops.Location = new System.Drawing.Point(93, 57);
            this.btn_queops.Name = "btn_queops";
            this.btn_queops.Size = new System.Drawing.Size(75, 23);
            this.btn_queops.TabIndex = 4;
            this.btn_queops.Text = "Queops";
            this.btn_queops.UseVisualStyleBackColor = true;
            // 
            // btn_ramses
            // 
            this.btn_ramses.Location = new System.Drawing.Point(12, 115);
            this.btn_ramses.Name = "btn_ramses";
            this.btn_ramses.Size = new System.Drawing.Size(75, 23);
            this.btn_ramses.TabIndex = 6;
            this.btn_ramses.Text = "Ramses";
            this.btn_ramses.UseVisualStyleBackColor = true;
            this.btn_ramses.Click += new System.EventHandler(this.btn_ramses_Click);
            // 
            // btn_cromag
            // 
            this.btn_cromag.Location = new System.Drawing.Point(12, 57);
            this.btn_cromag.Name = "btn_cromag";
            this.btn_cromag.Size = new System.Drawing.Size(75, 23);
            this.btn_cromag.TabIndex = 5;
            this.btn_cromag.Text = "Cromag";
            this.btn_cromag.UseVisualStyleBackColor = true;
            this.btn_cromag.Click += new System.EventHandler(this.btn_cromag_Click);
            // 
            // btn_pericles
            // 
            this.btn_pericles.Location = new System.Drawing.Point(94, 115);
            this.btn_pericles.Name = "btn_pericles";
            this.btn_pericles.Size = new System.Drawing.Size(75, 23);
            this.btn_pericles.TabIndex = 7;
            this.btn_pericles.Text = "Pericles";
            this.btn_pericles.UseVisualStyleBackColor = true;
            // 
            // btn_pitagoras
            // 
            this.btn_pitagoras.Location = new System.Drawing.Point(12, 86);
            this.btn_pitagoras.Name = "btn_pitagoras";
            this.btn_pitagoras.Size = new System.Drawing.Size(75, 23);
            this.btn_pitagoras.TabIndex = 8;
            this.btn_pitagoras.Text = "Pitagoras";
            this.btn_pitagoras.UseVisualStyleBackColor = true;
            // 
            // btn_ahmes
            // 
            this.btn_ahmes.Location = new System.Drawing.Point(93, 86);
            this.btn_ahmes.Name = "btn_ahmes";
            this.btn_ahmes.Size = new System.Drawing.Size(75, 23);
            this.btn_ahmes.TabIndex = 9;
            this.btn_ahmes.Text = "Ahmes";
            this.btn_ahmes.UseVisualStyleBackColor = true;
            this.btn_ahmes.Click += new System.EventHandler(this.btn_ahmes_Click);
            // 
            // btn_volta
            // 
            this.btn_volta.Location = new System.Drawing.Point(52, 144);
            this.btn_volta.Name = "btn_volta";
            this.btn_volta.Size = new System.Drawing.Size(75, 23);
            this.btn_volta.TabIndex = 2;
            this.btn_volta.Text = "Volta";
            this.btn_volta.UseVisualStyleBackColor = true;
            // 
            // btn_reg
            // 
            this.btn_reg.Location = new System.Drawing.Point(12, 28);
            this.btn_reg.Name = "btn_reg";
            this.btn_reg.Size = new System.Drawing.Size(75, 23);
            this.btn_reg.TabIndex = 1;
            this.btn_reg.Text = "Reg";
            this.btn_reg.UseVisualStyleBackColor = true;
            // 
            // FirstWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 177);
            this.Controls.Add(this.btn_reg);
            this.Controls.Add(this.btn_volta);
            this.Controls.Add(this.btn_ahmes);
            this.Controls.Add(this.btn_pitagoras);
            this.Controls.Add(this.btn_pericles);
            this.Controls.Add(this.btn_cromag);
            this.Controls.Add(this.btn_ramses);
            this.Controls.Add(this.btn_queops);
            this.Controls.Add(this.btn_neander);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FirstWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hidra";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_neander;
        private System.Windows.Forms.Button btn_queops;
        private System.Windows.Forms.Button btn_ramses;
        private System.Windows.Forms.Button btn_cromag;
        private System.Windows.Forms.Button btn_pericles;
        private System.Windows.Forms.Button btn_pitagoras;
        private System.Windows.Forms.Button btn_ahmes;
        private System.Windows.Forms.Button btn_volta;
        private System.Windows.Forms.Button btn_reg;

    }
}