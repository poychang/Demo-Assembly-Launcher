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
                Assert.Fail("���өߥX�@�Өҥ~");
            }
            catch (Exception e)
            {
                Assert.AreEqual("�̷ӳ]�p�W�d UnitTestProject.StubBizLogic �� StubMethodWithTooManyParameter �ե��k�u��ǤJ�@�ӰѼ�", e.Message);
            }
        }

        [TestMethod]
        public void TestNotReturnTypeofIExecuteResultException()
        {
            try
            {
                AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethod2");
                Assert.Fail("���өߥX�@�Өҥ~");
            }
            catch (Exception e)
            {
                Assert.AreEqual("�̷ӳ]�p�W�d UnitTestProject.StubBizLogic �� StubMethod2 �ե��k�����^���~�Ӧ� IExecuteResult<T> ����������A�i�N�Ӳե��k���^�ǫ��O�אּ DefaultExecuteResult", e.Message);
            }

            try
            {
                AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWithParameter2", "'parameter'");
                Assert.Fail("���өߥX�@�Өҥ~");
            }
            catch (Exception e)
            {
                Assert.AreEqual("�̷ӳ]�p�W�d UnitTestProject.StubBizLogic �� StubMethodWithParameter2 �ե��k�����^���~�Ӧ� IExecuteResult<T> ����������A�i�N�Ӳե��k���^�ǫ��O�אּ DefaultExecuteResult", e.Message);
            }
        }

        [TestMethod]
        public void TestJsonException()
        {
            try
            {
                AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWithParameter", "{parameter: wrong}");
                Assert.Fail("���өߥX�@�Өҥ~");
            }
            catch (Exception e)
            {
                Assert.AreEqual("JSON �ѼƤϧǦC�ƿ��~�AUnitTestProject.StubBizLogic �� StubMethodWithParameter �ե��k�һݰѼƫ��O����: System.String", e.Message);
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
