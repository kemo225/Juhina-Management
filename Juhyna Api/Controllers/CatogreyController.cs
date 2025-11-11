using Juhyna_BLL.Catogrey.ICatogreyBLL;
using Juhyna_BLL.Payment.InterFace;
using Juhyna_DAL.PaymentMethod.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class CatogreyController : ControllerBase
    {
    
            private readonly IcatogreyBLL _CatogreyBll;
            public CatogreyController(IcatogreyBLL Catogrey)
            {
                _CatogreyBll = Catogrey;
            }
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DtoPaymentRead>> GetAllCatogrey()
            {
                try
                {


                    var Catogries = _CatogreyBll.GetAllCatogrey();
                    if (Catogries == null || Catogries.Count == 0)
                        return NotFound("Data Is Not Found");
                    return Ok(Catogries);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            [HttpGet("Catogrey/{ID}", Name = "GetCatogreybyID")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]

        public ActionResult<DtoPaymentRead> GetCatogreybyID([FromRoute]int ID)
            {
                if (ID < 0)
                    return BadRequest("ID Is Not Valid");

                var Catogrey = _CatogreyBll.GetCatogreybyID(ID);
                if (Catogrey == null)
                    return NotFound("Data Is Not Found");

                return Ok(Catogrey);
            }

        }
    
}
