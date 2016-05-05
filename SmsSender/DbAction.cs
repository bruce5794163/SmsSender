using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Data;

namespace SmsSender
{
    class DbAction
    {
        
         
        public static void InsertSmsBindTemp(IMessage message)
        {
            try
            {
                SmsBindTemp smsBindTemp = MTO.TosmsBindTempObject(message);
                String sql = "insert into sms_bind_temp(uuid, sn,name, userId, phone, address, idNumber,receiveFlag,counter,userMessageId,hostName,createTime) " +
                              "values('" +
                              smsBindTemp.getUuid() + "','" +
                              smsBindTemp.getSn() + "','" +
                              smsBindTemp.getName() + "','" +
                              smsBindTemp.getUserId() + "','" +
                              smsBindTemp.getPhone() + "','" +
                              smsBindTemp.getAddress() + "','" +
                              smsBindTemp.getIdNumber() + "'," +
                              smsBindTemp.getReceiveFlag() + "," +
                              smsBindTemp.getCounter() + ",'" +
                              smsBindTemp.getUserMessageId() + "','" +
                              smsBindTemp.getHostName() + "','" +
                              smsBindTemp.getCreateTime().ToString() +
                              "')";                
                if (MySqlDB.ExcCommand(sql) == false)
                    Program.logWriter.WriteLine("InsertSmsBindTemp sql执行失败");

            }
            catch (System.Exception e)
            {
                Program.logWriter.WriteLine("InsertSmsBindTemp Exception:" + e.Message);
            }
        }
        public static void UpdateSmsBindTempReceiveFlag(String flag,SmsBindTemp temp)
        {
            String sql = "update sms_bind_temp set receiveFlag=" + flag + " where id=" + temp.getId();
            if (MySqlDB.ExcCommand(sql)==false)
                Program.logWriter.WriteLine("UpdateSmsBindTempReceiveFlag sql执行失败");
                
        }

        public static Boolean UpdateCount(SmsBindTemp obj)
        {
            String sql = "update sms_bind_temp set counter=" + obj.getCounter() + " where id=" + obj.getId();

            if (MySqlDB.ExcCommand(sql))
                return true;
            else
            {
                Program.logWriter.WriteLine("UpdateCount sql执行失败");
                return false;
            }
        }
        public static List<SmsBindTemp> GetReSendList()
        {
            DataSet ds= new DataSet();
                        
                String sql = "select * from sms_bind_temp where receiveFlag=0";
                ds = MySqlDB.QueryCommand(sql);
                if (ds == null)
                    return null;
                return MTO.TosmsBindTempObject(ds);
            
            
        }
        public static Boolean InsertSmsBind(SmsBind sms)
        {
            
            String sql = "insert into sms_bind(uuid, bindphone, sn, content, type, sendphone,receivephone,createtime) values(";
            sql += "'" + sms.getUuid() + "',";
            sql += "'" + sms.getBindphone() + "',";
            sql += "'" + sms.getSn() + "',";
            sql += "'" + sms.getContent() + "',";
            sql += "" + sms.getType() + ",";
            sql += "'" + sms.getSendphone() + "',";
            sql += "'" + sms.getReceivephone() + "',";
            sql += "'" + sms.getCreatetime().ToString() + "')";            
            if (MySqlDB.ExcCommand(sql))
            {
                return true;
            }
            else
            {
                Program.logWriter.WriteLine("InsertSmsBind sql执行失败");
                return false;
            }
        }
        public static List<SimExtendInfo> getExtendSimInfo()
        {
            string sQLString = "select b.balance,d.mobile as phone_number from t_ue_sim a,t_ue_sim_extend b,t_ue_basic_info c,t_user_basic_info d where   b.balance <= 5 and a.extend_id=b.id and c.owner_id=d.id and a.id=c.sim_id  and  b.first_activate_time<=now()  and a.id in (select sim_id from t_ue_basic_info where owner_id is not null)";
            DataSet ds = MySqlDB.QueryCommand(sQLString);
            if (ds == null)
                return null;
            return MTO.toSimExtendObjectList(ds);
        }
    }
}
