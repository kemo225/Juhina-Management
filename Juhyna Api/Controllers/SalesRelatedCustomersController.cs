using Juhyna_BLL.SaleCustomer.InterFace;
using Juhyna_DAL.SaleCustomer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Juhyna_Api.Controllers
{
    [Route("Sales/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class SalesRelatedCustomersController : ControllerBase
    {
        private readonly ISaleCustomerBLL _saleCustomerBLL;
        public SalesRelatedCustomersController(ISaleCustomerBLL saleCustomerBLL)
        {
            _saleCustomerBLL = saleCustomerBLL;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DtoSaleCustomerRead>> GetSaleRelatedCustomer()
        {
            var SaleRelatedCustomer=_saleCustomerBLL.GetSaleRelatedCustomer();
            if (SaleRelatedCustomer == null || SaleRelatedCustomer.Count == 0)
            {
                return NotFound("Data Is Not Found");
            }
        
                return Ok(SaleRelatedCustomer);
            
        }
        [HttpGet("GetCustomersRelatedBy/{SaleID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "SaleID" })]

        public ActionResult<IEnumerable<DtoSaleCustomerRead>> GetCustomersRelatedBySaleID([FromRoute] int SaleID)
        {
            var SaleRelatedCustomer = _saleCustomerBLL.GetCustomerRelatedBySaleID(SaleID);
            if (SaleRelatedCustomer == null || SaleRelatedCustomer.Count == 0)
            {
                return NotFound("Data Is Not Found");
            }
            
                return Ok(SaleRelatedCustomer);
            
        }
        [HttpGet("GetSalesRelatedBy/{CustomerID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "CustomerID" })]

        public ActionResult<IEnumerable<DtoSaleCustomerRead>> GetSalesRelatedByCustomerID([FromRoute] int CustomerID)
        {
            var SaleRelatedCustomer = _saleCustomerBLL.GetSalesRelatedByCustomerID(CustomerID);
            if (SaleRelatedCustomer == null || SaleRelatedCustomer.Count == 0)
            {
                return NotFound("Data Is Not Found");
            }
          
                return Ok(SaleRelatedCustomer);
            
        }
        //[HttpGet("CustomerName/{FirstName}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<IEnumerable<DtoSaleCustomerRead>> GetSalesRelatedByCustomerName( string FirstName)
        //{
        //    var SaleRelatedCustomer = _saleCustomerBLL.GetSalesRelatedByCustomerName(FirstName);
        //    if (SaleRelatedCustomer == null || SaleRelatedCustomer.Count == 0)
        //    {
        //        return NotFound("Data Is Not Found");
        //    }
        //    else
        //    {
        //        return Ok(SaleRelatedCustomer);
        //    }
        //}
        //[HttpGet("SaleName/{FirstName}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<IEnumerable<DtoSaleCustomerRead>> GetCustomerSRelatedBySaleName(string FirstName)
        //{
        //    var SaleRelatedCustomer = _saleCustomerBLL.GetCustomerSRelatedBySaleName(FirstName);
        //    if (SaleRelatedCustomer == null || SaleRelatedCustomer.Count == 0)
        //    {
        //        return NotFound("Data Is Not Found");
        //    }
        //    else
        //    {
        //        return Ok(SaleRelatedCustomer);
        //    }
        //}
        //[HttpDelete("/{ID}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult DeleteSaleCustomerByID(int ID)
        //{
        //    if (ID < 0)
        //        return BadRequest("Data Is InCorrect");
        //    if (_saleCustomerBLL.DeleteSaleCustomer(ID))
        //        return Ok("Data Is Deleted Successfuly");
        //    else
        //        return NotFound("Data Is Not Found");
        //}
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DTOSaleCustomerUpdate> UpdateSaleCustomer([FromBody] DTOSaleCustomerUpdate saleCustomerUpdate)
        {
            if (saleCustomerUpdate == null)
                return BadRequest("Data Not Correct");
            var sale=_saleCustomerBLL.UpdateSaleCustomer(saleCustomerUpdate);
            if (sale == null)
                return NotFound("Data is not found to update it");
            
                return Ok(sale);
        }
        [HttpGet("GetSaleCustomer/{ID}",Name = "GetSaleCustomerByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoSaleCustomerRead> GetSaleCustomerByID([FromRoute] int ID)
        {
            if (ID <= 0)
                return BadRequest("Data InValid");
            var saleCustomer=_saleCustomerBLL.GetSaleCustomerByID(ID);
            if(saleCustomer == null)
                return NotFound("Not Found Data");
            
                return Ok(saleCustomer);    
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DTOSaleCustomerUpdate>AddSaleCustomer([FromBody] DTOSaleCustomerAdd saleAdd)
        {
            var saleCustomerAdd=_saleCustomerBLL.AddSaleCustomer(saleAdd);
            if (saleCustomerAdd == null)
                return BadRequest("Faild In Add SaleCustomer");
             return Ok(saleCustomerAdd);
        }
    }
}
