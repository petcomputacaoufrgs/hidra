namespace Hidra
{
    partial class Ramses
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
            this.lbl_carryText = new System.Windows.Forms.Label();
            this.lbl_carry = new System.Windows.Forms.Label();
            this.lbl_rx = new System.Windows.Forms.Label();
            this.txt_rb = new System.Windows.Forms.TextBox();
            this.lbl_rb = new System.Windows.Forms.Label();
            this.txt_rx = new System.Windows.Forms.TextBox();
            this.txt_ra = new System.Windows.Forms.TextBox();
            this.groupSimulator.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxBits.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_pc
            // 
            this.txt_pc.Location = new System.Drawing.Point(116, 45);
            // 
            // btn_rodar
            // 
            this.btn_rodar.Location = new System.Drawing.Point(152, 121);
            // 
            // txt_ac
            // 
            this.txt_ac.Location = new System.Drawing.Point(33, 15);
            this.txt_ac.Visible = false;
            // 
            // txt_instrucoes
            // 
            this.txt_instrucoes.Location = new System.Drawing.Point(68, 45);
            this.txt_instrucoes.Size = new System.Drawing.Size(45, 20);
            // 
            // txt_acessos
            // 
            this.txt_acessos.Location = new System.Drawing.Point(68, 16);
            this.txt_acessos.Size = new System.Drawing.Size(45, 20);
            // 
            // btn_passoapasso
            // 
            this.btn_passoapasso.Location = new System.Drawing.Point(216, 115);
            // 
            // groupSimulator
            // 
            this.groupSimulator.Size = new System.Drawing.Size(298, 175);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(6, 94);
            this.groupBox2.Size = new System.Drawing.Size(119, 73);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_ra);
            this.groupBox1.Controls.Add(this.txt_rb);
            this.groupBox1.Controls.Add(this.lbl_rb);
            this.groupBox1.Controls.Add(this.txt_rx);
            this.groupBox1.Controls.Add(this.lbl_rx);
            this.groupBox1.Size = new System.Drawing.Size(167, 74);
            this.groupBox1.Controls.SetChildIndex(this.lbl_rx, 0);
            this.groupBox1.Controls.SetChildIndex(this.txt_rx, 0);
            this.groupBox1.Controls.SetChildIndex(this.lbl_rb, 0);
            this.groupBox1.Controls.SetChildIndex(this.txt_rb, 0);
            this.groupBox1.Controls.SetChildIndex(this.lbl_ac, 0);
            this.groupBox1.Controls.SetChildIndex(this.txt_ac, 0);
            this.groupBox1.Controls.SetChildIndex(this.lbl_pc, 0);
            this.groupBox1.Controls.SetChildIndex(this.txt_pc, 0);
            this.groupBox1.Controls.SetChildIndex(this.txt_ra, 0);
            // 
            // lbl_pc
            // 
            this.lbl_pc.Location = new System.Drawing.Point(89, 48);
            // 
            // lbl_ac
            // 
            this.lbl_ac.Location = new System.Drawing.Point(6, 18);
            this.lbl_ac.Size = new System.Drawing.Size(22, 13);
            this.lbl_ac.Text = "RA";
            // 
            // groupBoxBits
            // 
            this.groupBoxBits.Controls.Add(this.lbl_carryText);
            this.groupBoxBits.Controls.Add(this.lbl_carry);
            this.groupBoxBits.Location = new System.Drawing.Point(194, 19);
            this.groupBoxBits.Size = new System.Drawing.Size(85, 74);
            this.groupBoxBits.Controls.SetChildIndex(this.lbl_carry, 0);
            this.groupBoxBits.Controls.SetChildIndex(this.lbl_carryText, 0);
            this.groupBoxBits.Controls.SetChildIndex(this.lbl_zeroText, 0);
            this.groupBoxBits.Controls.SetChildIndex(this.lbl_negative, 0);
            this.groupBoxBits.Controls.SetChildIndex(this.lbl_zero, 0);
            this.groupBoxBits.Controls.SetChildIndex(this.lbl_negativeText, 0);
            // 
            // lbl_carryText
            // 
            this.lbl_carryText.AutoSize = true;
            this.lbl_carryText.Location = new System.Drawing.Point(27, 53);
            this.lbl_carryText.Name = "lbl_carryText";
            this.lbl_carryText.Size = new System.Drawing.Size(31, 13);
            this.lbl_carryText.TabIndex = 8;
            this.lbl_carryText.Text = "Carry";
            // 
            // lbl_carry
            // 
            this.lbl_carry.AutoSize = true;
            this.lbl_carry.Location = new System.Drawing.Point(64, 53);
            this.lbl_carry.Name = "lbl_carry";
            this.lbl_carry.Size = new System.Drawing.Size(13, 13);
            this.lbl_carry.TabIndex = 9;
            this.lbl_carry.Text = "0";
            // 
            // lbl_rx
            // 
            this.lbl_rx.AutoSize = true;
            this.lbl_rx.Location = new System.Drawing.Point(88, 18);
            this.lbl_rx.Name = "lbl_rx";
            this.lbl_rx.Size = new System.Drawing.Size(22, 13);
            this.lbl_rx.TabIndex = 8;
            this.lbl_rx.Text = "RX";
            // 
            // txt_rb
            // 
            this.txt_rb.Location = new System.Drawing.Point(33, 45);
            this.txt_rb.Name = "txt_rb";
            this.txt_rb.ReadOnly = true;
            this.txt_rb.Size = new System.Drawing.Size(44, 20);
            this.txt_rb.TabIndex = 11;
            // 
            // lbl_rb
            // 
            this.lbl_rb.AutoSize = true;
            this.lbl_rb.Location = new System.Drawing.Point(6, 48);
            this.lbl_rb.Name = "lbl_rb";
            this.lbl_rb.Size = new System.Drawing.Size(22, 13);
            this.lbl_rb.TabIndex = 10;
            this.lbl_rb.Text = "RB";
            // 
            // txt_rx
            // 
            this.txt_rx.Location = new System.Drawing.Point(116, 15);
            this.txt_rx.Name = "txt_rx";
            this.txt_rx.ReadOnly = true;
            this.txt_rx.Size = new System.Drawing.Size(44, 20);
            this.txt_rx.TabIndex = 9;
            // 
            // txt_ra
            // 
            this.txt_ra.Location = new System.Drawing.Point(33, 15);
            this.txt_ra.Name = "txt_ra";
            this.txt_ra.ReadOnly = true;
            this.txt_ra.Size = new System.Drawing.Size(44, 20);
            this.txt_ra.TabIndex = 12;
            // 
            // Ramses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 471);
            this.Name = "Ramses";
            this.Text = "Ramses";
            this.groupSimulator.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxBits.ResumeLayout(false);
            this.groupBoxBits.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_carryText;
        private System.Windows.Forms.Label lbl_carry;
        private System.Windows.Forms.TextBox txt_rb;
        private System.Windows.Forms.Label lbl_rb;
        private System.Windows.Forms.Label lbl_rx;
        private System.Windows.Forms.TextBox txt_ra;
        private System.Windows.Forms.TextBox txt_rx;
    }
}