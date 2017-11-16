using DemoBizLogic;
using DynamicExecuteAssembly;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoAssemblyLauncherWithWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BizLagicController : Controller
    {
        public AssemblyLauncher AssemblyLauncher = new AssemblyLauncher();

        // GET api/BizLagic/GetString
        [HttpGet("{method}")]
        public IActionResult Get(string method)
        {
            var result = AssemblyLauncher.Execute<BizLagicObjcet, object>(new BizLagicObjcet(), method, "");
            return new JsonResult(result);
        }

        //POST api/BizLagic/GetStringWithParamter
        [HttpPost("{method}")]
        public IActionResult Post(string method, [FromBody]BizLagicModel parameter)
        {
            var result = AssemblyLauncher.Execute<BizLagicObjcet, object>(new BizLagicObjcet(), method, JsonConvert.SerializeObject(parameter));
            return new JsonResult(result);
        }
    }
}
