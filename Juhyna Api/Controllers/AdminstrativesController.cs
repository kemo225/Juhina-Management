using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.AdminStrative.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Adminstrative.DTO;
using Juhyna_DAL.DTOPublic;
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class AdminstrativesController : ControllerBase
    {
        private readonly IAdminstrativeBLL _AdminStrative;
        private readonly IMemoryCache _Cashe;
        public string Cashkey = "JuhinaAdminstrative";
        public AdminstrativesController(IAdminstrativeBLL AdminStrative,IMemoryCache Cashe)
        {
            _AdminStrative = AdminStrative;
            _Cashe = Cashe;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DTOAdminstrativeRead>> GetAdminStratives()
        {

            if (!_Cashe.TryGetValue(Cashkey, out List<DTOAdminstrativeRead> AdminsCashe))
            {
                AdminsCashe = _AdminStrative.GetAdminStratives();
                if (AdminsCashe == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                _Cashe.Set(Cashkey, AdminsCashe, casheoption);
            }
            return Ok(AdminsCashe);
        }
        [HttpGet("GetAdminStrative/{ID}", Name = "GetAdminStrativeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]

        public ActionResult<DTOAdminstrativeRead> GetAdminStrativeReadBYID([FromRoute]int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Adminstrative = _AdminStrative.GetAdminStrativeReadBYID(ID);
            if (Adminstrative == null)
                return NotFound("Data Is Not Found");

            return Ok(Adminstrative);
        }
        [HttpGet("GetallAdminStrativesinInventoryID/{InventoryID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "InventoryID" })]

        public ActionResult<DTOAdminstrativeRead> GetallAdminStrativesinInventoryID([FromRoute]int InventoryID)
        {
            if (InventoryID < 0)
                return BadRequest("ID Is Not Valid");

            var Admin = _AdminStrative.GetallAdminStrativesinInventoryID(InventoryID);
            if (Admin == null)
                return NotFound("Data Is Not Found");

            return Ok(Admin);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoAdminStrativeUpdate> AddAdminStrative([FromBody]DTOAdminStrativeAdd adminadd)
        {
            if (adminadd == null)
                return BadRequest("Data Invalid");
            var Admin = _AdminStrative.AddAdminStrative(adminadd);
            if (Admin == null)
                return NotFound("Data Is Not Found");
            else
                _Cashe.Remove(Cashkey);
            return CreatedAtRoute("GetAdminStrativeByID", new { ID = Admin.ID }, Admin);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoAdminStrativeUpdate> UpdateAdminStrative([FromBody]DtoAdminStrativeUpdate AdminstrativeUpdate)
        {
            if (AdminstrativeUpdate == null)
                return BadRequest("Data Invalid");
            var Adminstrative = _AdminStrative.UpdateAdminStrative(AdminstrativeUpdate);
            if (Adminstrative == null)
                return NotFound("Data Is Not Found");
            else
                _Cashe.Remove(Cashkey);
            return Ok(Adminstrative);
        }
        [HttpDelete("{ID}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult DeleteAdminStrative([FromRoute]int ID)
        {
            if (ID <= 0)
                return BadRequest("Data Invalid");

            if (_AdminStrative.DeleteAdminStrative(ID))
            {
                  _Cashe.Remove(Cashkey);
                return Ok("Deleted Successfuly");
            }
            else
                return NotFound("Error When Deleted");
        }


    }
}
