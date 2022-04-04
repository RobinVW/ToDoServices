using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB4_ToDoServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB4_ToDoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {

        
        // GET: api/<ColumnController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ColumnController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ColumnController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ColumnController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ColumnController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
