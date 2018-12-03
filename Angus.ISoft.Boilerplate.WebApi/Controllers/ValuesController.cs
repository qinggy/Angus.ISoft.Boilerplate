using Angus.ISoft.Boilerplate.Infrastructure.Log;
using Angus.ISoft.Boilerplate.Service;
using Angus.ISoft.Boilerplate.WebApi.ExtensionAttribute;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Angus.ISoft.Boilerplate.WebApi.Controllers
{
    [RoutePrefix("api/v1/values")]
    public class ValuesController : ApiController
    {
        private readonly IDemoService demoService;
        public ValuesController(IDemoService _demoService)
        {
            demoService = _demoService;
        }

        // GET api/values
        [HttpGet]
        [Route("list")]
        [JwtAuth]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Array))]
        public IEnumerable<string> Get()
        {
            LoggerHelper.Debug("Debug");
            LoggerHelper.Info("Info");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
