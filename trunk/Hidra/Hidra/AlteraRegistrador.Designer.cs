namespace Hidra
{
    partial class AlteraRegistrador
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
            this.textBoxRegister = new System.Windows.Forms.TextBox();
            this.labelRA = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxRegister
            // 
            this.textBoxRegister.Location = new System.Drawing.Point(59, 41);
            this.textBoxRegister.MaxLength = 3;
            this.textBoxRegister.Name = "textBoxRegister";
            this.textBoxRegister.Size = new System.Drawing.Size(129, 20);
            this.textBoxRegister.TabIndex = 0;
            this.textBoxRegister.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRegister_KeyPress);
            // 
            // labelRA
            // 
            this.labelRA.AutoSize = true;
            this.labelRA.Location = new System.Drawing.Point(42, 16);
            this.labelRA.Name = "labelRA";
            this.labelRA.Size = new System.Drawing.Size(166, 13);
            this.labelRA.TabIndex = 1;
            this.labelRA.Text = "Digite o novo valor do registrador:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(125, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // AlteraRegistrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 98);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelRA);
            this.Controls.Add(this.textBoxRegister);
            this.MaximumSize = new System.Drawing.Size(258, 136);
            this.MinimumSize = new System.Drawing.Size(258, 136);
            this.Name = "AlteraRegistrador";
            this.Text = "Alterar Registrador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRegister;
        private System.Windows.Forms.Label labelRA;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}