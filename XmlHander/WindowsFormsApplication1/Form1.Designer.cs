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
            this.Create_button1 = new System.Windows.Forms.Button();
            this.Add_button1 = new System.Windows.Forms.Button();
            this.read_button2 = new System.Windows.Forms.Button();
            this.Alert_button1 = new System.Windows.Forms.Button();
            this.Delete_button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Create_button1
            // 
            this.Create_button1.Location = new System.Drawing.Point(10, 21);
            this.Create_button1.Name = "Create_button1";
            this.Create_button1.Size = new System.Drawing.Size(73, 24);
            this.Create_button1.TabIndex = 0;
            this.Create_button1.Text = "Create";
            this.Create_button1.UseVisualStyleBackColor = true;
            this.Create_button1.Click += new System.EventHandler(this.Create_button1_Click);
            // 
            // Add_button1
            // 
            this.Add_button1.Location = new System.Drawing.Point(10, 51);
            this.Add_button1.Name = "Add_button1";
            this.Add_button1.Size = new System.Drawing.Size(73, 24);
            this.Add_button1.TabIndex = 0;
            this.Add_button1.Text = "Add";
            this.Add_button1.UseVisualStyleBackColor = true;
            this.Add_button1.Click += new System.EventHandler(this.Add_button1_Click);
            // 
            // read_button2
            // 
            this.read_button2.Location = new System.Drawing.Point(10, 81);
            this.read_button2.Name = "read_button2";
            this.read_button2.Size = new System.Drawing.Size(73, 24);
            this.read_button2.TabIndex = 0;
            this.read_button2.Text = "Read";
            this.read_button2.UseVisualStyleBackColor = true;
            this.read_button2.Click += new System.EventHandler(this.read_button2_Click);
            // 
            // Alert_button1
            // 
            this.Alert_button1.Location = new System.Drawing.Point(10, 111);
            this.Alert_button1.Name = "Alert_button1";
            this.Alert_button1.Size = new System.Drawing.Size(73, 24);
            this.Alert_button1.TabIndex = 0;
            this.Alert_button1.Text = "Alert";
            this.Alert_button1.UseVisualStyleBackColor = true;
            this.Alert_button1.Click += new System.EventHandler(this.Alert_button1_Click);
            // 
            // Delete_button1
            // 
            this.Delete_button1.Location = new System.Drawing.Point(10, 141);
            this.Delete_button1.Name = "Delete_button1";
            this.Delete_button1.Size = new System.Drawing.Size(73, 24);
            this.Delete_button1.TabIndex = 0;
            this.Delete_button1.Text = "Delete";
            this.Delete_button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Delete_button1);
            this.Controls.Add(this.Alert_button1);
            this.Controls.Add(this.read_button2);
            this.Controls.Add(this.Add_button1);
            this.Controls.Add(this.Create_button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Create_button1;
        private System.Windows.Forms.Button Add_button1;
        private System.Windows.Forms.Button read_button2;
        private System.Windows.Forms.Button Alert_button1;
        private System.Windows.Forms.Button Delete_button1;
    }
}

