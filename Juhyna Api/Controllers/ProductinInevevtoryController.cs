using Juhyna_BLL.Order.InterFace;
using Juhyna_BLL.ProductIneventory.InterFace;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.ManageProducts.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.Orders.DTO;
using Juhyna_DAL.ProductInventory.DTO;
using Juhyna_DAL.Visits.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin,Adminstrative")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class ProductinInevevtoryController : ControllerBase
    {
        private readonly IproductInevtoryBLL _ProductInventoryBLL;
        private readonly IMemoryCache _Cashe;
        public string Cashkey = "JuhinaProductInventory";
        public ProductinInevevtoryController(IproductInevtoryBLL productInventoryBLL,IMemoryCache memory)
        {
            _ProductInventoryBLL = productInventoryBLL;
            _Cashe = memory;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DtoProductInventoryRead>> GetAllProductinInventories()
        {
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoProductInventoryRead> ProductInventories))
            {
                ProductInventories = _ProductInventoryBLL.GetAllProductinInventories();
                if (ProductInventories == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, ProductInventories, casheoption);
            }


            return Ok(ProductInventories);
        }

        [HttpGet("{ID}",Name ="GetAllProductsInInventoryByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]

        public ActionResult<DtoProductInventoryRead> GetAllProductsInInventoryByID([FromRoute] int ID)
        {
            if (ID <= 0)
            {
                return BadRequest("Invalid ID.");
            }
            var ProductInventory = _ProductInventoryBLL.GetAllProductsInInventoryByID(ID);
            if (ProductInventory == null)
            {
                return NotFound("Data Is Not Found.");
            }
          
                return Ok(ProductInventory);
            
        }
        [HttpGet("ByInventory/{InventoryID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        

        public ActionResult<IEnumerable<DtoProductInventoryRead>> GetAllProductsInInventory([FromRoute] int InventoryID)
        {
         if(InventoryID<=0)
                return BadRequest("Invalid ID.");
            var  ProductInventories = _ProductInventoryBLL.GetAllProductsInInventory(InventoryID);
                if (ProductInventories == null)
                    return NotFound("Data Is Not Found");
              
            
                return Ok(ProductInventories);
            
        }
        [HttpGet("ByProduct/{ProductID}/Inventory/{InventoryID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<IEnumerable<DtoProductInventoryRead>> GetProductInfoInInventory([FromRoute] int ProductID, [FromRoute] int InventoryID)
        {
            if(ProductID <= 0 )
                return BadRequest("Invalid Product ID.");
            if (InventoryID<=0)
                return BadRequest("Invalid Product ID.");


          var  ProductInventories = _ProductInventoryBLL.GetProductInfoInInventory(ProductID,InventoryID);
                if (ProductInventories == null)
                    return NotFound("Data Is Not Found");
           
            return Ok(ProductInventories);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoProductInventoryAddReturned> AddProductToInventory([FromBody] DtoProductInventoryAdd obj)
        {
            if (obj == null)
            {
                return BadRequest("Invalid data.");
            }
            var productinventory = _ProductInventoryBLL.AddProductToInventory(obj);
            if (productinventory!=null)
            {
                if (Cashkey != "")
                    _Cashe.Remove(Cashkey);
                return Ok(productinventory);
            }
            else
            {
                return BadRequest("Failed to add product to inventory.");
            }
        }
    }
}
