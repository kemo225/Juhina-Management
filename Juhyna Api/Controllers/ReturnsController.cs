using Juhyna_BLL.AdminStrative.InterFace;
using Juhyna_BLL.FollowingSaleAdminstrative.Interface;
using Juhyna_BLL.Return.InterFace;
using Juhyna_BLL.Validation;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.Models;
using Juhyna_DAL.Return.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class ReturnsController : ControllerBase
    {
        private readonly IReturnBLL _returnBLL;
        private readonly IInvoicesBLL _followingSaleAdminstrativeBLL;
        private readonly ValidationBLL _validationBLL;
        private readonly IMemoryCache _Cashe;
        private readonly IAdminstrativeBLL _Adminstrative;
        public string Cashkey ="";
        public ReturnsController(IAdminstrativeBLL Adminstrative, IMemoryCache memorey,IReturnBLL returnBLL,IInvoicesBLL followingSaleAdminstrativeBLL, ValidationBLL validationBLL)
        {
            _returnBLL = returnBLL;
            _followingSaleAdminstrativeBLL = followingSaleAdminstrativeBLL;
            _validationBLL = validationBLL;
            _Cashe = memorey;
            _Adminstrative = Adminstrative;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]

        public ActionResult<IEnumerable<DtoReturnRead>> GetAllReturns()
        {
            Cashkey = "AllReturns";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoReturnRead> Returns))
            {
                Returns = _returnBLL.GetAllReturns();
                if (Returns == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Returns, casheoption);
            }
            return Ok(Returns);
        }
        [HttpGet("Today/{SaleID}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]

        public ActionResult<IEnumerable<DtoReturnRead>> GetAllReturnsTodayBySaleID(int SaleID)
        {
            Cashkey = "AllReturns";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoReturnRead> Returns))
            {
                Returns = _returnBLL.GetAllReturnsTodayBySaleID(SaleID);
                if (Returns == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Returns, casheoption);
            }
            return Ok(Returns);
        }
        [HttpGet("{SaleID}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]

        public ActionResult<IEnumerable<DtoReturnRead>> GetAllReturnsBySaleID(int SaleID)
        {
            Cashkey= "AllReturns";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoReturnRead> Returns))
            {
                Returns = _returnBLL.GetAllReturnsBySaleID(SaleID);
                if (Returns == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Returns, casheoption);
            }
            return Ok(Returns);
        }



        [HttpGet("ConfirmedReturn")]
        [Authorize(Roles = "Admin")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]

        public ActionResult<IEnumerable<DtoReturnRead>> GetAllConfirmedReturns()
        {
            Cashkey= "AllConfirmedReturns";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoReturnRead> Returns))
            {
                Returns = _returnBLL.GetAllConfirmedReturns();
                if (Returns == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Returns, casheoption);
            }
            return Ok(Returns);
        }


        [HttpGet("UnConfirmedReturn")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]

        public ActionResult<IEnumerable<DtoReturnRead>> GetAllUnConfirmedReturns()
        {
            Cashkey= "AllUnConfirmedReturns";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoReturnRead> Returns))
            {
                Returns = _returnBLL.GetAllUnConfirmedReturns();
                if (Returns == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Returns, casheoption);
            }
            return Ok(Returns);
        }


        [HttpGet("ID/{ReturnID}", Name = "GetReturnById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoReturnRead> GetReturnById(int ReturnID)
        {
            if(ReturnID <= 0)
                return BadRequest("Data Is Not Valid");
            var Return = _returnBLL.GetReturnById(ReturnID);
            if (Return == null )
                return NotFound("Data Is Not Found");
            return Ok(Return);
        }


        [HttpPost("ByAdminstative")]
        [Authorize(Roles = "Adminstrative")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoReturnRead> IsEvaluateReturnByAdminstrative([FromBody] DtoReturnAddByAdminstrative Dto)
        {
            if (Dto == null)
                return BadRequest("Data Is Not Valid"); 
            var Return = _returnBLL.IsEvaluateReturn(Dto);


            if (Return == false)
                return NotFound("Data Is Not Found");

            if (Cashkey != "")
                _Cashe.Remove(Cashkey);
            
            return Ok("SuccessFuly,The Return Is Evaluated");
        }


        //[HttpDelete]
        //[Authorize(Roles = "Admin")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]

        //public ActionResult DeleteReturnByID(int Id)
        //{
        //    if (Id <= 0)
        //        return BadRequest("Data Is Not Valid");


        //    var invoice = _returnBLL.GetReturnById(Id);
        //    if (invoice == null)
        //        return NotFound("Data Is Not Found");


        //    if (!_returnBLL.DeleteReturnById(Id))           
        //        return BadRequest("Failed In Delete Data");

        //    if (Cashkey != "")
        //        _Cashe.Remove(Cashkey);
        //    return Ok("The Return Is Deleted Successfuly");


        //}


        [HttpPost("BySale")]
        [Authorize(Roles = "Sale")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<DtoReturnAddBySaleReturned> AddReturnBySale([FromBody] DtoReturnAddBySale dto)
        {
            if (dto == null)
                return BadRequest("Data Is Not Valid");
            var SaleID = int.Parse(User.FindFirst("ID")!.Value);
            dto.CraetedBySaleId = SaleID;
            var Invoice=_followingSaleAdminstrativeBLL.GetInvoiceBetweenSaleAdminstrativeByID(dto.InvoiceID);

            if (Invoice == null)
                return BadRequest("The Invoice Is Not Found");

            if (Invoice.SaleID != dto.CraetedBySaleId)
                return BadRequest($"You Cannot Create Return For This Invoice,is Confirmed By That ID : {Invoice.SaleID}");

            if (!_validationBLL.IsCandoReturn(dto.Back,Invoice.Rest))
                return BadRequest($"Error You Cannot Do Return The Invoice inside {Invoice.Rest} and You Want Back {dto.Back}");

            var NewReturn = _returnBLL.AddReturn(dto);
            if (null==NewReturn)
                return BadRequest("Failed Add Return");


            if (Cashkey != "")
                _Cashe.Remove(Cashkey);
            return Ok(NewReturn);
            
            

        }



     
        [HttpPut("BySale")]
        [Authorize(Roles = "Sale")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult UpdateReturnBySale([FromBody]DtoReturnUpdateBySale dto)
        {
            var CreatedbySaleID=int.Parse(User.FindFirst("ID")!.Value);
            dto.CraetedBySaleId = CreatedbySaleID;
            if (dto == null || dto.ID < 0)
                return BadRequest("Data Is Not Valid");

            var Return = _returnBLL.GetReturnById(dto.ID);
            var Adminstrative = _Adminstrative.GetAdminStrativeReadBYID(dto.AdminstrativeId);

            if (Return == null)
                return NotFound("Data Is Not Found");
             if(Return.IsConfirmed==true)
                return BadRequest("Is Already Confirmed by Adminstrative so Cannot Updated.");
             if(Adminstrative == null)
                return NotFound("The Adminstrative Is Not Found");


            if (!_returnBLL.UpdateReturnBySale(dto))
                return BadRequest("Failed In Update Data");

            if (Cashkey != "")
                _Cashe.Remove(Cashkey);
            return Ok("Data Is Updated SuccessFuly");

        }


    }
}
