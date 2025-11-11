using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.Payment.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Catogrey.DTO;
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

    public class PaymentsController : ControllerBase
    {
        private readonly IpaymentBLL _PaymentBLL;
        public PaymentsController(IpaymentBLL PaymentBL)
        {
            _PaymentBLL = PaymentBL;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DtoPaymentRead>> GetPayments()
        {
            try
            {


                var Admins = _PaymentBLL.GetAllPaymentMethods();
                if (Admins == null || Admins.Count == 0)
                    return NotFound("Data Is Not Found");
                return Ok(Admins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("Payment/{ID}", Name = "GetPaymentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]

        public ActionResult<DtoPaymentRead> GetPaymentByID([FromRoute] int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Payment = _PaymentBLL.GetPaymentMethodbyID(ID);
            if (Payment == null)
                return NotFound("Data Is Not Found");

            return Ok(Payment);
        }

    }
}
