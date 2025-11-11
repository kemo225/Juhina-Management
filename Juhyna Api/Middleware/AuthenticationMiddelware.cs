using Juhyna_BLL.Hashing.NewFolder;
using Juhyna_BLL.Token.InterFace;
using Juhyna_DAL.Models;
using System.Net;
using System.Text.Json;

namespace Juhyna_Api.Middleware
{
    public class AuthenticationMiddelware

    {
        private readonly RequestDelegate _next;
        private readonly ItokenBLL _TokenBLL;
        private readonly IHashingBLL _hashingBLL;


        public AuthenticationMiddelware(RequestDelegate next)
        {
            _next= next;
        }

        public async Task InvokeAsync(HttpContext context)
        { // this h2n f middelware
           var _TokenBLL = context.RequestServices.GetRequiredService<ItokenBLL>();
              var _hashingBLL = context.RequestServices.GetRequiredService<IHashingBLL>();

            // check if the token Logout before
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
                {
                    if(context.User.IsInRole("Admin") )
                    {
                        var TokenAdmins=  _TokenBLL.GetAllTokenAdmins();
                        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
                        foreach(var tokenadmin in TokenAdmins)
                        {
                            if(_hashingBLL.IsPasswordCorrect(token,tokenadmin.RefreshToken,tokenadmin.SaltRefreshToken))
                                {
                                if(tokenadmin.IsLogin == false)
                                {
                                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";

                                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "You Loggout By This Token Before" }));
                                return;
                            }
                            }
                        if (_hashingBLL.IsPasswordCorrect(token, tokenadmin.AccessToken, tokenadmin.SaltAccessToken))
                        {
                            if (tokenadmin.IsLogin == false)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";
                                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "You Loggout By This Token Before" }));
                                return;
                            }
                        }
                    }
                    }
                    if(context.User.IsInRole("Adminstrative"))
                    {
                        var TokenAdminstratives=  _TokenBLL.GetAllTokenAdminstratives();
                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
                        foreach(var tokenadminstrative in TokenAdminstratives)
                        {
                        if (_hashingBLL.IsPasswordCorrect(token, tokenadminstrative.RefreshToken, tokenadminstrative.SaltRefreshToken))
                        {
                            if (tokenadminstrative.IsLogin == false)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";

                                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "You Loggout By This Token Before" }));
                                return;
                            }
                        }
                        if (_hashingBLL.IsPasswordCorrect(token, tokenadminstrative.AccessToken, tokenadminstrative.SaltAccessToken))
                        {
                            if (tokenadminstrative.IsLogin == false)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "You Loggout By This Token Before" }));
                                return;
                            }
                        }
                    }
                    }
                    if(context.User.IsInRole("Sale"))
                    {
                        var TokenSales=  _TokenBLL.GetAllTokenSales();
                        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
                        foreach(var tokensale in TokenSales)
                        {
                        if (_hashingBLL.IsPasswordCorrect(token, tokensale.RefreshToken, tokensale.SaltRefreshToken))
                        {
                            if (tokensale.IsLogin == false)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";

                                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "You Loggout By This Token Before" }));
                                return;
                            }
                        }
                        if (_hashingBLL.IsPasswordCorrect(token, tokensale.AccessToken, tokensale.SaltAccessToken))
                        {
                            if (tokensale.IsLogin == false)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";

                                await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "You Loggout By This Token Before" }));
                                return;
                            }
                        }
                    }
                    }


                }

                await _next(context);
       
          
        }
    }
}
