using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.Inevntory.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Customer.DTO;
using Juhyna_DAL.Inventory.Dto;
using Juhyna_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryBLL _InventoryBLL;
        public string CashKey = "JuhinaInventory";
        private readonly IMemoryCache _Cashe;
        public InventoriesController(IInventoryBLL IInventoryBLL, IMemoryCache Cashe)
        {
            _InventoryBLL = IInventoryBLL;
            _Cashe = Cashe;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]

        public ActionResult<IEnumerable<DtoInventoryRead>> GetAllInventories()
        {
            try
            {
                if (!_Cashe.TryGetValue(CashKey, out List<DtoInventoryRead> CustomersCashe))
                {
                    CustomersCashe = _InventoryBLL.GetAllInventories();
                    if (CustomersCashe == null )
                        return NotFound("Data Is Not Found");
                    var casheoption = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    _Cashe.Set(CashKey, CustomersCashe, casheoption);
                }



                return Ok(CustomersCashe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    


        [HttpGet("Inventory/{ID}", Name = "GetInventoryByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoAdminRead> GetInventoryByID([FromRoute]int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Inventory = _InventoryBLL.GetInventoryByID(ID);
            if (Inventory == null)
                return NotFound("Data Is Not Found");

            return Ok(Inventory);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult AddInventory([FromBody] DtoInventoryAdd InventoryAdd)
        {
            if (InventoryAdd == null)
                return BadRequest("Data Invalid");
            var Inventory = _InventoryBLL.AddInventory(InventoryAdd);
            if (Inventory <= 0)
                return NotFound("Failed Add Inventory");
            
                return CreatedAtRoute("GetInventoryByID", new { ID = Inventory }, Inventory);
        }

        [HttpDelete("ID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult DeleteInventory([FromRoute]int ID)
        {
            if (ID <= 0)
                return BadRequest("Data Invalid");

            if (_InventoryBLL.DeleteInventory(ID))
                return Ok("Deleted SuccessFuly");
            
                return NotFound("Error When Deleted");
        }


    }


}
