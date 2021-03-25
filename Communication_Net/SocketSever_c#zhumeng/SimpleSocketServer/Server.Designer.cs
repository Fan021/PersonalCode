namespace SocketSever
{
    partial class Server
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
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonStartListen = new System.Windows.Forms.Button();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelIP.Location = new System.Drawing.Point(44, 33);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(69, 13);
            this.labelIP.TabIndex = 0;
            this.labelIP.Text = "SeverIP：";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(119, 31);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(161, 21);
            this.textBoxIP.TabIndex = 1;
            this.textBoxIP.Text = "10.190.22.57";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPort.Location = new System.Drawing.Point(44, 70);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(83, 13);
            this.labelPort.TabIndex = 0;
            this.labelPort.Text = "SeverPort：";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(119, 68);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(161, 21);
            this.textBoxPort.TabIndex = 1;
            this.textBoxPort.Text = "5566";
            // 
            // buttonStartListen
            // 
            this.buttonStartListen.Location = new System.Drawing.Point(299, 27);
            this.buttonStartListen.Name = "buttonStartListen";
            this.buttonStartListen.Size = new System.Drawing.Size(82, 26);
            this.buttonStartListen.TabIndex = 2;
            this.buttonStartListen.Text = "StartListen";
            this.buttonStartListen.UseVisualStyleBackColor = true;
            this.buttonStartListen.Click += new System.EventHandler(this.buttonStartListen_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.Location = new System.Drawing.Point(9, 190);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(381, 52);
            this.labelMessage.TabIndex = 3;
            this.labelMessage.Text = "Message";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(299, 70);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "StopListen";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 251);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.buttonStartListen);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelIP);
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonStartListen;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonStop;
    }
}

