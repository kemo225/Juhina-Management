using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.Return.InterFace;
using Juhyna_BLL.Sales.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Return.DTO;
using Juhyna_DAL.Sales.DtoSales;
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
    public class SalesController : ControllerBase
    {
        private readonly ISalesBLL _sale;
        private readonly IMemoryCache _Cashe;
        public string Cashkey ="JuhinaSales";
        public SalesController(ISalesBLL sale,IMemoryCache memoryCache)
          {
                _sale = sale;
                _Cashe = memoryCache;
        }
      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DTOsalesRead>> GetSales()
        {
            if (!_Cashe.TryGetValue(Cashkey, out List<DTOsalesRead> Sales))
            {
                Sales = _sale.GetSales();
                if (Sales == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Sales, casheoption);
            }
            return Ok(Sales);
        }

        [HttpGet("Sale/{ID}", Name = "GetSaleByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]

        public ActionResult<DtoAdminRead> GetSaleByID([FromRoute] int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Admin = _sale.GetSalesById(ID);
            if (Admin == null)
                return NotFound("Data Is Not Found");

            return Ok(Admin);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DTOSalesUpdate> AddSale([FromBody] DTOSalesAdd saleadd)
        {
            if (saleadd == null)
                return BadRequest("Data Invalid");
            var Admin = _sale.AddSale(saleadd);
            if (Admin == null)
                return NotFound("Data Is Not Found");
            else
                _Cashe.Remove(Cashkey); 
            return CreatedAtRoute(nameof(GetSaleByID), new { ID = Admin.ID }, Admin);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DTOSalesUpdate> UpdateSale([FromBody]DTOSalesUpdate saleUpdaet)
        {
            if (saleUpdaet == null)
                return BadRequest("Data Invalid");
            var sale = _sale.UpdateSale(saleUpdaet);
            if (sale == null)
                return NotFound("Data Is Not Found");
            else
                _Cashe.Remove(Cashkey);
            return Ok(sale);
        }
        [HttpDelete("ID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult DeleteSale([FromRoute] int ID)
        {
            if (ID <= 0)
                return BadRequest("Data Invalid");

            if (_sale.DeleteSale(ID))
                return Ok("Deleted SuccessFuly");
            else
                _Cashe.Remove(Cashkey);
            return NotFound("Error When Deleted");
        }
       
    }
}
