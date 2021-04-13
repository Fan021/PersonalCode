namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.Init_button1 = new System.Windows.Forms.Button();
            this.Send_button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Receive_button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Send_Cycle_button1 = new System.Windows.Forms.Button();
            this.Stop_button1 = new System.Windows.Forms.Button();
            this.Close_button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Init_button1
            // 
            this.Init_button1.Location = new System.Drawing.Point(14, 21);
            this.Init_button1.Name = "Init_button1";
            this.Init_button1.Size = new System.Drawing.Size(95, 35);
            this.Init_button1.TabIndex = 0;
            this.Init_button1.Text = "Init";
            this.Init_button1.UseVisualStyleBackColor = true;
            this.Init_button1.Click += new System.EventHandler(this.Init_button1_Click);
            // 
            // Send_button1
            // 
            this.Send_button1.Location = new System.Drawing.Point(12, 79);
            this.Send_button1.Name = "Send_button1";
            this.Send_button1.Size = new System.Drawing.Size(95, 35);
            this.Send_button1.TabIndex = 0;
            this.Send_button1.Text = "Send";
            this.Send_button1.UseVisualStyleBackColor = true;
            this.Send_button1.Click += new System.EventHandler(this.Send_button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(129, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(374, 21);
            this.textBox1.TabIndex = 1;
            // 
            // Receive_button1
            // 
            this.Receive_button1.Location = new System.Drawing.Point(12, 139);
            this.Receive_button1.Name = "Receive_button1";
            this.Receive_button1.Size = new System.Drawing.Size(95, 35);
            this.Receive_button1.TabIndex = 0;
            this.Receive_button1.Text = "Receive";
            this.Receive_button1.UseVisualStyleBackColor = true;
            this.Receive_button1.Click += new System.EventHandler(this.Receive_button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(129, 153);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(374, 21);
            this.textBox2.TabIndex = 1;
            // 
            // Send_Cycle_button1
            // 
            this.Send_Cycle_button1.Location = new System.Drawing.Point(12, 209);
            this.Send_Cycle_button1.Name = "Send_Cycle_button1";
            this.Send_Cycle_button1.Size = new System.Drawing.Size(95, 35);
            this.Send_Cycle_button1.TabIndex = 0;
            this.Send_Cycle_button1.Text = "SendCycle";
            this.Send_Cycle_button1.UseVisualStyleBackColor = true;
            this.Send_Cycle_button1.Click += new System.EventHandler(this.Send_Cycle_button1_Click);
            // 
            // Stop_button1
            // 
            this.Stop_button1.Location = new System.Drawing.Point(129, 209);
            this.Stop_button1.Name = "Stop_button1";
            this.Stop_button1.Size = new System.Drawing.Size(111, 35);
            this.Stop_button1.TabIndex = 0;
            this.Stop_button1.Text = "Stop_SendCycle";
            this.Stop_button1.UseVisualStyleBackColor = true;
            this.Stop_button1.Click += new System.EventHandler(this.Stop_button1_Click);
            // 
            // Close_button1
            // 
            this.Close_button1.Location = new System.Drawing.Point(129, 21);
            this.Close_button1.Name = "Close_button1";
            this.Close_button1.Size = new System.Drawing.Size(95, 35);
            this.Close_button1.TabIndex = 0;
            this.Close_button1.Text = "Close";
            this.Close_button1.UseVisualStyleBackColor = true;
            this.Close_button1.Click += new System.EventHandler(this.Close_button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 334);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Receive_button1);
            this.Controls.Add(this.Stop_button1);
            this.Controls.Add(this.Send_Cycle_button1);
            this.Controls.Add(this.Send_button1);
            this.Controls.Add(this.Close_button1);
            this.Controls.Add(this.Init_button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Init_button1;
        private System.Windows.Forms.Button Send_button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Receive_button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Send_Cycle_button1;
        private System.Windows.Forms.Button Stop_button1;
        private System.Windows.Forms.Button Close_button1;
    }
}

