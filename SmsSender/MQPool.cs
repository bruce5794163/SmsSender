
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.IO;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Runtime.CompilerServices;
namespace SmsSender
{
    public class MQPool
    {
        private Stack<IConnection> mqpool;
        private const int POOL_MAX_SIZE = 20;
        private int current_Size = 0;

        private IConnectionFactory factory;
        public IConnection connection;

        
        private static MQPool mqconnPool;

        private MQPool()
        {
            factory = new ConnectionFactory(Program.mq);
            if (mqpool == null)
            {
                mqpool = new Stack<IConnection>();
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static MQPool getInstance()
        {
            if (mqconnPool == null)
            {
                mqconnPool = new MQPool();
            }
            return mqconnPool;
        }

        public ISession getSession()
        {
            try
            {
                return this.getConnection().CreateSession();
            }
            catch 
            {
                return null;
            }
        }
        public void closeSession(ISession session)
        {
            if (session != null)
            {
                session.Close();
            }
        }
        public IConnection getConnection()
        {
            IConnection conn;
            lock (this)
            {
                if (mqpool.Count == 0)
                {
                    if (current_Size < POOL_MAX_SIZE)
                    {
                        conn = createConnection();
                        conn.Start();
                        current_Size++;                        
                        mqpool.Push(conn);
                    }
                    else
                    {
                        try
                        {
                            Monitor.Wait(this);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                conn = (IConnection)mqpool.Pop();
                if (conn.IsStarted == false)
                {
                    conn = createConnection();
                    conn.Start();
                }
            }
            return conn;
        }

        public void releaseConnection(IConnection conn)
        {
            lock (this)
            {
                mqpool.Push(conn);
                Monitor.Pulse(this);
            }
        }

        private IConnection createConnection()
        {
            lock (this)
            {
                try
                {
                    IConnection newConn = connection = factory.CreateConnection(Program.mquser, Program.mqpwd);
                    newConn.Start();
                    return newConn;
                }
                catch
                {
                    Program.logWriter.WriteLine("网络异常，MQ无法连接。");
                    return null;
                }
            }
        }
    }
}