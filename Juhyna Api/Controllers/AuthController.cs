using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.AdminStrative.InterFace;
using Juhyna_BLL.Sales.InterFace;
using Juhyna_DAL.DTOPublic;
using Juhyna_DAL.Services.DTO_Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Juhyna_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAdminBLL _AdminBLL;
        private readonly IAdminstrativeBLL _AdminstrativeBL;
        private readonly ISalesBLL _SalesBLL;
        public AuthController(IAdminBLL adminBLL, IAdminstrativeBLL adminstrativeBL, ISalesBLL salesBLL)
        {
            _AdminBLL = adminBLL;
            _AdminstrativeBL = adminstrativeBL;
            _SalesBLL = salesBLL;
        }   

        [HttpPost("/LoginAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoToken> LoginAdmin([FromBody]DtoLogin admin)
        {
            if (admin == null)
                return BadRequest("Data Is Not Correct");

            var Admin = _AdminBLL.Login(admin);
            if (Admin == null)
                return NotFound("Error in UserName Or Password");

            return Ok(Admin);
          
        }



        [HttpPost("/LoginAdminstrative")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoToken> LoginAdminstrative([FromBody]DtoLogin adminstrative)
        {
            if (adminstrative == null)
                return BadRequest("Data Is Not Correct");

            var Admin = _AdminstrativeBL.Login(adminstrative);
            if (Admin == null)
                return NotFound("Error in UserName Or Password");

            return Ok(Admin);

        }


        [HttpPost("/LoginSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoToken> LoginSale(DtoLogin Sale)
        {
            if (Sale == null)
                return BadRequest("Data Is Not Correct");

            var Admin = _SalesBLL.Login(Sale);
            if (Sale == null)
                return NotFound("Error in UserName Or Password");

            return Ok(Admin);

        }
        [HttpPost("/LogoutAdmin/{AdminID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoToken> LogoutAdmin(string RefreshToken,[FromRoute] int AdminID)
        {
            if (RefreshToken == null)
                return BadRequest("Data Is Not Correct");
            if(AdminID<=0)
                return BadRequest("AdminID Is Not Correct");

            if (!_AdminBLL.Logout(RefreshToken,AdminID))
                return NotFound("Error in Logout");

            return Ok();

        }
        [HttpPost("/LogoutAdminstrative/{AdminstraiveID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoToken> LogoutAdminstrative( string RefreshToken, [FromRoute] int AdminstraiveID)
        {
            if (RefreshToken == null)
                return BadRequest("Data Is Not Correct");
            if (AdminstraiveID <= 0)
                return BadRequest("AdminID Is Not Correct");

            if (!_AdminstrativeBL.Logout(RefreshToken, AdminstraiveID))
                return NotFound("Error in Logout");

            return Ok();

        }
        [HttpPost("/LogoutSale/{SaleID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DtoToken> LogoutSale(string RefreshToken, [FromRoute] int SaleID)
        {
            if (RefreshToken == null)
                return BadRequest("Data Is Not Correct");
            if (SaleID <= 0)
                return BadRequest("AdminID Is Not Correct");

            if (!_SalesBLL.Logout(RefreshToken, SaleID))
                return NotFound("Error in Logout");

            return Ok();

        }




        [HttpPut("/Sale/ChangePassword")]
        [Authorize]
        [Authorize(Roles ="Sale,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangePasswordForSale([FromBody] DToChangePassword dto)
        {
            if (dto == null)
                return BadRequest("Data Is Not Correct");

            if(dto.NewPassword==dto.OldPassword)
                return BadRequest("New Password Cannot Be The Same As Old Password");

            if (!_SalesBLL.ChangePassword(dto))
                return NotFound("Old Password is Not Correct");

            return Ok("Password Changed Successfuly");

        }


        [HttpPut("/Admin/ChangePassword")]
        [Authorize]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangePasswordForAdmin([FromBody] DToChangePassword dto)
        {
            if (dto == null)
                return BadRequest("Data Is Not Correct");

            if (dto.NewPassword == dto.OldPassword)
                return BadRequest("New Password Cannot Be The Same As Old Password");


            if (!_AdminBLL.ChangePassword(dto))
                return NotFound("Error in Change Password");

            return Ok("Password Changed Successfuly");

        }

        [HttpPut("/Adminstrative/ChangePassword")]
        [Authorize]
        [Authorize(Roles = "Adminstrative,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangePasswordForAdminstrative([FromBody]DToChangePassword dto)
        {
            if (dto == null)
                return BadRequest("Data Is Not Correct");
            if (dto.NewPassword == dto.OldPassword)
                return BadRequest("New Password Cannot Be The Same As Old Password");
            if (!_AdminstrativeBL.ChangePassword(dto))
                return BadRequest("Error in Change Password");

            return Ok("Password Changed Successfuly");

        }

       

    }
}
