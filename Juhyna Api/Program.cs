using Juhyna_Api.Middleware;
using Juhyna_Api.Serviecs;
using Juhyna_BLL.Admins.ClsAdmin;
using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.AdminStrative.Adminstrative;
using Juhyna_BLL.AdminStrative.InterFace;
using Juhyna_BLL.Catogrey.Catogrey;
using Juhyna_BLL.Catogrey.ICatogreyBLL;
using Juhyna_BLL.CustomerPlace.CustomerPlace;
using Juhyna_BLL.CustomerPlace.InterFace;
using Juhyna_BLL.Customers.Customers;
using Juhyna_BLL.Customers.InterFace;
using Juhyna_BLL.EmailService.InterFace;
using Juhyna_BLL.FollowingSaleAdminstrative.Interface;
using Juhyna_BLL.FollowingSaleAdminstrative.NewFolder;
using Juhyna_BLL.Hashing.Hashing;
using Juhyna_BLL.Hashing.NewFolder;
using Juhyna_BLL.Inevntory.InterFace;
using Juhyna_BLL.Inevntory.Inventory;
using Juhyna_BLL.ManageProductInventory.InterFace;
using Juhyna_BLL.ManageProductInventory.ManageProductInventory;
using Juhyna_BLL.NotificationService.Notification;
using Juhyna_BLL.Order.InterFace;
using Juhyna_BLL.Order.Order;
using Juhyna_BLL.Payment.InterFace;
using Juhyna_BLL.Payment.Paymentmethod;
using Juhyna_BLL.ProductIneventory.InterFace;
using Juhyna_BLL.ProductIneventory.productInventory;
using Juhyna_BLL.Products.InterFace;
using Juhyna_BLL.Products.Product;
using Juhyna_BLL.Return.InterFace;
using Juhyna_BLL.Return.Return;
using Juhyna_BLL.SaleCustomer.InterFace;
using Juhyna_BLL.SaleCustomer.SaleCustomer;
using Juhyna_BLL.Sales.clsSales;
using Juhyna_BLL.Sales.InterFace;
using Juhyna_BLL.Token.InterFace;
using Juhyna_BLL.Token.Token;
using Juhyna_BLL.Validation;
using Juhyna_BLL.Visit.InterFace;
using Juhyna_BLL.Visit.Visit;
using Juhyna_DAL.Admins.DataaccessAdmin;
using Juhyna_DAL.Admins.InterFace;
using Juhyna_DAL.Admins.Mapper;
using Juhyna_DAL.Adminstrative.DataAccessAdminstrative;
using Juhyna_DAL.Adminstrative.InterFace;
using Juhyna_DAL.Catogrey.InterFace;
using Juhyna_DAL.Customer.DataAccessCustmer;
using Juhyna_DAL.Customer.InterFace;
using Juhyna_DAL.CustomerPlace.DataAccessCustomerPlaces;
using Juhyna_DAL.CustomerPlace.InterFace;
using Juhyna_DAL.EmailService.InterFce;
using Juhyna_DAL.EmailService.NewFolder;
using Juhyna_DAL.FollowingSaleAdminstrative.DatacAccessFolllowingsaleAdminstrative;

//using Juhyna_DAL.FollowingSaleAdminstrative.DatacAccessFolllowingsaleAdminstrative;
using Juhyna_DAL.FollowingSaleAdminstrative.InterFace;
using Juhyna_DAL.Inventory.InterFace;
using Juhyna_DAL.Inventory.Inventory;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.DataAccess;
using Juhyna_DAL.ManageProductInInventoryByAdminstrative.InterFace;
using Juhyna_DAL.ManageProducts.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.Orders.DataAccess;
using Juhyna_DAL.Orders.InterFace;
using Juhyna_DAL.PaymentMethod.InterFace;
using Juhyna_DAL.PaymentMethod.Paymentmethod;
using Juhyna_DAL.ProductInventory.DataAcceaaManageProduct;
using Juhyna_DAL.Products.InterFace;
using Juhyna_DAL.Products.Products;
using Juhyna_DAL.Reports.InterFace;
using Juhyna_DAL.Reports.ReportSend;
using Juhyna_DAL.Return.DataAccessReturn;
using Juhyna_DAL.Return.InterFace;
using Juhyna_DAL.SaleCustomer.DataAccessSaleCustomer;
using Juhyna_DAL.SaleCustomer.InterFace;
using Juhyna_DAL.Sales.DataAccessSales;
using Juhyna_DAL.Sales.InterFace;
using Juhyna_DAL.Services.HashShA256.Hasing;
using Juhyna_DAL.Services.HashShA256.NewFolder;
using Juhyna_DAL.Services.InterFace_Token;
using Juhyna_DAL.Services.JWT;
using Juhyna_DAL.Tokens.InterFace;
using Juhyna_DAL.Tokens.Token;
using Juhyna_DAL.Validation;
using Juhyna_DAL.Visits.DataAccesVisit;
using Juhyna_DAL.Visits.Interface;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.RateLimiting;

// awl m Sale Agree Update Product Inventory

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

// setting Authentication  JWT Bearer
builder.Services.AddAuthentication(A => A.DefaultAuthenticateScheme = "Bearer")
  .AddJwtBearer("Bearer", options =>
  {
      string Secretkey =   builder.Configuration["SecretKey"]!;
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key
      };
      // Other configurations...
  });

