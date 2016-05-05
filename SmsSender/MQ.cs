using System;
using System.Collections.Generic;
using System.Text;

using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace SmsSender
{
    public struct Property
    {
        public string name;
        public string value;
    }

    class MQ
    {
        
        
        public IConnection connection;
        private ISession session;
        private IMessageProducer producer;

       
        public void Start()
        {
           
                connection = MQPool.getInstance().getConnection();
                session = MQPool.getInstance().getSession();
        }

        public void Close()
        {
            if (session != null)
            {
                session.Close();
            }
            if (connection != null)
            {
                connection.Stop();
                connection.Close();
            }
        }

        public void CreateProducer(bool blnTopic, string strTopicName)
        {
            try
            {
                if (blnTopic)
                {

                    producer = MQPool.getInstance().getSession().CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(strTopicName));
                }
                else
                {
                    producer = MQPool.getInstance().getSession().CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(strTopicName));
                }
            }
            catch (Exception ex)
            {
                Program.logWriter.WriteLine("CreateProducer" + "出现异常：" + ex.Message.ToString());
                Program.mqconnection = false;
            }
        }



        public IMessageConsumer CreateConsumer(bool blnTopic, string strTopicName)
        {
            try
            {
                if (blnTopic)
                {
                    return MQPool.getInstance().getSession().CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(strTopicName));
                }
                else
                {
                    return MQPool.getInstance().getSession().CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(strTopicName));
                }
            }
            catch(Exception ex)
            {
                Program.logWriter.WriteLine("CreateConsumer"+"出现异常："+ex.Message.ToString());
                return  null;
            }
        }

        public IMessageConsumer CreateConsumer(bool blnTopic, string strTopicName, string strSelector)
        {
            if (strSelector == "")
            {
                GlobalFunction.MsgBox("MQ selector不能为空");
                return null;
            }

            if (blnTopic)
            {
                return MQPool.getInstance().getSession().CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(strTopicName), strSelector, false);
            }
            else
            {
                return MQPool.getInstance().getSession().CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(strTopicName), strSelector, false);
            }
        }

        public void SendMQMessage(string strText)
        {
            if (producer == null)
                return;
            ITextMessage msg = producer.CreateTextMessage();
            msg.Text = strText;
            producer.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
        }

        public void SendMQMessage(string strText, List<Property> lstProperty)
        {
            try
            {
                if (producer == null)
                    return;
                ITextMessage msg = producer.CreateTextMessage();
                msg.Text = strText;

                foreach (Property prop in lstProperty)
                {
                    msg.Properties.SetString(prop.name, prop.value);
                }
                producer.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
            }
            catch (System.Exception ex)
            {
                Program.logWriter.WriteLine("MQ发送消息失败：" + ex.Message.ToString());
            }
        }
    }
}
