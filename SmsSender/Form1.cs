using System;
using System.Windows.Forms;

using Apache.NMS;
using System.Collections.Generic;

using System.Net;
using System.Configuration;
using System.Net.Sockets;
using System.Xml;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Threading;

namespace SmsSender
{
    public partial class Form1 : Form
    {
        
        CustomerCreater cust = new CustomerCreater();
        String ipValue = "";
        Thread newThread;
        
        public Form1()
        {            
            InitializeComponent();
            this.initService();
            
            
        }
        private Boolean ConnectSms()
        {

            String TypeStr = "";
            String CopyRightToCOM = "";
            String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";

            if (WaveComTools.Sms_Connection(CopyRightStr, uint.Parse(Program.comport), 9600, out TypeStr, out CopyRightToCOM) == 1) ///5为串口号，0为红外接口，1,2,3,...为串口
            {
                State_Show.Text = TypeStr;
                Sms_Connection_Button.Enabled = false;
                Sms_Disconnection_Button.Enabled = true;
                Sms_Send_Button.Enabled = true;
                return true;

            }
            else
            {
                State_Show.Text = "连接失败！";
                Sms_Connection_Button.Enabled = true;
                Sms_Disconnection_Button.Enabled = false;
                Sms_Send_Button.Enabled = false;
                return false;
            }
        }

        private void Sms_Connection_Button_Click(object sender, EventArgs e)
        {
            this.initService();

        }
        private void initService()
        {
            Program.logWriter.WriteLine("-----------------服务初始化------------------");
            Program.logWriter.WriteLine("===========连接短信猫=========");
            GetLocalSetting();
            if (!ConnectSms())
            {
                Program.logWriter.WriteLine("Exception:===========连接短信猫失败=========");
                return;
            }
            Program.logWriter.WriteLine("===========创建消费者=========");
            cust.CreateBindCustomer();
            cust.CreateAppCustomer();
            Program.logWriter.WriteLine("===========创建消费者成功=========");
                                                          
            newThread =new Thread(new ThreadStart(this.work));            
            newThread.Start();

            Program.logWriter.WriteLine("===========服务器启动成功=========");
            Program.logWriter.WriteLine("-----------------服务初始化完成------------------");
            Sms_Connection_Button.Enabled = false;
            Sms_Disconnection_Button.Enabled = true;
            Sms_Send_Button.Enabled = true;
        }



        private void Sms_Disconnection_Button_Click(object sender, EventArgs e)
        {
            WaveComTools.Sms_Disconnection();
            Sms_Connection_Button.Enabled = true;
            Sms_Disconnection_Button.Enabled = false;
            Sms_Send_Button.Enabled = false;
        }

        private void work()
        {
            while (true)
            {
                if (Program.mqconnection == false)
                {
                    cust.CreateBindCustomer();
                    cust.CreateAppCustomer();
                }
                ReSendMsg();
                ReceivedMsg();
                ReportServerStatus();                
                Thread.Sleep(Program.frequency);
            }
        }
        
        private void ReSendMsg()
        {
            Program.logWriter.WriteLine("定时任务开始" );
            uint num = 0;
            List<SmsBindTemp> reSendList = DbAction.GetReSendList();
            if (reSendList == null)
                return;
            for (int i = 0; i < reSendList.Count; i++)
            {
                SmsBindTemp temp = reSendList[i];
                if (temp.getCounter() == 50)
                {
                    string str = "ZLDJ#" + temp.getPhone() + "#" + temp.getIdNumber() + "#" + temp.getName() + "#" + temp.getAddress().Replace("#", "号");
                    Program.logWriter.WriteLine("发送短信：" + str);
                    num = WaveComTools.Sms_Send("10086", str);
                    if (num != 1)
                    {
                        Program.logWriter.WriteLine("发送结果：" + num.ToString());
                        this.cust.CreateAlertProducer("15306185107", "短信服务器"+ipValue+" 内容：" + str + "发送失败。");
                        //Program.logWriter.WriteLine("重新连接短信猫!");
                        //ConnectSms();
                        //Program.logWriter.WriteLine("重新连接短信猫完成!");
                    }
                    Program.logWriter.WriteLine("发送短信完成！");
                    temp.setCounter(0);
                    DbAction.UpdateCount(temp);
                }
                else
                {
                    temp.setCounter(temp.getCounter() + 1);
                    DbAction.UpdateCount(temp);
                }
            }
        }
        
