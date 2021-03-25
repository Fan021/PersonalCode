namespace SocketSever
{
    partial class FormTcpServer
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
            this.btnStartStopListen = new System.Windows.Forms.Button();
            this.textBoxReceivedMsg = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendToAllClient = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxClientList = new System.Windows.Forms.ComboBox();
            this.textBoxSendMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStartStopListen
            // 
            this.btnStartStopListen.Location = new System.Drawing.Point(672, 50);
            this.btnStartStopListen.Name = "btnStartStopListen";
            this.btnStartStopListen.Size = new System.Drawing.Size(104, 42);
            this.btnStartStopListen.TabIndex = 0;
            this.btnStartStopListen.Text = "开始监听";
            this.btnStartStopListen.UseVisualStyleBackColor = true;
            this.btnStartStopListen.Click += new System.EventHandler(this.btnStartStopListen_Click);
            // 
            // textBoxReceivedMsg
            // 
            this.textBoxReceivedMsg.Location = new System.Drawing.Point(12, 50);
            this.textBoxReceivedMsg.Multiline = true;
            this.textBoxReceivedMsg.Name = "textBoxReceivedMsg";
            this.textBoxReceivedMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReceivedMsg.Size = new System.Drawing.Size(639, 430);
            this.textBoxReceivedMsg.TabIndex = 3;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(252, 12);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(129, 21);
            this.textBoxPort.TabIndex = 4;
            this.textBoxPort.Text = "5566";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Port：";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(57, 12);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(129, 21);
            this.textBoxIP.TabIndex = 4;
            this.textBoxIP.Text = "10.190.22.67";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP：";
            // 
            // btnSendToAllClient
            // 
            this.btnSendToAllClient.Location = new System.Drawing.Point(672, 546);
            this.btnSendToAllClient.Name = "btnSendToAllClient";
            this.btnSendToAllClient.Size = new System.Drawing.Size(101, 41);
            this.btnSendToAllClient.TabIndex = 2;
            this.btnSendToAllClient.Text = "发送所有客户端";
            this.btnSendToAllClient.UseVisualStyleBackColor = true;
            this.btnSendToAllClient.Click += new System.EventHandler(this.btnSendToAllClient_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(559, 546);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(101, 41);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 503);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "客户端列表：";
            // 
            // comboBoxClientList
            // 
            this.comboBoxClientList.FormattingEnabled = true;
            this.comboBoxClientList.Location = new System.Drawing.Point(93, 500);
            this.comboBoxClientList.Name = "comboBoxClientList";
            this.comboBoxClientList.Size = new System.Drawing.Size(194, 20);
            this.comboBoxClientList.TabIndex = 7;
            // 
            // textBoxSendMsg
            // 
            this.textBoxSendMsg.Location = new System.Drawing.Point(12, 537);
            this.textBoxSendMsg.Multiline = true;
            this.textBoxSendMsg.Name = "textBoxSendMsg";
            this.textBoxSendMsg.Size = new System.Drawing.Size(521, 70);
            this.textBoxSendMsg.TabIndex = 8;
            // 
            // FormTcpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 619);
            this.Controls.Add(this.textBoxSendMsg);
            this.Controls.Add(this.comboBoxClientList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxReceivedMsg);
            this.Controls.Add(this.btnSendToAllClient);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnStartStopListen);
            this.Name = "FormTcpServer";
            this.Text = "FormTcpServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTcpServer_FormClosing);
            this.Load += new System.EventHandler(this.FrmTcpServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartStopListen;
        private System.Windows.Forms.TextBox textBoxReceivedMsg;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendToAllClient;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxClientList;
        private System.Windows.Forms.TextBox textBoxSendMsg;
    }
}