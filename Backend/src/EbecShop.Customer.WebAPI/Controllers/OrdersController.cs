using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EbecShop.Customer.BizLogic.Contract;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EbecShop.Customer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private ICustomerLogic customerLogic;

        public OrdersController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(customerLogic.GetTeamOrders(new Model.Team { Id = 0 })); //TODO Change to Team context;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(customerLogic.GetTeamOrders(new Model.Team { Id = 0 }).Where(order => order.Id == id).FirstOrDefault()); //TODO Change to Team context;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
