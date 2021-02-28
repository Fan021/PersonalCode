namespace StudentDatabase
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
            this.create_button = new System.Windows.Forms.Button();
            this.Add_button = new System.Windows.Forms.Button();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Search_button = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Name_textBox = new System.Windows.Forms.TextBox();
            this.Age_textBox = new System.Windows.Forms.TextBox();
            this.Score_textBox = new System.Windows.Forms.TextBox();
            this.Name_label = new System.Windows.Forms.Label();
            this.Age_label = new System.Windows.Forms.Label();
            this.Subject_label = new System.Windows.Forms.Label();
            this.Score_label = new System.Windows.Forms.Label();
            this.Name_label1 = new System.Windows.Forms.Label();
            this.Name_textBox1 = new System.Windows.Forms.TextBox();
            this.Age_label1 = new System.Windows.Forms.Label();
            this.Age_textBox1 = new System.Windows.Forms.TextBox();
            this.Subject_label1 = new System.Windows.Forms.Label();
            this.Subject_textBox1 = new System.Windows.Forms.TextBox();
            this.Score_label1 = new System.Windows.Forms.Label();
            this.Score_textBox1 = new System.Windows.Forms.TextBox();
            this.Subject_listBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // create_button
            // 
            this.create_button.Location = new System.Drawing.Point(767, 12);
            this.create_button.Name = "create_button";
            this.create_button.Size = new System.Drawing.Size(90, 36);
            this.create_button.TabIndex = 0;
            this.create_button.Text = "CREATE";
            this.create_button.UseVisualStyleBackColor = true;
            this.create_button.Click += new System.EventHandler(this.create_button_Click);
            // 
            // Add_button
            // 
            this.Add_button.BackColor = System.Drawing.Color.Gainsboro;
            this.Add_button.Location = new System.Drawing.Point(121, 481);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(90, 36);
            this.Add_button.TabIndex = 1;
            this.Add_button.Text = "ADD";
            this.Add_button.UseVisualStyleBackColor = false;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(283, 481);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(90, 36);
            this.Update_button.TabIndex = 2;
            this.Update_button.Text = "UPDATE";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(447, 481);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(90, 36);
            this.Delete_button.TabIndex = 3;
            this.Delete_button.Text = "DELETE";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Search_button
            // 
            this.Search_button.Location = new System.Drawing.Point(767, 78);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(90, 35);
            this.Search_button.TabIndex = 4;
            this.Search_button.Text = "SEARCH";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 9);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(731, 453);
            this.dataGridView1.TabIndex = 5;
            // 
            // Name_textBox
            // 
            this.Name_textBox.Location = new System.Drawing.Point(14, 490);
            this.Name_textBox.Name = "Name_textBox";
            this.Name_textBox.Size = new System.Drawing.Size(85, 21);
            this.Name_textBox.TabIndex = 6;
            this.Name_textBox.TextChanged += new System.EventHandler(this.Name_textBox_TextChanged);
            // 
            // Age_textBox
            // 
            this.Age_textBox.Location = new System.Drawing.Point(16, 531);
            this.Age_textBox.Name = "Age_textBox";
            this.Age_textBox.Size = new System.Drawing.Size(83, 21);
            this.Age_textBox.TabIndex = 6;
            this.Age_textBox.TextChanged += new System.EventHandler(this.Name_textBox_TextChanged);
            // 
            // Score_textBox
            // 
            this.Score_textBox.Location = new System.Drawing.Point(16, 614);
            this.Score_textBox.Name = "Score_textBox";
            this.Score_textBox.Size = new System.Drawing.Size(83, 21);
            this.Score_textBox.TabIndex = 6;
            this.Score_textBox.TextChanged += new System.EventHandler(this.Name_textBox_TextChanged);
            // 
            // Name_label
            // 
            this.Name_label.AutoSize = true;
            this.Name_label.Location = new System.Drawing.Point(12, 475);
            this.Name_label.Name = "Name_label";
            this.Name_label.Size = new System.Drawing.Size(29, 12);
            this.Name_label.TabIndex = 7;
            this.Name_label.Text = "Name";
            // 
            // Age_label
            // 
            this.Age_label.AutoSize = true;
            this.Age_label.Location = new System.Drawing.Point(14, 516);
            this.Age_label.Name = "Age_label";
            this.Age_label.Size = new System.Drawing.Size(23, 12);
            this.Age_label.TabIndex = 7;
            this.Age_label.Text = "Age";
            // 
            // Subject_label
            // 
            this.Subject_label.AutoSize = true;
            this.Subject_label.Location = new System.Drawing.Point(14, 557);
            this.Subject_label.Name = "Subject_label";
            this.Subject_label.Size = new System.Drawing.Size(47, 12);
            this.Subject_label.TabIndex = 7;
            this.Subject_label.Text = "Subject";
            // 
            // Score_label
            // 
            this.Score_label.AutoSize = true;
            this.Score_label.Location = new System.Drawing.Point(14, 599);
            this.Score_label.Name = "Score_label";
            this.Score_label.Size = new System.Drawing.Size(35, 12);
            this.Score_label.TabIndex = 7;
            this.Score_label.Text = "Score";
            // 
            // Name_label1
            // 
            this.Name_label1.AutoSize = true;
            this.Name_label1.Location = new System.Drawing.Point(781, 135);
            this.Name_label1.Name = "Name_label1";
            this.Name_label1.Size = new System.Drawing.Size(35, 12);
            this.Name_label1.TabIndex = 8;
            this.Name_label1.Text = "Name:";
            // 
            // Name_textBox1
            // 
            this.Name_textBox1.Location = new System.Drawing.Point(822, 129);
            this.Name_textBox1.Name = "Name_textBox1";
            this.Name_textBox1.Size = new System.Drawing.Size(85, 21);
            this.Name_textBox1.TabIndex = 9;
            // 
            // Age_label1
            // 
            this.Age_label1.AutoSize = true;
            this.Age_label1.Location = new System.Drawing.Point(781, 171);
            this.Age_label1.Name = "Age_label1";
            this.Age_label1.Size = new System.Drawing.Size(29, 12);
            this.Age_label1.TabIndex = 8;
            this.Age_label1.Text = "Age:";
            // 
            // Age_textBox1
            // 
            this.Age_textBox1.Location = new System.Drawing.Point(822, 167);
            this.Age_textBox1.Name = "Age_textBox1";
            this.Age_textBox1.Size = new System.Drawing.Size(85, 21);
            this.Age_textBox1.TabIndex = 9;
            // 
            // Subject_label1
            // 
            this.Subject_label1.AutoSize = true;
            this.Subject_label1.Location = new System.Drawing.Point(757, 205);
            this.Subject_label1.Name = "Subject_label1";
            this.Subject_label1.Size = new System.Drawing.Size(53, 12);
            this.Subject_label1.TabIndex = 8;
            this.Subject_label1.Text = "Subject:";
            // 
            // Subject_textBox1
            // 
            this.Subject_textBox1.Location = new System.Drawing.Point(822, 202);
            this.Subject_textBox1.Name = "Subject_textBox1";
            this.Subject_textBox1.Size = new System.Drawing.Size(85, 21);
            this.Subject_textBox1.TabIndex = 9;
            // 
            // Score_label1
            // 
            this.Score_label1.AutoSize = true;
            this.Score_label1.Location = new System.Drawing.Point(769, 241);
            this.Score_label1.Name = "Score_label1";
            this.Score_label1.Size = new System.Drawing.Size(41, 12);
            this.Score_label1.TabIndex = 8;
            this.Score_label1.Text = "Score:";
            // 
            // Score_textBox1
            // 
            this.Score_textBox1.Location = new System.Drawing.Point(822, 238);
            this.Score_textBox1.Name = "Score_textBox1";
            this.Score_textBox1.Size = new System.Drawing.Size(85, 21);
            this.Score_textBox1.TabIndex = 9;
            // 
            // Subject_listBox
            // 
            this.Subject_listBox.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Subject_listBox.FormattingEnabled = true;
            this.Subject_listBox.IntegralHeight = false;
            this.Subject_listBox.ItemHeight = 16;
            this.Subject_listBox.Items.AddRange(new object[] {
            "Chinese",
            "Math"});
            this.Subject_listBox.Location = new System.Drawing.Point(15, 572);
            this.Subject_listBox.Name = "Subject_listBox";
            this.Subject_listBox.Size = new System.Drawing.Size(83, 21);
            this.Subject_listBox.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 656);
            this.Controls.Add(this.Subject_listBox);
            this.Controls.Add(this.Score_textBox1);
            this.Controls.Add(this.Subject_textBox1);
            this.Controls.Add(this.Age_textBox1);
            this.Controls.Add(this.Name_textBox1);
            this.Controls.Add(this.Score_label1);
            this.Controls.Add(this.Subject_label1);
            this.Controls.Add(this.Age_label1);
            this.Controls.Add(this.Name_label1);
            this.Controls.Add(this.Score_label);
            this.Controls.Add(this.Subject_label);
            this.Controls.Add(this.Age_label);
            this.Controls.Add(this.Name_label);
            this.Controls.Add(this.Score_textBox);
            this.Controls.Add(this.Age_textBox);
            this.Controls.Add(this.Name_textBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Search_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.Add_button);
            this.Controls.Add(this.create_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button create_button;
        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox Name_textBox;
        private System.Windows.Forms.TextBox Age_textBox;
        private System.Windows.Forms.TextBox Score_textBox;
        private System.Windows.Forms.Label Name_label;
        private System.Windows.Forms.Label Age_label;
        private System.Windows.Forms.Label Subject_label;
        private System.Windows.Forms.Label Score_label;
        private System.Windows.Forms.Label Name_label1;
        private System.Windows.Forms.TextBox Name_textBox1;
        private System.Windows.Forms.Label Age_label1;
        private System.Windows.Forms.TextBox Age_textBox1;
        private System.Windows.Forms.Label Subject_label1;
        private System.Windows.Forms.TextBox Subject_textBox1;
        private System.Windows.Forms.Label Score_label1;
        private System.Windows.Forms.TextBox Score_textBox1;
        private System.Windows.Forms.ListBox Subject_listBox;
    }
}

