using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.Catogrey.ICatogreyBLL;
using Juhyna_BLL.Products.InterFace;
using Juhyna_DAL.Admins.DTOAdmin;
using Juhyna_DAL.Models;
using Juhyna_DAL.Products.Dto;
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
    public class ProductsController : ControllerBase
    {
        private readonly IproductBLL _productBLL;
        private readonly IMemoryCache _Cashe;
        private readonly IcatogreyBLL _CatogreyBLL;
     

        public string CashKey = "JuhinaProducts";
        public ProductsController(IproductBLL productBLL, IMemoryCache cashe, IcatogreyBLL CatogreyBLL)
        {
            _productBLL = productBLL;
            _Cashe = cashe;
            _CatogreyBLL= CatogreyBLL;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DTOProducctRead>> GetAllProducts()
        {
            try
            {
                if (!_Cashe.TryGetValue(CashKey, out List<DTOProducctRead> ProductCashe))
                {
                    ProductCashe = _productBLL.GetAllProducts();

                    if (ProductCashe == null)
                        return NotFound("Data Is Not Found");

                    var casheoption = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    _Cashe.Set(CashKey, ProductCashe, casheoption);
                }
                return Ok(ProductCashe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Product/{ID}", Name = "GetProductsByID")]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DTOProducctRead> GetProductsByID([FromRoute] int ID)
        {
            if (ID < 0)
                return BadRequest("ID Is Not Valid");

            var Admin = _productBLL.GetProductsByID(ID);
            if (Admin == null)
                return NotFound("Data Is Not Found");

            return Ok(Admin);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]

        public ActionResult<DtoAdminUpdate> AddProduct([FromBody] DTOproductAdd ProductAdd)
        {
            if (ProductAdd == null)
                return BadRequest("Data Invalid");
            var category = _CatogreyBLL.GetCatogreybyID(ProductAdd.CategoryID);
            if (category == null)
                return NotFound("Category ID Is Not Found");
            var Product = _productBLL.AddProduct(ProductAdd);
            if (Product == null)
                return NotFound("Data Is Not Found");
            else
            {
                _Cashe.Remove(CashKey);
                return CreatedAtRoute("GetProductsByID", new { ID = Product.ID }, Product);
            }

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]

        public ActionResult<DtoAdminUpdate> UpdateProduct([FromBody] dtoproductUpdate ProductUpdate)
        {
            if (ProductUpdate == null)
                return BadRequest("Data Invalid");
            var product = _productBLL.UpdateProduct(ProductUpdate);
            if (product == null)
                return NotFound("Data Is Not Found");
            else
                _Cashe.Remove(CashKey);
            return Ok(product);
        }
     
    }
}
