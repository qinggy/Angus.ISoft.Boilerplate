﻿using Angus.ISoft.Boilerplate.Service;
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
        public IEnumerable<string> Get()
        {
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
