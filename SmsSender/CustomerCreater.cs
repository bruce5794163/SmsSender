using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Configuration;


namespace SmsSender
{
    class CustomerCreater
    {
        private MQ m_mq;
        private IMessageConsumer m_consumer;
        

        public CustomerCreater()
        {
            initMq();
        }
        public void initMq()
        {
            try
            {
                m_mq = new MQ();                

            }
            catch (Exception ex)
            {
                Program.logWriter.WriteLine("CustomerCreater Exception:" + ex.Message);
            }
        }
        public void CreateBindCustomer()
        {
            try
            {
                m_consumer = m_mq.CreateConsumer(false, "BIND_IDNO_REQUEST");
                m_consumer.Listener += new MessageListener(BindCustomer_Listener);
                Program.mqconnection = true;
            }
            catch (System.Exception ex)
            {
                Program.logWriter.WriteLine("CreateBindCustomer Exception:" + ex.Message);
            }
        }
        public void BindProductor(String QueueName,SmsBindTemp temp,SmsBind vo)
        {
            try
            {                                
                m_mq.CreateProducer(false, QueueName + temp.getHostName());                
                List<Property> m_lstProperty = new List<Property>();

                //TODO//等待完成
                Property sn;
                sn.name = "sn";
                sn.value = temp.getSn();
                m_lstProperty.Add(sn);

                Property uuid;
                uuid.name = "uuid";
                uuid.value =temp.getUuid();
                m_lstProperty.Add(uuid);

                Property name;
                name.name = "name";
                name.value = temp.getName();
                m_lstProperty.Add(name);

                Property userId;
                userId.name = "userId";
                userId.value = temp.getUserId();
                m_lstProperty.Add(userId);
                Property phone;
                phone.name = "phone";
                phone.value = temp.getPhone();
                m_lstProperty.Add(phone);

                Property address;
                address.name = "address";
                address.value = temp.getAddress();
                m_lstProperty.Add(address);
                Property idNumber;
                idNumber.name = "idNumber";
                idNumber.value = temp.getIdNumber();
                Property content;
                content.name = "content";
                content.value = vo.getContent();
                m_lstProperty.Add(content);
                Property state;
                state.name = "state";
                state.value = vo.getState();
                m_lstProperty.Add(state);
                Property userMessageId;
                userMessageId.name = "userMessageId";
                userMessageId.value = temp.getUserMessageId();
                m_lstProperty.Add(userMessageId);
                m_mq.SendMQMessage("", m_lstProperty);
                Program.logWriter.WriteLine("=============MQ队列写入完成 电话号码："+temp.getPhone()+"=============");
            }
            catch (Exception ex)
            {
                Program.logWriter.WriteLine("BindProductor Exception:" + ex.Message);
                Program.mqconnection = false;
            }
        }
        
        public void CreateAppCustomer()
        {
            try
            {
                m_consumer = m_mq.CreateConsumer(false, "BIND_APP_REQUEST");
                m_consumer.Listener += new MessageListener(BindCustomer_Listener);
                Program.mqconnection = true;
            }
            catch (System.Exception ex)
            {
                Program.logWriter.WriteLine("BIND_APP_REQUEST Exception:" + ex.Message);
            }

        }
        private void BindCustomer_Listener(IMessage message)
        {
            Program.logWriter.WriteLine(message.ToString());
            DbAction.InsertSmsBindTemp(message);
            SmsBindTemp sms=MTO.TosmsBindTempObject(message);
            SmsBind smsbind = MTO.ToSmsBindObject(message, "");
            DbAction.InsertSmsBind(smsbind);
            String SendMsg="ZLDJ#" +sms.getPhone() + "#" + sms.getIdNumber() + "#" + sms.getName() + "#" + sms.getAddress();
            Program.logWriter.WriteLine("发送短信：" + SendMsg);
            WaveComTools.Sms_Send("10086", SendMsg);
            Program.logWriter.WriteLine("发送短信完成");
        }
        public void CreateAlertProducer(string m_phonenumber, string m_content)
        {
            try
            {
                Property property;
                Property property2;
                this.m_mq.CreateProducer(false, "SERVER_INFO");
                List<Property> lstProperty = new List<Property>();
                property.name = "phoneNo";
                property.value = m_phonenumber;
                lstProperty.Add(property);
                property2.name = "content";
                property2.value = m_content;
                lstProperty.Add(property2);
                this.m_mq.SendMQMessage("", lstProperty);
                Program.logWriter.WriteLine("=============MQ队列写入完成 电话号码：=============");
            }
            catch (Exception exception)
            {
                Program.logWriter.WriteLine("CreateAlertProducer Exception:" + exception.Message);
            }
        }

    }
}
