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
            this.bttnOK = new System.Windows.Forms.Button();
            this.bttnCancel = new System.Windows.Forms.Button();
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
            // bttnOK
            // 
            this.bttnOK.Location = new System.Drawing.Point(68, 72);
            this.bttnOK.Name = "bttnOK";
            this.bttnOK.Size = new System.Drawing.Size(51, 23);
            this.bttnOK.TabIndex = 2;
            this.bttnOK.Text = "OK";
            this.bttnOK.UseVisualStyleBackColor = true;
            this.bttnOK.Click += new System.EventHandler(this.bttnOK_Click);
            // 
            // bttnCancel
            // 
            this.bttnCancel.Location = new System.Drawing.Point(125, 72);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(51, 23);
            this.bttnCancel.TabIndex = 3;
            this.bttnCancel.Text = "Cancel";
            this.bttnCancel.UseVisualStyleBackColor = true;
            this.bttnCancel.Click += new System.EventHandler(this.bttnCancel_Click);
            // 
            // AlteraRegistrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 112);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.bttnOK);
            this.Controls.Add(this.labelRA);
            this.Controls.Add(this.textBoxRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(258, 136);
            this.MinimumSize = new System.Drawing.Size(258, 136);
            this.Name = "AlteraRegistrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alterar Registrador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRegister;
        private System.Windows.Forms.Label labelRA;
        private System.Windows.Forms.Button bttnOK;
        private System.Windows.Forms.Button bttnCancel;
    }
}