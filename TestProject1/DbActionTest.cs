using SmsSender;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Apache.NMS;
using System.Collections.Generic;

namespace TestProject1
{
    
    
    /// <summary>
    ///这是 DbActionTest 的测试类，旨在
    ///包含所有 DbActionTest 单元测试
    ///</summary>
    [TestClass()]
    public class DbActionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///InsertSmsBind 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SmsSender.exe")]
        public void InsertSmsBindTest()
        {
            SmsBind_Accessor sms = null; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = DbAction_Accessor.InsertSmsBind(sms);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///InsertSmsBindTemp 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SmsSender.exe")]
        public void InsertSmsBindTempTest()
        {
            IMessage message = null; // TODO: 初始化为适当的值
            DbAction_Accessor.InsertSmsBindTemp(message);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///UpdateCount 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SmsSender.exe")]
        public void UpdateCountTest()
        {
            SmsBindTemp obj = null; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = DbAction_Accessor.UpdateCount(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///UpdateSmsBindTempReceiveFlag 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SmsSender.exe")]
        public void UpdateSmsBindTempReceiveFlagTest()
        {
            string flag = string.Empty; // TODO: 初始化为适当的值
            SmsBindTemp temp = null; // TODO: 初始化为适当的值
            DbAction_Accessor.UpdateSmsBindTempReceiveFlag(flag, temp);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///GetReSendList 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SmsSender.exe")]
        public void GetReSendListTest()
        {
            List<SmsBindTemp> expected = null; // TODO: 初始化为适当的值
            List<SmsBindTemp> actual;
            actual = DbAction_Accessor.GetReSendList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
