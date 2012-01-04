namespace Hidra
{
    partial class MainWindow
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
            this.groupSimulator = new System.Windows.Forms.GroupBox();
            this.btn_passoapasso = new System.Windows.Forms.Button();
            this.btn_rodar = new System.Windows.Forms.Button();
            this.lbl_negativeText = new System.Windows.Forms.Label();
            this.lbl_zeroText = new System.Windows.Forms.Label();
            this.lbl_negative = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_acessos = new System.Windows.Forms.TextBox();
            this.lbl_acesso = new System.Windows.Forms.Label();
            this.txt_instrucoes = new System.Windows.Forms.TextBox();
            this.lbl_instrucoes = new System.Windows.Forms.Label();
            this.lbl_zero = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_pc = new System.Windows.Forms.TextBox();
            this.lbl_pc = new System.Windows.Forms.Label();
            this.lbl_ac = new System.Windows.Forms.Label();
            this.txt_ac = new System.Windows.Forms.TextBox();
            this.groupAssembler = new System.Windows.Forms.GroupBox();
            this.txtAssembler = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupSimulator.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupAssembler.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupSimulator
            // 
            this.groupSimulator.Controls.Add(this.btn_passoapasso);
            this.groupSimulator.Controls.Add(this.btn_rodar);
            this.groupSimulator.Controls.Add(this.lbl_negativeText);
            this.groupSimulator.Controls.Add(this.lbl_zeroText);
            this.groupSimulator.Controls.Add(this.lbl_negative);
            this.groupSimulator.Controls.Add(this.groupBox2);
            this.groupSimulator.Controls.Add(this.lbl_zero);
            this.groupSimulator.Controls.Add(this.groupBox1);
            this.groupSimulator.Location = new System.Drawing.Point(151, 27);
            this.groupSimulator.Name = "groupSimulator";
            this.groupSimulator.Size = new System.Drawing.Size(298, 152);
            this.groupSimulator.TabIndex = 2;
            this.groupSimulator.TabStop = false;
            this.groupSimulator.Text = "Simulador";
            // 
            // btn_passoapasso
            // 
            this.btn_passoapasso.Location = new System.Drawing.Point(101, 62);
            this.btn_passoapasso.Name = "btn_passoapasso";
            this.btn_passoapasso.Size = new System.Drawing.Size(58, 34);
            this.btn_passoapasso.TabIndex = 15;
            this.btn_passoapasso.Text = "Passo a Passo";
            this.btn_passoapasso.UseVisualStyleBackColor = true;
            // 
            // btn_rodar
            // 
            this.btn_rodar.Location = new System.Drawing.Point(101, 30);
            this.btn_rodar.Name = "btn_rodar";
            this.btn_rodar.Size = new System.Drawing.Size(58, 23);
            this.btn_rodar.TabIndex = 14;
            this.btn_rodar.Text = "Rodar";
            this.btn_rodar.UseVisualStyleBackColor = true;
            // 
            // lbl_negativeText
            // 
            this.lbl_negativeText.AutoSize = true;
            this.lbl_negativeText.Location = new System.Drawing.Point(219, 110);
            this.lbl_negativeText.Name = "lbl_negativeText";
            this.lbl_negativeText.Size = new System.Drawing.Size(50, 13);
            this.lbl_negativeText.TabIndex = 4;
            this.lbl_negativeText.Text = "Negative";
            // 
            // lbl_zeroText
            // 
            this.lbl_zeroText.AutoSize = true;
            this.lbl_zeroText.Location = new System.Drawing.Point(240, 133);
            this.lbl_zeroText.Name = "lbl_zeroText";
            this.lbl_zeroText.Size = new System.Drawing.Size(29, 13);
            this.lbl_zeroText.TabIndex = 5;
            this.lbl_zeroText.Text = "Zero";
            // 
            // lbl_negative
            // 
            this.lbl_negative.AutoSize = true;
            this.lbl_negative.Location = new System.Drawing.Point(275, 110);
            this.lbl_negative.Name = "lbl_negative";
            this.lbl_negative.Size = new System.Drawing.Size(13, 13);
            this.lbl_negative.TabIndex = 6;
            this.lbl_negative.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_acessos);
            this.groupBox2.Controls.Add(this.lbl_acesso);
            this.groupBox2.Controls.Add(this.txt_instrucoes);
            this.groupBox2.Controls.Add(this.lbl_instrucoes);
            this.groupBox2.Location = new System.Drawing.Point(165, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(127, 76);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // txt_acessos
            // 
            this.txt_acessos.Location = new System.Drawing.Point(68, 19);
            this.txt_acessos.MaxLength = 5;
            this.txt_acessos.Name = "txt_acessos";
            this.txt_acessos.ReadOnly = true;
            this.txt_acessos.Size = new System.Drawing.Size(49, 20);
            this.txt_acessos.TabIndex = 10;
            // 
            // lbl_acesso
            // 
            this.lbl_acesso.AutoSize = true;
            this.lbl_acesso.Location = new System.Drawing.Point(6, 21);
            this.lbl_acesso.Name = "lbl_acesso";
            this.lbl_acesso.Size = new System.Drawing.Size(47, 13);
            this.lbl_acesso.TabIndex = 8;
            this.lbl_acesso.Text = "Acessos";
            // 
            // txt_instrucoes
            // 
            this.txt_instrucoes.Location = new System.Drawing.Point(68, 47);
            this.txt_instrucoes.MaxLength = 5;
            this.txt_instrucoes.Name = "txt_instrucoes";
            this.txt_instrucoes.ReadOnly = true;
            this.txt_instrucoes.Size = new System.Drawing.Size(49, 20);
            this.txt_instrucoes.TabIndex = 11;
            // 
            // lbl_instrucoes
            // 
            this.lbl_instrucoes.AutoSize = true;
            this.lbl_instrucoes.Location = new System.Drawing.Point(6, 51);
            this.lbl_instrucoes.Name = "lbl_instrucoes";
            this.lbl_instrucoes.Size = new System.Drawing.Size(56, 13);
            this.lbl_instrucoes.TabIndex = 9;
            this.lbl_instrucoes.Text = "Instruções";
            // 
            // lbl_zero
            // 
            this.lbl_zero.AutoSize = true;
            this.lbl_zero.Location = new System.Drawing.Point(275, 133);
            this.lbl_zero.Name = "lbl_zero";
            this.lbl_zero.Size = new System.Drawing.Size(13, 13);
            this.lbl_zero.TabIndex = 7;
            this.lbl_zero.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_pc);
            this.groupBox1.Controls.Add(this.lbl_pc);
            this.groupBox1.Controls.Add(this.lbl_ac);
            this.groupBox1.Controls.Add(this.txt_ac);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(88, 76);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // txt_pc
            // 
            this.txt_pc.Location = new System.Drawing.Point(34, 19);
            this.txt_pc.MaxLength = 4;
            this.txt_pc.Name = "txt_pc";
            this.txt_pc.ReadOnly = true;
            this.txt_pc.Size = new System.Drawing.Size(44, 20);
            this.txt_pc.TabIndex = 2;
            // 
            // lbl_pc
            // 
            this.lbl_pc.AutoSize = true;
            this.lbl_pc.Location = new System.Drawing.Point(7, 23);
            this.lbl_pc.Name = "lbl_pc";
            this.lbl_pc.Size = new System.Drawing.Size(21, 13);
            this.lbl_pc.TabIndex = 0;
            this.lbl_pc.Text = "PC";
            // 
            // lbl_ac
            // 
            this.lbl_ac.AutoSize = true;
            this.lbl_ac.Location = new System.Drawing.Point(7, 48);
            this.lbl_ac.Name = "lbl_ac";
            this.lbl_ac.Size = new System.Drawing.Size(21, 13);
            this.lbl_ac.TabIndex = 1;
            this.lbl_ac.Text = "AC";
            // 
            // txt_ac
            // 
            this.txt_ac.Location = new System.Drawing.Point(34, 45);
            this.txt_ac.MaxLength = 4;
            this.txt_ac.Name = "txt_ac";
            this.txt_ac.ReadOnly = true;
            this.txt_ac.Size = new System.Drawing.Size(44, 20);
            this.txt_ac.TabIndex = 3;
            // 
            // groupAssembler
            // 
            this.groupAssembler.Controls.Add(this.txtAssembler);
            this.groupAssembler.Location = new System.Drawing.Point(640, 27);
            this.groupAssembler.Name = "groupAssembler";
            this.groupAssembler.Size = new System.Drawing.Size(317, 432);
            this.groupAssembler.TabIndex = 3;
            this.groupAssembler.TabStop = false;
            this.groupAssembler.Text = "Montador";
            // 
            // txtAssembler
            // 
            this.txtAssembler.Location = new System.Drawing.Point(7, 20);
            this.txtAssembler.Multiline = true;
            this.txtAssembler.Name = "txtAssembler";
            this.txtAssembler.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAssembler.Size = new System.Drawing.Size(304, 406);
            this.txtAssembler.TabIndex = 0;
            this.txtAssembler.WordWrap = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 471);
            this.Controls.Add(this.groupAssembler);
            this.Controls.Add(this.groupSimulator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindows";
            this.groupSimulator.ResumeLayout(false);
            this.groupSimulator.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupAssembler.ResumeLayout(false);
            this.groupAssembler.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupSimulator;
        private System.Windows.Forms.GroupBox groupAssembler;
        private System.Windows.Forms.TextBox txtAssembler;
        private System.Windows.Forms.Label lbl_pc;
        private System.Windows.Forms.Label lbl_instrucoes;
        private System.Windows.Forms.Label lbl_acesso;
        private System.Windows.Forms.Label lbl_negativeText;
        private System.Windows.Forms.Label lbl_zeroText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public System.Windows.Forms.TextBox txt_pc;
        public System.Windows.Forms.Button btn_rodar;
        public System.Windows.Forms.TextBox txt_ac;
        public System.Windows.Forms.TextBox txt_instrucoes;
        public System.Windows.Forms.TextBox txt_acessos;
        public System.Windows.Forms.Label lbl_negative;
        public System.Windows.Forms.Label lbl_zero;
        public System.Windows.Forms.Button btn_passoapasso;
        private System.Windows.Forms.Label lbl_ac;
    }
}

