using Microsoft.AspNetCore.Mvc;
using EbecShop.Customer.BizLogic.Contract;
using EbecShop.Model;
using EbecShop.Customer.WebAPI.DTO;
using System.Net;
using System;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EbecShop.Customer.WebAPI.Controllers
{
    [EnableCors("AllowAngularAppOrigin")]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private ICustomerLogic customerLogic;
        private Team teamContext;

        public OrdersController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
            teamContext = new Team();
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] int teamId)
        {
            return Ok(customerLogic.GetTeamOrders(new Model.Team { Id = teamId })); //TODO Change to Team context;
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    return Ok(customerLogic.GetTeamOrders(new Model.Team { Id = 0 }).Where(order => order.Id == id).FirstOrDefault()); //TODO Change to Team context;
        //}

        // POST api/values
        [EnableCors("AllowAngularAppOrigin")]
        [HttpPost]
        public IActionResult Post_MakeOrder([FromBody] NewOrderRequest order)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var createdOrder = customerLogic.CreateOrder(order.TeamId, order.Products);
                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
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
