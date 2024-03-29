﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EbecShop.Model;
using EbecShop.Customer.BizLogic.Contract;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EbecShop.WebAPI.Customer.Controllers
{
    [EnableCors("AllowAngularAppOrigin")]
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
