using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using EbecShop.WebAPI.Auth.Models;
using EbecShop.WebAPI.Auth.DbContext;
using EbecShop.Shop.BizLogic.Contract;
using EbecShop.WebAPI.Auth.Helpers;

namespace EbecShop.WebAPI.Auth.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        //TODO to config
        private static decimal InitialTeamBalance => 1000;

        private UserManager<IdentityUser> _userManager;
        private AuthDbContext _authDbContext;
        private IShopLogic _shopLogic;
        private IJwtFactory _jwtFactory;
        private JwtIssuerOptions _jwtOptions;

        public AccountsController(
            UserManager<IdentityUser> userManager,
            AuthDbContext authDbContext,
            IShopLogic shopLogic,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _authDbContext = authDbContext;
            _shopLogic = shopLogic;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest credentials)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.Login, credentials.Password);
            if (identity == null) {
                ModelState.AddModelError("login_failure", "Invalid credentials.");
                return BadRequest(ModelState);
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.Login, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
                return await Task.FromResult<ClaimsIdentity>(null);

            if(await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        } 

        [HttpPost]
        [Route("register/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegistrationRequest model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            IdentityUser userIdentity = new IdentityUser() { UserName = model.User.Login };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (result.Succeeded == false)
                return new BadRequestObjectResult(result.Errors);

            var team = _shopLogic.CreateNewTeam(model.User.TeamName, InitialTeamBalance);

            if (team.Id == default(int))
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error in registering team");

            var customerUser = model.User;
            customerUser.IdentityId = userIdentity.Id;
            customerUser.TeamId = team.Id;

            await _authDbContext.Customers.AddAsync(customerUser);
            await _authDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

        [HttpPost]
        [Route("register/salesman")]
        public async Task<IActionResult> RegisterSalesman([FromBody] SalesmanRegistrationUser model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            IdentityUser userIdentity = new IdentityUser() { UserName = model.User.Login };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (result.Succeeded == false)
                return new BadRequestObjectResult(result.Errors);

            var salesmanUser = model.User;
            salesmanUser.IdentityId = userIdentity.Id;

            await _authDbContext.Salesmen.AddAsync(salesmanUser);
            await _authDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}