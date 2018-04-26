using Microsoft.AspNetCore.Mvc;
using EbecShop.Customer.BizLogic.Contract;
using EbecShop.Model;
using System.Net;
using System;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using System.Collections.Generic;
using EbecShop.WebAPI.Customer.DTO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EbecShop.WebAPI.Customer.Controllers
{
    [EnableCors("AllowAngularAppOrigin")]
    [Route("api/customer/[controller]")]
    public class OrdersController : Controller
    {
        private ICustomerLogic customerLogic;
        private Model.Team teamContext;

        public OrdersController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
            teamContext = new Model.Team();
        }

        // GET: api/orders
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> GetAllAsync()
        {
            try
            {
                var orders = await customerLogic.GetOrdersAsync();
                AddTeamDataToOrders(orders);

                return Ok(orders.Select(o => DTO.Order_DTO.MapFromModel(o)));
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        private void AddTeamDataToOrders(IEnumerable<Model.Order> orders)
        {
            if (orders == null)
                throw new ArgumentNullException(nameof(orders));

            var teamCache = new Dictionary<int, Team>();
            foreach (var order in orders)
            {
                Team team;
                if (teamCache.TryGetValue(order.TeamId, out team) == false)
                {
                    team = customerLogic.GetTeam(order.TeamId);
                    teamCache.Add(team.Id, team);
                }
                order.Team = team;
            }
        }

        [HttpGet]
        [Route("byTeam")]
        public IActionResult Get([FromQuery] int teamId)
        {
            if (teamId <= 0)
                return BadRequest("Id must be greater than zero.");

            var orders = customerLogic.GetTeamOrders(new Model.Team { Id = teamId }).Result; //TODO Change to Team context;

            if (orders.Any() == false)
                return NotFound();

            AddTeamDataToOrders(orders);
                        
            return Ok(orders.Select(order => DTO.Order_DTO.MapFromModel(order))); 
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get_Order(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            var order = customerLogic.GetOrder(id); //TODO Change to Team context;

            if (order == null)
                return NotFound();

            try
            {
                return Ok(DTO.Order_DTO.MapFromModel(order));
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

// GET: api/orders/{id}/details
[HttpGet]        
        [Route("{id}/details")]
        public IActionResult Get_OrderDetails(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            var order = customerLogic.GetOrder(id); //TODO Change to Team context;

            if (order == null)
                return NotFound();
            try
            {
                return Ok(DTO.OrderDetails.MapFromModel(order));
            }
            catch (Exception ex)
            {
#if DEBUG
                throw;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
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
#if DEBUG
                throw;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            try
            {
                var cancelledOrder = await customerLogic.CancelOrder(id);
                if (cancelledOrder.Status == Model.Enums.OrderStatus.Cancelled)
                    return Ok(cancelledOrder);
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch(Exception ex)
            {
#if DEBUG
                throw;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
