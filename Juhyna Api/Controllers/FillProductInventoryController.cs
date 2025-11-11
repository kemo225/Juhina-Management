using Juhyna_BLL.ManageProductInventory.InterFace;
using Juhyna_BLL.ProductIneventory.InterFace;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.DTO;
using Juhyna_DAL.ManageProducts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]


    public class FillProductInventoryController : ControllerBase
    {
        private readonly IManageProductInevntoryBLL _ProductInventoryBLL;
        public FillProductInventoryController(IManageProductInevntoryBLL productInventoryBLL)
        {
            _ProductInventoryBLL = productInventoryBLL;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Adminstrative,Admin")]

        public ActionResult<IEnumerable<DtoProductInventoryRead>> GetAllManageProducts()
        {
            var ProductInventory = _ProductInventoryBLL.GetAllManageProductsByAdminstrative();
            if (ProductInventory == null)
            {
                return NotFound("Data Is Not Found.");
            }
            else
            {
                return Ok(ProductInventory);
            }
        }
        [HttpPost("FillProductinInventory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Adminstrative")]

        public ActionResult FillProductinInventory([FromBody] DtoFillProductInventory dto)
        {
            dto.CreatedByAdminstrativeID = int.Parse(User.FindFirst("ID")!.Value);
            var IsFill = _ProductInventoryBLL.FillProductinInventory(dto);
            if (!IsFill)
            {
                return BadRequest("Wrong in Fill The Product");
            }
          
                return Ok("The product is Fill Successfuly");
            
        }
        //[HttpPost("ExchangeProductinInventory")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult ExchangeProductinInventory(DtoExcahngeProductInventory dto)
        //{
        //    var IsExchange = _ProductInventoryBLL.ExchangeProductinInventory(dto);
        //    if (!IsExchange)
        //    {
        //        return BadRequest("Wrong in Exchange The Product");
        //    }
        //    else
        //    {
        //        return Ok("The product is Exchange Successfuly");
        //    }
        //}
    }
}
