
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
namespace SmsSender
{
    public class ConnectionPoolABC
    {
        private Stack<MySqlConnection> pool;
        private const int POOL_MAX_SIZE = 200;
        private int current_Size = 0;                
        private static ConnectionPoolABC connPool;

        private ConnectionPoolABC()
        {
            if (pool == null)
            {
                pool = new Stack<MySqlConnection>();
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ConnectionPoolABC getInstance()
        {
            if (connPool == null)
            {
                connPool = new ConnectionPoolABC();
            }
            return connPool;
        }

        public MySqlConnection getConnection()
        {
            Program.logWriter.WriteLine("当前池大小："+current_Size.ToString());
            Program.logWriter.WriteLine("当前堆栈大小：" + pool.Count);
            MySqlConnection conn;
            
            lock (this)
            {
                if (pool.Count == 0)
                {
                    if (current_Size < POOL_MAX_SIZE)
                    {
                        conn = createConnection();
                        if (conn != null)
                        {
                            current_Size++;
                            pool.Push(conn);
                        }
                    }
                    else
                    {
                        //try
                        //{
                        //    Monitor.Wait(this);
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.WriteLine(e.Message);
                        //}
                    }

                }
                if (pool.Count != 0)
                {
                    conn = (MySqlConnection)pool.Pop();
                    if (conn.State != ConnectionState.Open)
                        conn = createConnection();
                    return conn;
                }
                else
                {
                    return null;
                }

            }
            
        }



        public void releaseConnection(MySqlConnection conn)
        {
            lock (this)
            {
                try
                {
                    pool.Push(conn);
                    //Monitor.Pulse(this);
                }
                catch
                {
                    Program.logWriter.WriteLine("释放连接错误");
                    pool.Clear();
                    current_Size = 0;
                }
            }
        }

        private MySqlConnection createConnection()
        {
            lock (this)
            {
                try
                {
                    MySqlConnection newConn = new MySqlConnection(Program.mysql);
                    newConn.Open();
                    return newConn;
                }
                catch
                {
                    Program.logWriter.WriteLine("网络异常，数据库无法连接。");
                    pool.Clear();
                    current_Size = 0;
                    return null;
                }
            }
        }
    }
}