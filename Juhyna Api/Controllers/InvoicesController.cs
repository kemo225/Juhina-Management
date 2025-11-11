using Juhyna_BLL.EmailService;
using Juhyna_BLL.FollowingSaleAdminstrative.Interface;
using Juhyna_BLL.ProductIneventory.InterFace;
using Juhyna_BLL.Sales.InterFace;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.Inventory.Dto;
using Juhyna_DAL.InvoiceSaleAdminstrative.DTO;
using Juhyna_DAL.Models;
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
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesBLL _folloingSaleAdminstrativeBLL;
        private readonly IMemoryCache _Cashe;
        private readonly IproductInevtoryBLL _Productinventory;
        private readonly ISalesBLL _SaleBLL;
        

        public string Cashkey = "";
        public InvoicesController(ISalesBLL salesBLL,IproductInevtoryBLL Productinventory, IInvoicesBLL folloingSaleAdminstrativeBLL, IMemoryCache Cashe)
        {
            _folloingSaleAdminstrativeBLL=folloingSaleAdminstrativeBLL;
            _Cashe = Cashe;
            _Productinventory= Productinventory;
            _SaleBLL= salesBLL;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]//Response Cashe
        [OutputCache(Duration = 60)]//OutPut Cashe

        public ActionResult<IEnumerable<DTOFollowingSaleAdminstrativeRead>>GetAllInvoiceBetweenSaleAdminstrative()
        {
            Cashkey = "AllInvoiceBetweenSaleAdminstrative";
            if (!_Cashe.TryGetValue(Cashkey, out List<DTOFollowingSaleAdminstrativeRead> InvoiceCasce))
            {
                InvoiceCasce = _folloingSaleAdminstrativeBLL.GetAllInvoiceBetweenSaleAdminstrative();
                if (InvoiceCasce == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, InvoiceCasce, casheoption);
            }

      
            return Ok(InvoiceCasce);
        }


        [HttpGet("Today")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]


        public ActionResult<IEnumerable<DTOFollowingSaleAdminstrativeRead>> GetAllInvoiceBetweenSaleAdminstrativeToday()
        {
            Cashkey = "AllInvoiceBetweenSaleAdminstrativeToday";

            if (!_Cashe.TryGetValue(Cashkey, out List<DTOFollowingSaleAdminstrativeRead> InvoiceCasce))
            {
                InvoiceCasce = _folloingSaleAdminstrativeBLL.GetAllInvoiceBetweenSaleAdminstrativeToday();
                if (InvoiceCasce == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, InvoiceCasce, casheoption);
            }


            return Ok(InvoiceCasce);
        }
        [HttpGet("AdminstrativeID/{AdminstrativeID}")]
        [Authorize(Roles = "Admin,Adminstrative")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<IEnumerable<DTOFollowingSaleAdminstrativeRead>> GetInvoiceBetweenAdminstrativeSalesbyAdminstrativeID([FromRoute]int AdminstrativeID)
        {
          
              var  InvoiceCasce = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenAdminstrativeSalesbyAdminstrativeID(AdminstrativeID);
                if (InvoiceCasce == null)
                    return NotFound("Data Is Not Found");
            


            return Ok(InvoiceCasce);
        }
        [HttpGet("today/SaleID/{SaleID}")]
        [Authorize(Roles = "Admin,Sale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<IEnumerable<DTOFollowingSaleAdminstrativeRead>> GetInvoiceBetweenSaleAdminstrtivesbySaleIDToday([FromRoute] int SaleID)
        {
            if(SaleID<=0)
                return BadRequest("SaleID Not Valid");
         
            var    InvoiceCasce = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrtivesbySaleIDToday(SaleID);
                if (InvoiceCasce == null|| InvoiceCasce.Count==0)
                    return NotFound("Data Is Not Found");
                
        

            return Ok(InvoiceCasce);
        }

        [HttpGet("SaleID/{SaleID}")]
        [Authorize(Roles = "Admin,Sale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<IEnumerable<DTOFollowingSaleAdminstrativeRead>> GetInvoiceBetweenSaleAdminstrtivesbySaleID([FromRoute] int SaleID)
        {
           if(SaleID<=0)
                return BadRequest("SaleID Not Valid");
            var InvoiceCasce = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrtivesbySaleID(SaleID);
                if (InvoiceCasce == null)
                    return NotFound("Data Is Not Found");

            


            return Ok(InvoiceCasce);
        }


        [HttpGet("At/{date}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt([FromRoute] string date)
        {
            if (string.IsNullOrEmpty(date))
                return BadRequest("Date Not Valid");

            var InvoiceCasce = _folloingSaleAdminstrativeBLL.GetAllInvoiceBetweenSaleAdminstrativeAt(date);
                if (InvoiceCasce == null)
                    return NotFound("Data Is Not Found");
          
          


            return Ok(InvoiceCasce);
        }


        [HttpGet("ID/{ID}",Name ="GetInvoiceByID")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoFollowingSaleAdminstrarive> GetInvoiceBetweenSaleAdminstrativeByID([FromRoute] int ID)
        {
            if(ID <= 0)
                return BadRequest("ID Not Valid");

            var InvoiceCasce = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrativeByID(ID);
                if (InvoiceCasce == null)
                    return NotFound("Data Is Not Found");
           

            return Ok(InvoiceCasce);
        }


        [HttpGet("At/{date}/{productID}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]


        public ActionResult<DTOFollowingSaleAdminstrativeRead> GetAllInvoiceBetweenSaleAdminstrativeAt([FromRoute] string date, [FromRoute] int productID)
        {
           if(productID<=0)
                return BadRequest("ProductID Not Valid");
           if(string.IsNullOrEmpty( date))
                return BadRequest("Date Not Valid");
            var  InvoiceCasce = _folloingSaleAdminstrativeBLL.GetAllInvoiceBetweenSaleAdminstrativeAt(date,productID);
                if (InvoiceCasce == null)
                    return NotFound("Data Is Not Found");
        
            return Ok(InvoiceCasce);
        }

        //[HttpPut("BySale")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult UpdateInvoiceBySale(DtoFollowingSaleAdminstrativeupdateforSale dto)
        //{
        //    if (dto == null )
        //        return BadRequest("Data Is Not Valid");

        //    var invoice = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrativeDetailsByID(dto.Id);
        //    if (invoice == null)
        //        return NotFound("Data Is Not Found");
        //    else if (invoice.IsConfrmed == true)
        //        return BadRequest("You cannot Update The Invoice");


        //    if (_folloingSaleAdminstrativeBLL.UpdateInvoiceBySale(dto))
        //        return Ok();
        //    else
        //        return BadRequest("Failed In Update Data");

        //}

        [HttpPut("ByAdminstrative")]
        [Authorize(Roles = "Adminstrative")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateInvoiceByAdminstrative([FromBody]DtoFollowingSaleAdminstrativeUpdateforAdminstrative dto)
        {
            if (dto == null || dto.ID < 0)
                return BadRequest("Data Is Not Valid");

            var invoice = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrativeDetailsByID(dto.ID);
            if (invoice == null)
                return NotFound("Data Is Not Found");
            else if (invoice.IsConfrmed == true)
                return BadRequest("You cannot Update The Invoice");


            if (_folloingSaleAdminstrativeBLL.UpdateInvoiceByAdminstrative(dto))
            {
                if (Cashkey != "")
                    _Cashe.Remove(Cashkey);
                return Ok();
            }
            else
                return BadRequest("Failed In Update Data");

        }

        //[HttpDelete]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult DeleteInvoiceByID(int Id,int AdminstrativeID)
        //{
        //    if (Id < 0)
        //        return BadRequest("Data Is Not Valid");


        //    var invoice = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrativeDetailsByID(Id);
        //    if (invoice == null)
        //        return NotFound("Data Is Not Found");


        //    if (_folloingSaleAdminstrativeBLL.DeleteInvoicebyID(Id, AdminstrativeID))
        //        return Ok("Deleted Successfuly");
        //    else
        //        return BadRequest("Failed In Delete Data");

        //}


        [HttpPost("ByAdminstrative")]
        [Authorize(Roles = "Adminstrative")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<DtoFollowingSaleAdminstrativeAddforadminstrativeReturned> AddInvoice([FromBody]DtoFollowingSaleAdminstrativeAddforadminstrative dto)
        {
            if (dto == null)
                return BadRequest("Data Is Not Valid");
            dto.CreatedByAdminstrativeID= int.Parse(User.FindFirst("ID").Value);
            var productininventory = _Productinventory.GetAllProductsInInventoryByID(dto.ProductinInventoryID);
            if (productininventory == null)
                return NotFound("Product In Inventory Not Found");
            if (productininventory.InventoryQuantity < dto.Quantity)
                return BadRequest("Quantity In Inventory Not Sufficient");
            var sale = _SaleBLL.GetSalesById(dto.SaleID);
            if (sale == null)
                return NotFound("Sale Not Found");
            var InvoiceAdded = _folloingSaleAdminstrativeBLL.AddInvoiceByAdminstative(dto);
            if (null== InvoiceAdded)
                return BadRequest("Failed Add Invoice");
            try { 
            EmailService.SendEmail(
    sale.Email,
    "New Invoice Created",
    $"Dear {sale.FirstName} {sale.LastName},\n\n" +
    "An invoice has been created for you by the administrative staff.\n\n" +
    "Invoice Details:\n" +
    "----------------------------\n" +
    $"Invoice ID: {InvoiceAdded.ID}\n" +
    $"Product Name: {InvoiceAdded.ProductName}\n" +
    $"Product Price: {InvoiceAdded.productprice} EGP\n" +
    $"Quantity: {InvoiceAdded.Quantity}\n" +
    "Inventory Information:\n" +
    "----------------------------\n" +
    $"Inventory Name: {InvoiceAdded.InventoryName}\n" +
    $"Inventory Address: {InvoiceAdded.InventoryAddress}\n\n" +
    "Administrative Info:\n" +
    "----------------------------\n" +
    $"Created By: {InvoiceAdded.CreatedByAdminstrativeName}\n" +
    $"Created At: {InvoiceAdded.CreateAt:yyyy-MM-dd HH:mm}\n\n" +
    "Thank you for your efforts!\n" +
    "Best regards,\n" +
    "Sales Management System"
);
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
            }
            if(Cashkey!="") 
            _Cashe.Remove(Cashkey);


                return Ok(InvoiceAdded);
            
            
        }


        [HttpPost("BySale")]
        [Authorize(Roles = "Sale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoFollowingSaleAdminstrativeAddforadminstrative> AddInvoice([FromBody]DtoFollowingSaleAdminstrativeAddforSale dto)
        {
            if (dto == null)
                return BadRequest("Data Is Not Valid");
            if(dto.ID < 0)
                return BadRequest("Data Is Not Valid");
            var sale = _SaleBLL.GetSalesById(int.Parse(User.FindFirst("ID").Value));
            if (sale == null)
                return NotFound("Sale Not Found");
            var invoice=_folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrativeByID(dto.ID);
            if (invoice == null)
                return NotFound("Invoice Not Found");
            if(sale.ID!=invoice.SaleID)
                return BadRequest("You Are Not Authorized To Confirm This Invoice");
            var invoiceDetails = _folloingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrativeDetailsByID(dto.ID);   


            if (!_folloingSaleAdminstrativeBLL.ConfirmBySale(dto))
            {
                return NotFound("Data Is Not Found");
            }
            try
            {
                EmailService.SendEmail(
invoiceDetails.EmailAdminstrative,
"Invoice Accepted",
$"Dear {invoiceDetails.CreatedByAdminstrativeName},\n\n" +
"An invoice has been created for you by the administrative staff.\n\n" +
"Invoice Details:\n" +
"----------------------------\n" +
$"Invoice ID: {invoice.Id}\n" +
$"Product Name: {invoiceDetails.ProductName}\n" +
$"Quantity : {invoiceDetails.Quantity}"+
$"Created At: {invoice.CreateAt:yyyy-MM-dd HH:mm}\n\n" +
"Thank you for your efforts!\n" +
"Best regards,\n" +
"Sales Management System"
);
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
            }
            if (Cashkey != "")
                _Cashe.Remove(Cashkey);
            return Ok("Confirmed Successfuly");

        }

    }
}
