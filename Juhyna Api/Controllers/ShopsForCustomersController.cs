using Juhyna_BLL.CustomerPlace.InterFace;
using Juhyna_DAL.CustomerPlace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class ShopsForCustomersController : ControllerBase
    {
        private readonly ICustomerPlaceBLL _CustomerPlace;
        public ShopsForCustomersController(ICustomerPlaceBLL CustomerPlace)
        {
            _CustomerPlace = CustomerPlace;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]

        public ActionResult<IEnumerable<DTOCustomerPlaceRead>> GetShopsCustomers()
        {
            var Admins = _CustomerPlace.GetShopsCustomers();
            if (Admins == null || Admins.Count == 0)
                return NotFound("Data Is Not Found");
            return Ok(Admins);
        }
        [HttpGet("GetShopsCustomerBy/{ID}", Name = "GetShopsCustomerByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DTOCustomerPlaceRead> GetShopsCustomerByID([FromRoute] int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Admin = _CustomerPlace.GetShopsCustomerByID(ID);
            if (Admin == null)
                return NotFound("Data Is Not Found");

            return Ok(Admin);
        }
        [HttpGet("GetShopsbyCustomerID/{CustomerID}", Name = "GetShopsbyCustomerID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DTOCustomerPlaceRead> GetShopsbyCustomerID([FromRoute] int CustomerID)
        {
            if (CustomerID < 0)
                return BadRequest("ID Is Not Valid");

            var ShopCustomer = _CustomerPlace.GetShopsByCistomerID(CustomerID);
            if (ShopCustomer == null)
                return NotFound("Data Is Not Found");

            return Ok(ShopCustomer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles ="Admin")]

        public ActionResult<DTOCustomerPlacereAdd> AddPlaceToCustomer([FromBody] DTOCustomerPlaceAdd CustomerAdd)
        {
            if (CustomerAdd == null)
                return BadRequest("Data Invalid");
            var Customer = _CustomerPlace.AddPlaceToCustomer(CustomerAdd);
            if (Customer == null)
                return NotFound("Data Is Not Found");
            else
                return CreatedAtRoute("GetShopsCustomerByID", new { ID = Customer.ID }, Customer);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteShopsCustomerby([FromRoute] int ID,[FromRoute]string Email)
        {
            if (ID < 0)
                return BadRequest("Id Must be More than 0");
            else if (_CustomerPlace.DeleteShopsCustomerby(ID, Email))
                return Ok();
            else
                return NotFound($"ID : {ID} Not Exist.");
        }

    }
}
