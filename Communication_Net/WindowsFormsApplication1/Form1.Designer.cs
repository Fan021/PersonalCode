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
            this.Send_utton1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Stop_button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Start_button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Init_button1
            // 
            this.Init_button1.Location = new System.Drawing.Point(7, 12);
            this.Init_button1.Name = "Init_button1";
            this.Init_button1.Size = new System.Drawing.Size(68, 27);
            this.Init_button1.TabIndex = 0;
            this.Init_button1.Text = "Init";
            this.Init_button1.UseVisualStyleBackColor = true;
            this.Init_button1.Click += new System.EventHandler(this.Init_button1_Click);
            // 
            // Send_utton1
            // 
            this.Send_utton1.Location = new System.Drawing.Point(7, 45);
            this.Send_utton1.Name = "Send_utton1";
            this.Send_utton1.Size = new System.Drawing.Size(68, 27);
            this.Send_utton1.TabIndex = 0;
            this.Send_utton1.Text = "Send";
            this.Send_utton1.UseVisualStyleBackColor = true;
            this.Send_utton1.Click += new System.EventHandler(this.Send_utton1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(104, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 21);
            this.textBox1.TabIndex = 1;
            // 
            // Stop_button1
            // 
            this.Stop_button1.Location = new System.Drawing.Point(107, 78);
            this.Stop_button1.Name = "Stop_button1";
            this.Stop_button1.Size = new System.Drawing.Size(68, 27);
            this.Stop_button1.TabIndex = 0;
            this.Stop_button1.Text = "Stop";
            this.Stop_button1.UseVisualStyleBackColor = true;
            this.Stop_button1.Click += new System.EventHandler(this.Stop_button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(7, 111);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(265, 21);
            this.textBox2.TabIndex = 1;
            // 
            // Start_button1
            // 
            this.Start_button1.Location = new System.Drawing.Point(7, 78);
            this.Start_button1.Name = "Start_button1";
            this.Start_button1.Size = new System.Drawing.Size(68, 27);
            this.Start_button1.TabIndex = 0;
            this.Start_button1.Text = "Start";
            this.Start_button1.UseVisualStyleBackColor = true;
            this.Start_button1.Click += new System.EventHandler(this.Start_button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Start_button1);
            this.Controls.Add(this.Stop_button1);
            this.Controls.Add(this.Send_utton1);
            this.Controls.Add(this.Init_button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Init_button1;
        private System.Windows.Forms.Button Send_utton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Stop_button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Start_button1;
    }
}

