using System;
using DynamicExecuteAssembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class AssemblyLauncherUnitTest
    {
        public AssemblyLauncher AssemblyLauncher { get; }
        public StubBizLogic StubBizLogic { get; }

        public AssemblyLauncherUnitTest()
        {
            AssemblyLauncher = new AssemblyLauncher();
            StubBizLogic = new StubBizLogic();
        }

        [TestMethod]
        public void TestReturnType()
        {
            var stubMethod = AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethod");
            Assert.IsInstanceOfType(stubMethod, typeof(IExecuteResult<string>));
        }

        [TestMethod]
        public void TestExecute()
        {
            var stubMethod = AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethod");
            Assert.AreEqual("Result", stubMethod.Message);

            var stubMethodWithParameter = AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWithParameter",
                "'parameter'");
            Assert.AreEqual("Result with parameter", stubMethodWithParameter.Message);
        }

        [TestMethod]
        public void TestTooManyParametersException()
        {
            try
            {
                AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWithTooManyParameter", "'parameter'");
                Assert.Fail("應該拋出一個例外");
            }
            catch (Exception e)
            {
                Assert.AreEqual("依照設計規範 UnitTestProject.StubBizLogic 的 StubMethodWithTooManyParameter 組件方法只能傳入一個參數", e.Message);
            }
        }

        [TestMethod]
        public void TestNotReturnTypeofIExecuteResultException()
        {
            try
            {
                AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethod2");
                Assert.Fail("應該拋出一個例外");
            }
            catch (Exception e)
            {
                Assert.AreEqual("依照設計規範 UnitTestProject.StubBizLogic 的 StubMethod2 組件方法必須回傳繼承自 IExecuteResult<T> 介面的物件，可將該組件方法的回傳型別改為 DefaultExecuteResult", e.Message);
            }

            try
            {
                AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWithParameter2", "'parameter'");
                Assert.Fail("應該拋出一個例外");
            }
            catch (Exception e)
            {
                Assert.AreEqual("依照設計規範 UnitTestProject.StubBizLogic 的 StubMethodWithParameter2 組件方法必須回傳繼承自 IExecuteResult<T> 介面的物件，可將該組件方法的回傳型別改為 DefaultExecuteResult", e.Message);
            }
        }

        [TestMethod]
        public void TestJsonException()
        {
            try
            {
                AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWithParameter", "{parameter: wrong}");
                Assert.Fail("應該拋出一個例外");
            }
            catch (Exception e)
            {
                Assert.AreEqual("JSON 參數反序列化錯誤，UnitTestProject.StubBizLogic 的 StubMethodWithParameter 組件方法所需參數型別應為: System.String", e.Message);
            }
        }
    }

    public class StubBizLogic
    {
        public IExecuteResult<string> StubMethod()
        {
            return new DefaultExecuteResult<string>() { Message = "Result" };
        }

        public IExecuteResult<string> StubMethodWithParameter(string parameter)
        {
            return new DefaultExecuteResult<string>() { Message = $"Result with {parameter}" };
        }

        public IExecuteResult<string> StubMethodWithTooManyParameter(string parameter1, string parameter2)
        {
            return new DefaultExecuteResult<string>() { Message = $"Result with {parameter1} and {parameter2}" };
        }

        public object StubMethod2()
        {
            return new { Message = "Result" };
        }

        public object StubMethodWithParameter2(string parameter)
        {
            return new { Message = $"Result with {parameter}" };
        }
    }
}
