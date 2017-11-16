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
        public void TestExecute()
        {
            var stubMethod = AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethod", "");
            Assert.IsInstanceOfType(stubMethod, typeof(IExecuteResult<string>));
            Assert.AreEqual("From StubMethod", stubMethod.Message);

            var stubMethodWithParameter = AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWithParameter",
                "'Sub Parameter'");
            Assert.IsInstanceOfType(stubMethodWithParameter, typeof(IExecuteResult<string>));
            Assert.AreEqual("From StubMethod with parameter: Sub Parameter", stubMethodWithParameter.Message);
        }

        public void TestExecuteWithMoreParameter()
        {
            var stubMethodWithParameter = AssemblyLauncher.Execute<StubBizLogic, string>(StubBizLogic, "StubMethodWith2Parameter",
                "'Sub Parameter'");
            Assert.IsInstanceOfType(stubMethodWithParameter, typeof(IExecuteResult<string>));
            Assert.AreEqual("From StubMethod with parameter: Sub Parameter", stubMethodWithParameter.Message);
        }
    }

    public class StubBizLogic
    {
        public IExecuteResult<string> StubMethod()
        {
            return new DefaultExecuteResult<string>() { Message = "From StubMethod" };
        }

        public IExecuteResult<string> StubMethodWithParameter(string parameter)
        {
            return new DefaultExecuteResult<string>() { Message = $"From StubMethod with parameter: {parameter}" };
        }

        public IExecuteResult<string> StubMethodWith2Parameter(string parameter1, string parameter2)
        {
            return new DefaultExecuteResult<string>();
        }
    }
}
