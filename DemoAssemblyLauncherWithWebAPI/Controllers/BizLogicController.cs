using DemoBizLogic;
using DynamicExecuteAssembly;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace DemoAssemblyLauncherWithWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BizLogicController : ControllerBase
    {
        public AssemblyLauncher AssemblyLauncher = new AssemblyLauncher();

        // GET api/BizLogic/$MethodList
        /// <summary>取得 BizLagic 組件的方法清單</summary>
        /// <remarks>參考 OData 的 prefix 方式，使用 $ 做為識別符</remarks>
        /// <returns></returns>
        [HttpGet("$MethodList")]
        public IActionResult GetMethodList()
        {
            var result = typeof(IBizLogicObjcet).GetMethods().Select(p => p.Name);
            return new JsonResult(result);
        }

        // GET api/BizLogic/GetString
        /// <summary>執行 BizLagic 組件的 GetString 方法</summary>
        /// <param name="method">組件方法</param>
        /// <returns></returns>
        [HttpGet("{method}")]
        public IActionResult Get(string method)
        {
            var result = AssemblyLauncher.Execute<BizLogicObjcet, object>(new BizLogicObjcet(), method);
            return new JsonResult(result);
        }

        //POST api/BizLogic/GetStringWithParamter
        /// <summary>執行 BizLagic 組件的 GetStringWithParamter 方法</summary>
        /// <remarks>執行帶參數的組建方法，建議使用 POST，避免破壞 URL 的一致性</remarks>
        /// <param name="method">組件方法</param>
        /// <param name="parameter">組件參數</param>
        /// <returns></returns>
        [HttpPost("{method}")]
        public IActionResult Post(string method, [FromBody] BizLagicModel parameter)
        {
            var result = AssemblyLauncher.Execute<BizLogicObjcet, object>(new BizLogicObjcet(), method, JsonSerializer.Serialize(parameter));
            return new JsonResult(result);
        }
    }
}