// Setting Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Type Your Token Like:Bearer YourToken"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
    {
             // link with all EndPoint
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"       // نفس الاسم اللي كتبناه في AddSecurityDefinition
            }
        },
        new string[] {}  // لو عايز تحدد أدوار تكتب هنا، بس هنا بنخليها فاضية
    }
            });
});


builder.Services.AddControllers();
 //Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add automapper
builder.Services.AddAutoMapper(typeof(AdminProfiles).Assembly);//sign fOR THIS ONLY auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());// sign FOR ALL AUTO Mapper

//add Context
var Connecrionstring = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<JuhinaDBContext>(op => op.UseSqlServer(Connecrionstring));
///
builder.Services.AddScoped<IAdminDAL, ClsDataAccessAdmins>();
//

//
builder.Services.AddScoped<IAdminBLL, ClsAdmin>();

builder.Services.AddScoped<ISalesBLL, ClsSales>();

builder.Services.AddScoped<ISales, ClsDataAccessSales>();

builder.Services.AddScoped<IInvoiceDAL, ClsDatacAccessInvoices>();



builder.Services.AddScoped<IInventoryBLL, InventoryBLL>();

//
builder.Services.AddScoped<IInventoryDAL, InventoryDAL>();

//
builder.Services.AddScoped<IInvoicesBLL, ClsInvoice>();

//
builder.Services.AddScoped<IAdminstrative, ClsDataAccessAdminStrative>();

//

//
builder.Services.AddScoped<IVisitDAL, ClsDataAccessVisit>();

//
builder.Services.AddScoped<IReturnDAL, clsDataAccessReturn>();
//
builder.Services.AddScoped<IReturnBLL, ClsReturn>();
//
builder.Services.AddScoped<IToken, JwtToken>();

//
builder.Services.AddScoped<IVisitBLL, ClsVisit>();
//
builder.Services.AddScoped<IProductInventoryDAL, ClsDataAccessProductInevntory>();

//
builder.Services.AddScoped<IproductInevtoryBLL, ClsProductInventory>();
//
builder.Services.AddScoped<IManagementProductInentoryDAl, ClsDataAccessFillProductsInventory>();
//
builder.Services.AddScoped<IManageProductInevntoryBLL, ClsManageProductInventory>();
builder.Services.AddScoped<IReportSend, ReportSend>();
//
builder.Services.AddScoped<IAdminstrativeBLL, ClsAdminStrative>();

builder.Services.AddScoped<ICustomer, ClsDataAccessCustomer>();

builder.Services.AddScoped<ICustomerBLL, ClsCustomer>();
///
builder.Services.AddScoped<ISaleCustomer, ClsDataAccessSaleCustomer>();
///
builder.Services.AddScoped<ISaleCustomerBLL, ClsSaleCustomer>();
///
builder.Services.AddScoped<ICustomerPlace, ClsDataAccessCustomerPlace>();
//
builder.Services.AddScoped<ICustomerPlaceBLL, ClsCustomerPlace>();

//
builder.Services.AddScoped<IOrderDAL, ClsDataAccessOrder>();
//
builder.Services.AddScoped<IOrderBLL, ClsOrder>();
//
builder.Services.AddScoped<IproductBLL, ProductBLL>();
//
builder.Services.AddScoped<IproductDAL, ProductDAL>();
//
builder.Services.AddScoped<IpaymentBLL, Juhyna_BLL.Payment.Paymentmethod.PaymentMethod>();
//
builder.Services.AddScoped<IPaymentDAL, Juhyna_DAL.PaymentMethod.Paymentmethod.PaymentMethod>();

builder.Services.AddScoped<ICatogreyDAL, Juhyna_DAL.Catogrey.Catogrey.CatogreyDAL>();

//
builder.Services.AddScoped<IcatogreyBLL, CatogreyBLL>();

//
builder.Services.AddScoped<ValidationDAL>();

builder.Services.AddScoped<ValidationBLL>();

//
builder.Services.AddScoped<ItokenBLL, TokenBLL>();

builder.Services.AddScoped<ItokenDal, TokenDAL>();
//
builder.Services.AddScoped<IhashingDal, Hashing>();

builder.Services.AddScoped<IHashingBLL, HashingBLL>();
//
builder.Services.AddScoped<INotfifcationDAL, NotificationDAL>();

builder.Services.AddScoped<InotificationBLL, NotificationBLL>();


//
// Add Memory Cache
builder.Services.AddMemoryCache();
// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCoolPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
//
builder.Services.AddRateLimiter(options =>
{
    // sliding window: يسمح بعدد معين من الطلبات في نافذة زمنية محددة
    options.AddSlidingWindowLimiter("sliding", opt =>
    {
        opt.PermitLimit = 10;
        opt.Window = TimeSpan.FromSeconds(10); // النافذة الزمنية
        opt.SegmentsPerWindow = 2; // تقسيم النافذة إلى أجزاء لتحسين الدقة
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 5; // عدد الطلبات التي يمكن وضعها في الطابور
    });
});
//


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); // convert request http to https

app.UseAuthentication();

app.UseMiddleware<AuthenticationMiddelware>();

app.UseAuthorization();

app.MapControllers();//  play system Routing that Read Routes inside Controllers because when arrive request send it to correctly Place 
SheduleRunner.StartShecdule();

app.Run();
