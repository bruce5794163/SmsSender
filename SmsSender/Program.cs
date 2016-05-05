using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmsSender
{
    static class Program
    {
        public static bool watchMyMessage = false;
        public static readonly bool consoleWatch = false;
        public static FileWriter logWriter= new FileWriter();

        public static String machine = "";
        public static String mysql = "";
        public static String mq = "";
        public static String mquser = "";
        public static String mqpwd = "";
        public static int frequency = 1000;
        public static String comport = "";
        public static bool mqconnection=false;
        
        //public static ConnectionPool conn;//连接池对象
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            logWriter.WriteLine("=======程序开启======");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

    }
}
