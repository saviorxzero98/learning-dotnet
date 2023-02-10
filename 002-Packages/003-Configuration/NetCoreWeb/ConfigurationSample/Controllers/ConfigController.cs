using ConfigurationSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ConfigurationSample.Controllers
{
    [Route("api")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        protected IConfiguration _config;

        public ConfigController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/appsetting
        [HttpGet]
        [Route("appsetting")]
        public ActionResult<string> AppSetting()
        {
            // [方法一] 透過 appsetting.json 取出 Setting
            
            // 取法一
            MySetting mySetting = _config.GetSection("MySetting").Get<MySetting>();

            // 取法二
            string value = _config.GetSection("MySetting:Phone").Value;

            return $"appsetting.json : {value}";
        }

        // GET api/appsetting/connectionstring
        [HttpGet]
        [Route("appsetting/connectionstring")]
        public ActionResult<string> AppSettingConnectionString()
        {
            // [方法一] 透過 appsetting.json 取出 Setting
            string value = _config.GetConnectionString("Default");

            return $"appsetting.json : {value}";
        }


        /// <summary>
        /// GET api/webconfig/appsetting
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("webconfig/appsetting")]
        public ActionResult<string> WebConfigSetting()
        {
            // [方法二] 使用 ConfigurationManager 取出 app.config 的 AppSetting
            string value = ConfigurationManager.AppSettings.Get("MySetting");

            return $"app.config : {value}";
        }

        // GET api/webconfig/connectionstring
        [HttpGet]
        [Route("webconfig/connectionstring")]
        public ActionResult<string> WebConfigConnectionStrings()
        {
            // [方法二] 使用 ConfigurationManager 取出 app.config 的 Connection String
            string value = ConfigurationManager.ConnectionStrings["MY_DB"].ToString();

            return $"app.config : {value}";
        }
    }
}
