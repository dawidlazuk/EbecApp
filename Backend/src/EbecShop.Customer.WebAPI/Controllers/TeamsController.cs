using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EbecShop.Customer.BizLogic.Contract;
using EbecShop.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EbecShop.Customer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAngularAppOrigin")]
    public class TeamsController : Controller
    {
        private ICustomerLogic customerLogic;

        public TeamsController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            try
            {
                Team team = customerLogic.GetTeam(id);
                if (team != null)
                    return Ok(team);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
