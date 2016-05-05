using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Configuration;
using System.Data;
namespace SmsSender
{
    class MTO
    {
        public static SmsBindTemp TosmsBindTempObject(IMessage message)
        {
            SmsBindTemp smsBindTemp = new SmsBindTemp();
            String sn = message.Properties.GetString("sn");
            String uuid = message.Properties.GetString("uuid");
            String name = message.Properties.GetString("name");
            String userMessageId = message.Properties.GetString("userMessageId");
            String phone = message.Properties.GetString("phone");
            String address = message.Properties.GetString("address");
            String idNumber = message.Properties.GetString("idNumber");
            String userId = message.Properties.GetString("userId");
            String hostName = message.Properties.GetString("hostName");
            smsBindTemp.setSn(sn);
            smsBindTemp.setUuid(uuid);
            smsBindTemp.setName(name);
            smsBindTemp.setUserId(userId);
            smsBindTemp.setUserMessageId(userMessageId);
            smsBindTemp.setPhone(phone);
            smsBindTemp.setAddress(address);
            smsBindTemp.setIdNumber(idNumber);
            smsBindTemp.setHostName(hostName);
            smsBindTemp.setCreateTime(DateTime.Now);
            return smsBindTemp;
        }
        public static List<SmsBindTemp> TosmsBindTempObject(DataSet ds)
        {
            List<SmsBindTemp> smsList = new List<SmsBindTemp>();
            if (ds.Tables.Count > 0)
            {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        SmsBindTemp obj = new SmsBindTemp();
                        obj.setSn(dr["sn"].ToString());
                        obj.setUuid(dr["uuid"].ToString());
                        obj.setName(dr["name"].ToString());
                        obj.setUserId(dr["userid"].ToString());
                        obj.setUserMessageId(dr["usermessageid"].ToString());
                        obj.setPhone(dr["phone"].ToString());
                        obj.setAddress(dr["address"].ToString());
                        obj.setIdNumber(dr["idnumber"].ToString());
                        obj.setHostName(dr["hostname"].ToString());
                        obj.setReceiveFlag(int.Parse(dr["receiveflag"].ToString()));
                        obj.setCounter(int.Parse(dr["counter"].ToString()));
                        obj.setCreateTime(DateTime.Parse(dr["createtime"].ToString()));
                        obj.setId(long.Parse(dr["id"].ToString()));
                        smsList.Add(obj);
                    }                    
            }
            return smsList;
        }
        public static SmsBind ToSmsBindObject(IMessage message, String state)
        {                       
                    String name = message.Properties.GetString("name");            
                    String phone = message.Properties.GetString("phone");
                    String address = message.Properties.GetString("address");
                    String idNumber = message.Properties.GetString("idNumber");                        
                    String msg = "ZLDJ#" + phone + "#" + idNumber + "#" + name + "#" + address;
                    SmsBind vo = new SmsBind();
                    vo.setContent(msg);
                    vo.setType(2);
                    vo.setSendphone("10086");
                    vo.setReceivephone("15852744042");
                    vo.setState(state);
                    vo.setCreatetime(DateTime.Now);
                    return vo;
              
        }

        public static SmsBind ToSmsBindObject(string msg, string state, int type)
        {
            SmsBind bind = new SmsBind();
            bind.setContent(msg);
            bind.setType(type);
            bind.setSendphone("10086");
            bind.setReceivephone("15852744042");
            bind.setState(state);
            bind.setCreatetime(DateTime.Now);
            return bind;
        }

        public static List<SimExtendInfo> toSimExtendObjectList(DataSet ds)
        {
            List<SimExtendInfo> list = new List<SimExtendInfo>();
            if (ds.Tables.Count > 0)
            {
                DataTable table = ds.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    SimExtendInfo item = new SimExtendInfo
                    {
                        Balance = row["balance"].ToString(),
                        Phone_number = row["phone_number"].ToString()
                    };
                    list.Add(item);
                }
            }
            return list;
        }
    }
}
