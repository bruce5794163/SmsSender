namespace SmsSender
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MobPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Sms_Connection_Button = new System.Windows.Forms.Button();
            this.State_Show = new System.Windows.Forms.Label();
            this.Sms_Disconnection_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ReceiveSms_Text = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Sms_Send_Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TelNum_Text = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SendSms_Text = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btbalancealert = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.l_server_status = new System.Windows.Forms.Label();
            this.t_server_status = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MobPort
            // 
            this.MobPort.Location = new System.Drawing.Point(79, 35);
            this.MobPort.Name = "MobPort";
            this.MobPort.ReadOnly = true;
            this.MobPort.Size = new System.Drawing.Size(87, 21);
            this.MobPort.TabIndex = 3;
            this.MobPort.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "端口号：";
            // 
            // Sms_Connection_Button
            // 
            this.Sms_Connection_Button.Location = new System.Drawing.Point(11, 48);
            this.Sms_Connection_Button.Name = "Sms_Connection_Button";
            this.Sms_Connection_Button.Size = new System.Drawing.Size(76, 24);
            this.Sms_Connection_Button.TabIndex = 1;
            this.Sms_Connection_Button.Text = "启动服务";
            this.Sms_Connection_Button.Click += new System.EventHandler(this.Sms_Connection_Button_Click);
            // 
            // State_Show
            // 
            this.State_Show.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.State_Show.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.State_Show.Location = new System.Drawing.Point(10, 17);
            this.State_Show.Name = "State_Show";
            this.State_Show.Size = new System.Drawing.Size(221, 24);
            this.State_Show.TabIndex = 0;
            this.State_Show.Text = "服务状态";
            this.State_Show.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Sms_Disconnection_Button
            // 
            this.Sms_Disconnection_Button.Enabled = false;
            this.Sms_Disconnection_Button.Location = new System.Drawing.Point(155, 48);
            this.Sms_Disconnection_Button.Name = "Sms_Disconnection_Button";
            this.Sms_Disconnection_Button.Size = new System.Drawing.Size(76, 24);
            this.Sms_Disconnection_Button.TabIndex = 2;
            this.Sms_Disconnection_Button.Text = "关闭服务";
            this.Sms_Disconnection_Button.Click += new System.EventHandler(this.Sms_Disconnection_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Sms_Disconnection_Button);
            this.groupBox1.Controls.Add(this.Sms_Connection_Button);
            this.groupBox1.Controls.Add(this.State_Show);
            this.groupBox1.Location = new System.Drawing.Point(275, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 80);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ReceiveSms_Text);
            this.groupBox4.Location = new System.Drawing.Point(13, 99);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(238, 232);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "接收短信";
            // 
            // ReceiveSms_Text
            // 
            this.ReceiveSms_Text.Location = new System.Drawing.Point(8, 24);
            this.ReceiveSms_Text.Multiline = true;
            this.ReceiveSms_Text.Name = "ReceiveSms_Text";
            this.ReceiveSms_Text.Size = new System.Drawing.Size(224, 192);
            this.ReceiveSms_Text.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Sms_Send_Button);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TelNum_Text);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.SendSms_Text);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(279, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 232);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送短信";
            // 
            // Sms_Send_Button
            // 
            this.Sms_Send_Button.Enabled = false;
            this.Sms_Send_Button.Location = new System.Drawing.Point(72, 200);
            this.Sms_Send_Button.Name = "Sms_Send_Button";
            this.Sms_Send_Button.Size = new System.Drawing.Size(112, 24);
            this.Sms_Send_Button.TabIndex = 5;
            this.Sms_Send_Button.Text = "发送";
            this.Sms_Send_Button.Click += new System.EventHandler(this.Sms_Send_Button_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 32);
            this.label6.TabIndex = 4;
            this.label6.Text = "注：发送内容最多70个汉字或180个英文字母, 超长时自动分段发送。";
            // 
            // TelNum_Text
            // 
            this.TelNum_Text.Location = new System.Drawing.Point(8, 136);
            this.TelNum_Text.Name = "TelNum_Text";
            this.TelNum_Text.Size = new System.Drawing.Size(212, 21);
            this.TelNum_Text.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "手机号码：";
            // 
            // SendSms_Text
            // 
            this.SendSms_Text.Location = new System.Drawing.Point(8, 40);
            this.SendSms_Text.Multiline = true;
            this.SendSms_Text.Name = "SendSms_Text";
            this.SendSms_Text.Size = new System.Drawing.Size(212, 72);
            this.SendSms_Text.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "短信内容：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MobPort);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(17, 11);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 79);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "端口设置";
            // 
            // btbalancealert
            // 
            this.btbalancealert.Location = new System.Drawing.Point(461, 350);
            this.btbalancealert.Name = "btbalancealert";
            this.btbalancealert.Size = new System.Drawing.Size(75, 23);
            this.btbalancealert.TabIndex = 11;
            this.btbalancealert.Text = "短信告知";
            this.btbalancealert.UseVisualStyleBackColor = true;
            this.btbalancealert.Click += new System.EventHandler(this.btbalancealert_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(287, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "服务器状态：";
            // 
            // l_server_status
            // 
            this.l_server_status.AutoSize = true;
            this.l_server_status.Location = new System.Drawing.Point(117, 348);
            this.l_server_status.Name = "l_server_status";
            this.l_server_status.Size = new System.Drawing.Size(0, 12);
            this.l_server_status.TabIndex = 14;
            // 
            // t_server_status
            // 
            this.t_server_status.Location = new System.Drawing.Point(96, 345);
            this.t_server_status.Name = "t_server_status";
            this.t_server_status.Size = new System.Drawing.Size(165, 21);
            this.t_server_status.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 385);
            this.Controls.Add(this.t_server_status);
            this.Controls.Add(this.l_server_status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btbalancealert);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "短信发送器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MobPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Sms_Connection_Button;
        private System.Windows.Forms.Label State_Show;
        private System.Windows.Forms.Button Sms_Disconnection_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox ReceiveSms_Text;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Sms_Send_Button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TelNum_Text;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SendSms_Text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btbalancealert;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label l_server_status;
        private System.Windows.Forms.TextBox t_server_status;
    }
}

