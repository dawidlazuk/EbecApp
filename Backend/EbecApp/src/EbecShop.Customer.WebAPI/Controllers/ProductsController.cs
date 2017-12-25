using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EbecShop.Model;
using EbecShop.Customer.BizLogic.Contract;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EbecShop.Customer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ICustomerLogic customerLogic;

        public ProductsController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return customerLogic.GetProducts();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = customerLogic.GetProduct(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound($"Product with id {id} not found.");
        }
    }
}
