using Microsoft.AspNetCore.Mvc;
using EbecShop.Customer.BizLogic.Contract;
using EbecShop.Model;
using System.Net;
using System;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using System.Collections.Generic;
using EbecShop.Shop.BizLogic.Contract;
using EbecShop.Model.Enums;
using EbecShop.WebAPI.Customer.DTO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EbecShop.WebAPI.Shop.Controllers
{
    [EnableCors("AllowAngularAppOrigin")]
    [Route("api/shop/[controller]")]
    public class OrdersController : Controller
    {
        private IShopLogic _shopLogic;

        //TODO remove
        private ICustomerLogic customerLogic;

        public OrdersController(ICustomerLogic customerLogic, IShopLogic shopLogic)
        {
            this.customerLogic = customerLogic;
            this._shopLogic = shopLogic;
        }

        // GET: api/orders
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> GetAllAsync()
        {
            try
            {
                var orders = await customerLogic.GetOrdersAsync();
                AddTeamDataToOrders(orders);

                return Ok(orders.Select(o => Order_DTO.MapFromModel(o)));
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
                        
            return Ok(orders.Select(order => Order_DTO.MapFromModel(order))); 
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
                return Ok(Order_DTO.MapFromModel(order));
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
                return Ok(OrderDetails.MapFromModel(order));
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
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //    throw new NotImplementedException();
        //}

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

        [HttpPut("{id}")]
        public IActionResult ChangeOrderStatus(int id, [FromBody] OrderStatusChangeRequest data)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than zero.");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            Order order;
            try
            {
                switch (data.Status)
                {
                    case OrderStatus.InProgress:
                        order = _shopLogic.SetOrderState_InProgress(id);
                        break;
                        
                    case OrderStatus.ReadyToReceive:
                        order = _shopLogic.SetOrderState_ReadyToReceive(id);
                        break;

                    case OrderStatus.Finished:
                        order = _shopLogic.SetOrderState_Finished(id);
                        break;                        

                    default:
                        throw new NotImplementedException($"Status {data.Status} is not supporrted");
                }

                return Ok(Order_DTO.MapFromModel(order));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