        private void ReceivedMsg()
        {
          
            Program.logWriter.WriteLine("开始检查短信息内容.");			
			string text = "";
			uint num = WaveComTools.Sms_Receive("4", out text);
			Program.logWriter.WriteLine("读取结果：" + num);
            if (num == 1u)
            {
                Program.logWriter.WriteLine("读取到新的短信息，开始处理.");
                Program.logWriter.WriteLine("短信息内容为：" + text);
                List<SmsBindTemp> reSendList = DbAction.GetReSendList();
                if (reSendList == null)
                    return;
                if (text != null)
                {
                    string[] array = text.Split('|');
                    for (int i = 1; i < array.Length; i++)
                    {
                        string[] array2 = array[i].Split('#');
                        for (int j = 0; j < reSendList.Count; j++)
                        {
                            SmsBindTemp smsBindTemp = reSendList[j];
                            if (array[i].Contains("尊敬的客户您好，您提交的") && array[i].Contains(smsBindTemp.getPhone()) && array[i].Contains("的身份登记资料已提交成功。在号码使用前，如登记信息有误，可通过本机发送“QXDJ#预配号"))
                            {
                                this.successHandler(array2[3], smsBindTemp, 1);
                            }
                            else
                            {
                                if (array[i].Contains("的号码已办理身份证件确认，无需重复办理。中国移动") && array[i].Contains(smsBindTemp.getPhone()))
                                {
                                    this.successHandler(array2[3], smsBindTemp, 1);
                                }
                                else
                                {
                                    if (array[i].Contains(smsBindTemp.getPhone()) && array[i].Contains("已登记过，无须再次登记，谢谢您的支持"))
                                    {
                                        this.successHandler(array2[3], smsBindTemp, 1);
                                    }
                                    else
                                    {
                                        if (array[i].Contains("对不起，因系统繁忙，") && (array[i].Contains(smsBindTemp.getPhone()) & array[i].Contains("提交的身份资料未能登记成功，请稍后重试。")))
                                        {
                                            this.failedHandler(array2[3], smsBindTemp, 2);
                                        }
                                        else
                                        {
                                            if (array[i].Contains(smsBindTemp.getPhone()) && array[i].Contains("提交的身份证号码有误，请重新输入。"))
                                            {
                                                this.failedHandler(array2[3], smsBindTemp, 3);
                                            }
                                            else
                                            {
                                                if (array[i].Contains("您好！对不起，") && array[i].Contains(smsBindTemp.getPhone()) && array[i].Contains("所输入的指令不正确，登记身份资料请发送短信“ZLDJ#要登记的号码#用户身份证号"))
                                                {
                                                    this.failedHandler(array2[3], smsBindTemp, 4);
                                                }
                                                else
                                                {
                                                    if (array[i].Contains("提交的客户名称或者地址不符合要求：客户名称至少2个汉字或4个字符；地址至少8个汉字或16个字符，请重新输入") && array[i].Contains(smsBindTemp.getPhone()))
                                                    {
                                                        this.failedHandler(array2[3], smsBindTemp, 5);
                                                    }
                                                    else
                                                    {
                                                        if (array[i].Contains("您登记的号码不存在或号码错误"))
                                                        {
                                                            this.failedHandler(array2[3], smsBindTemp, 6);
                                                        }
                                                        else
                                                        {
                                                            Program.logWriter.WriteLine("收到未知短信内容：" + array[i]);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    this.SetText(text);
                    Program.logWriter.WriteLine("收到短信：" + text);
                    Program.logWriter.WriteLine("开始删除短信息!");
                    this.batchDeleteMsg(array);
                    Program.logWriter.WriteLine("删除短息完成!");
                }
            }
        }

        private void successHandler(string content, SmsBindTemp temp, int type)
        {
            SmsBind sms = MTO.ToSmsBindObject(content, "success", type);
            sms.setBindphone(temp.getPhone());
            sms.setSn(temp.getSn());
            sms.setUuid(temp.getUuid());
            DbAction.InsertSmsBind(sms);
            this.cust.BindProductor("BIND_IDNO_RESPONSE_", temp, sms);
            DbAction.UpdateSmsBindTempReceiveFlag("1", temp);
        }

        private void failedHandler(string content, SmsBindTemp temp, int type)
        {
            SmsBind sms = MTO.ToSmsBindObject(content, "unsuccess", type);
            sms.setBindphone(temp.getPhone());
            sms.setSn(temp.getSn());
            sms.setUuid(temp.getUuid());
            DbAction.InsertSmsBind(sms);
            this.cust.BindProductor("BIND_IDNO_RESPONSE_", temp, sms);
            DbAction.UpdateSmsBindTempReceiveFlag("2", temp);
            if (type == 5||type==3)
            {
                SendMail("tingjun.pu@rokyinfo.com", "浦廷钧", "tingjun.pu@rokyinfo.com;ying.hua@rokyinfo.com;alex.li@rokyinfo.com", "Error", content, "tingjun.pu", "listandmount", "smtp.ym.163.com", "");
            }
        }

        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            if (this.ReceiveSms_Text.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { "\n" + text });
            }
            else
            {
                this.ReceiveSms_Text.Text = "\n" + text;
            }
        }



        private void SetLabText(string text)
        {
            if (this.t_server_status.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabText);
                this.Invoke(d, new object[] { "\n" + text });
            }
            else
            {
                this.t_server_status.Text = "\n" + text;
            }
        }
        private void batchDeleteMsg(String[] SmsArray)
        {
            for (int i = 0; i < SmsArray.Length; i++)
            {
                String Msg = SmsArray[i];
                String[] ReceiveSmsStrArray = Msg.Split('#');
                DeleteMsg(ReceiveSmsStrArray[0]);
            }

        }
        private void DeleteMsg(String index)
        {
            WaveComTools.Sms_Delete(index);            
        }
        private Boolean DetectSms()
        {
            if (WaveComTools.Sms_NewFlag() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Sms_Send_Button_Click(object sender, EventArgs e)
        {
            
            if (WaveComTools.Sms_Send(TelNum_Text.Text, SendSms_Text.Text) == 1)
            {
                MessageBox.Show("发送成功!");
            }
            else
            {
                MessageBox.Show("发送失败!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btbalancealert_Click(object sender, EventArgs e)
        {
            this.btbalancealert.Enabled = false;
            List<SimExtendInfo> list = DbAction.getExtendSimInfo();
            for (int i = 0; i < list.Count; i++)
            {
                Program.logWriter.WriteLine(list[i].Phone_number + "您的电动车gps防盗追踪器余额不足，请在月底前去中国移动营业厅充值，充值号码请查看客户端显示的SIM号码！或拨打客服电话及经销商查询！");
                this.cust.CreateAlertProducer(list[i].Phone_number, "您的电动车gps防盗追踪器余额不足，请在月底前去中国移动营业厅充值，充值号码请查看客户端显示的SIM号码！或拨打客服电话及经销商查询！");
                
            }
            this.btbalancealert.Enabled = true;

        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            Program.logWriter.WriteLine("开始上报服务器状态！");
            HttpClient client = new HttpClient("http://192.168.102.201:8000/update/?name=smsmodem:" + ipValue + "&status=OK&comment=report&cate=service");
            String a=client.GetString();
            Program.logWriter.WriteLine("上报服务器状态结束！"+a);            
        }
        private void ReportServerStatus()
        {
            Program.logWriter.WriteLine("开始上报服务器状态！");            
            try
            {
                HttpClient client = new HttpClient("http://192.168.102.201:8000/update/?name=smsmodem:" + ipValue + "&status=OK&comment=report&cate=service");                
                String a = client.GetString();
                GetResult(a);
                
            }
            catch(Exception ex)
            {
                Program.logWriter.WriteLine("服务器上报失败："+ex.Message.ToString());
                GetResult("服务器上报失败：" + ex.Message.ToString());
            }
            Program.logWriter.WriteLine("上报服务器状态结束！");
        }
        private void GetResult(String result)
        {
            DateTime dt = DateTime.Now;
            Program.logWriter.WriteLine(dt.ToString() + ":上报服务器状态为" + result);
            this.SetLabText("\n" + dt.ToString() + result);
        }

        private void  GetLocalSetting()
        {            
            XmlReader();
        }
        private void XmlReader()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("setting.xml");
            System.Xml.XmlNodeList nodes = doc.SelectNodes("//config");
            foreach (System.Xml.XmlNode xn in nodes)
            {
                ipValue=xn.SelectSingleNode("machine").InnerText;
                Program.mysql = xn.SelectSingleNode("mysql").InnerText;
                Program.mq = xn.SelectSingleNode("mq").InnerText;
                Program.mquser = xn.SelectSingleNode("mquser").InnerText;
                Program.mqpwd = xn.SelectSingleNode("mqpwd").InnerText;
                Program.frequency = int.Parse(xn.SelectSingleNode("frequency").InnerText);
                Program.comport = xn.SelectSingleNode("comport").InnerText;
                this.MobPort.Text = Program.comport;
            }
            Program.logWriter.WriteLine("获取配置参数：" + ipValue);

        }
        private string SendMail(string from, string fromname, string to, string subject, string body, string username, string password, string server, string fujian)
        {

            try
            {
                //邮件发送类 
                MailMessage mail = new MailMessage();

                //是谁发送的邮件 

                mail.From = new MailAddress(from, fromname);

                //发送给谁 
                String[] tolist = to.Split(';');
                for (int i = 0; i < tolist.Length; i++) 
                {
                    mail.To.Add(tolist[i]);
                }

                //标题 

                mail.Subject = subject;

                //内容编码 

                mail.BodyEncoding = Encoding.Default;

                //发送优先级 

                mail.Priority = MailPriority.High;

                //邮件内容 

                mail.Body = body;

                //是否HTML形式发送 

                mail.IsBodyHtml = true;

                //附件 

                if (fujian.Length > 0)
                {

                    mail.Attachments.Add(new Attachment(fujian));

                }

                //邮件服务器和端口 

                SmtpClient smtp = new SmtpClient(server, 25);

                smtp.UseDefaultCredentials = true;

                //指定发送方式 

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //指定登录名和密码 
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //超时时间 

                smtp.Timeout = 10000;

                smtp.Send(mail);

                return "send ok";

            }

            catch (Exception exp)
            {

                return exp.Message;

            }

        }
    }    
}
