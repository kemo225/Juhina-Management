using Juhyna_BLL.Admins.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
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
    [Authorize]//
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class AdminsController : ControllerBase
    {
        private readonly IAdminBLL _Admin;
        private readonly IMemoryCache _Cashe;
        public string CashKey = "JuhinaAdmin";
        public AdminsController(IAdminBLL Admin,IMemoryCache cashe)
        {
        _Admin = Admin;
        _Cashe = cashe;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DtoAdminRead>>GetAdmins()
        {
            try
            {
                if (!_Cashe.TryGetValue(CashKey, out List<DtoAdminRead> AdminsCashe))
                {
                    AdminsCashe = _Admin.GetAdmins();

                    if (AdminsCashe == null)
                        return NotFound("Data Is Not Found");

                    var casheoption = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    _Cashe.Set(CashKey, AdminsCashe, casheoption);
                }
                return Ok(AdminsCashe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Admin/{ID}",Name = "GetAdminByID")]
        [OutputCache(Duration = 60,VaryByRouteValueNames = new[] { "ID" })]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoAdminRead> GetAdminByID([FromRoute]int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Admin = _Admin.GetAdminReadBYID(ID);
            if (Admin == null)
                return NotFound("Data Is Not Found");

            return Ok(Admin);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoAdminUpdate> AddAdmin([FromBody]DtoAdminAdd adminadd)
        {
            if (adminadd == null)
                return BadRequest("Data Invalid");
            var Admin = _Admin.AddAdmin(adminadd);
            if (Admin == null)
                return NotFound("Data Is Not Found");
            else
            { 
                _Cashe.Remove(CashKey);
                return CreatedAtRoute("GetAdminByID", new { ID = Admin.ID }, Admin); 
            }

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoAdminUpdate> UpdateAdmin([FromBody]DtoAdminUpdate adminupdate)
        {
            if (adminupdate == null)
                return BadRequest("Data Invalid");
            var Admin = _Admin.UpdateAdmin(adminupdate);
            if (Admin == null)
                return NotFound("Data Is Not Found");
            else
                 _Cashe.Remove(CashKey);
            return Ok( Admin);
        }
        [HttpDelete]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult DeleteAdmin(int ID)
        {
            if (ID <= 0)
                return BadRequest("Data Invalid");

            if (_Admin.DeleteAdmin(ID))
            {
                _Cashe.Remove(CashKey);
                return Ok("Deleted Successfully");
            }

            else
                return NotFound("Error When Deleted");
        }


    }
}
