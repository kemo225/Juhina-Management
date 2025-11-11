using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.Customers.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Customer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin,Sale")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class CustomersController : ControllerBase
    {
        public string CashKey = "JuhinaCustomer";
        private readonly IMemoryCache _Cashe;
        private readonly ICustomerBLL _Customer;
        public CustomersController(ICustomerBLL Customer,IMemoryCache Cashe)
        {
            _Customer = Customer;
            _Cashe = Cashe;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DTOCustmerRead>> GetCustomers()
        {
            if(!_Cashe.TryGetValue(CashKey, out List<DTOCustmerRead> CustomersCashe))
            {
                CustomersCashe = _Customer.GetCustomers();
                if (CustomersCashe == null || CustomersCashe.Count == 0)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(CashKey, CustomersCashe, casheoption);
            }
     
            return Ok(CustomersCashe);
        }
        [HttpGet("Customer/{ID}", Name = "GetCustomerBYID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]

        public ActionResult<DTOCustmerRead> GetCustomerBYID([FromRoute]int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Customer = _Customer.GetCustomerBYID(ID);
            if (Customer == null)
                return NotFound("Data Is Not Found");

            return Ok(Customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoCustmerUpdate> AddCustomer([FromBody]DTOCustmerAdd CustomerAdd)
        {
            if (CustomerAdd == null)
                return BadRequest("Data Invalid");
            var Customer = _Customer.AddCustomer(CustomerAdd);
            if (Customer == null)
                return NotFound("Data Is Not Found");
            else
                _Cashe.Remove(CashKey);
            return CreatedAtRoute("GetAdminByID", new { ID = Customer.ID }, Customer);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoCustmerUpdate> UpdateCustomer(DtoCustmerUpdate Customerupdate)
        {
            if (Customerupdate == null)
                return BadRequest("Data Invalid");
            var Customer = _Customer.UpdateCustomer(Customerupdate);
            if (Customer == null)
                return NotFound("Data Is Not Found");
            else
                _Cashe.Remove(CashKey);
            return Ok(Customer);
        }
        [HttpDelete]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult DeleteCustomer(int ID)
        {
            if (ID <= 0)
                return BadRequest("Data Invalid");

            if (_Customer.DeleteCustomer(ID))
            {
                _Cashe.Remove(CashKey);
                return Ok("Deleted Successfuly");
            }
            else
                return NotFound("Error When Deleted");
        }

    }

}
