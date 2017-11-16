﻿using DemoBizLogic;
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
        /// <summary>執行 BizLagic 組件的 GetString 方法</summary>
        /// <param name="method">組件方法</param>
        /// <returns></returns>
        [HttpGet("{method}")]
        public IActionResult Get(string method)
        {
            var result = AssemblyLauncher.Execute<BizLagicObjcet, object>(new BizLagicObjcet(), method, "");
            return new JsonResult(result);
        }

        //POST api/BizLagic/GetStringWithParamter
        /// <summary>執行 BizLagic 組件的 GetStringWithParamter 方法</summary>
        /// <remarks>執行帶參數的組建方法，建議使用 POST，避免破壞 URL 的一致性</remarks>
        /// <param name="method">組件方法</param>
        /// <param name="parameter">組件參數</param>
        /// <returns></returns>
        [HttpPost("{method}")]
        public IActionResult Post(string method, [FromBody]BizLagicModel parameter)
        {
            var result = AssemblyLauncher.Execute<BizLagicObjcet, object>(new BizLagicObjcet(), method, JsonConvert.SerializeObject(parameter));
            return new JsonResult(result);
        }
    }
}