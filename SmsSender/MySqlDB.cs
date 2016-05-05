using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SmsSender
{
    internal class MySqlDB
    {


        private static MySqlConnection conn;
        
        public MySqlDB()
        {                        
                
        }

        public static bool ExcCommand(string command)
        {
            bool result;
            
            conn = ConnectionPoolABC.getInstance().getConnection();
            Program.logWriter.WriteLine("ExcCommand=====获取数据库连接！");
            if (conn == null)
            {
                return false;
            }
                MySqlCommand mySqlCommand = new MySqlCommand();
                Program.logWriter.WriteLine("sql:" + command + ";");
                try
                {
                    mySqlCommand.Connection = conn;  
                    mySqlCommand.CommandText = command;
                    if (mySqlCommand.ExecuteNonQuery() >0)
                    {                       
                        result = true;
                    }
                    else
                    {                       
                        result = false;
                    }
                    
                }
                catch (Exception ex)
                {                    
                    Program.logWriter.WriteLine("ExcCommand Exception:" + ex.Message);
                    result = false;
                }
            ConnectionPoolABC.getInstance().releaseConnection(conn);
            Program.logWriter.WriteLine("ExcCommand=====释放数据库连接！");
            return result;
        }
        public static DataSet QueryCommand(string SQLString)
        {

            MySqlCommand mySqlCommand = new MySqlCommand();
            mySqlCommand.CommandText = SQLString;
            Program.logWriter.WriteLine("sql:" + SQLString + ";");
            
            conn = ConnectionPoolABC.getInstance().getConnection();
            Program.logWriter.WriteLine("获取连接conn");
            if (conn == null)
                return null;
            DataSet dataSet = new DataSet();
            mySqlCommand.Connection = conn;  
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            Program.logWriter.WriteLine("设置连接参数");
                
            try
            {
                mySqlDataAdapter.Fill(dataSet, "ds");
                Program.logWriter.WriteLine("填充ds");
            }
            catch (MySqlException ex)
            {                    
                Program.logWriter.WriteLine("QueryCommand Exception:" + ex.ErrorCode);
                return null;
            }                
            ConnectionPoolABC.getInstance().releaseConnection(conn);
            Program.logWriter.WriteLine("释放连接");
            return dataSet;
            
        }
    }
}
