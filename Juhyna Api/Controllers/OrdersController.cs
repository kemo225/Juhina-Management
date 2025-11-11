using Juhyna_BLL.Admins.InterFace;
using Juhyna_BLL.Customers.InterFace;
using Juhyna_BLL.EmailService;
using Juhyna_BLL.FollowingSaleAdminstrative.Interface;
using Juhyna_BLL.Order.InterFace;
using Juhyna_BLL.Validation;
using Juhyna_BLL.Visit.InterFace;
using Juhyna_DAL.Customer.InterFace;
using Juhyna_DAL.FollowingSaleAdminstrative.DTO;
using Juhyna_DAL.Models;
using Juhyna_DAL.Orders.DTO;
using Juhyna_DAL.Reports;
using Juhyna_DAL.Visits.DTO;
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


    public class OrdersController : ControllerBase
    {
       private readonly IOrderBLL _orderBLL;
        private readonly IInvoicesBLL _InvoiceBLL;
        private readonly IVisitBLL _VisitBLL;
        private readonly ValidationBLL _ValidationBLL;
        private readonly IAdminBLL adminBLL;
        private readonly ICustomerBLL _customerBLL;
        private readonly IMemoryCache _Cashe;
        public  string Cashkey = "";
        public OrdersController(IMemoryCache cache,IOrderBLL orderBLL, ValidationBLL ValidationBLL, IInvoicesBLL InvoiceBLL, IVisitBLL VisitBLL,IAdminBLL adminBLL, ICustomerBLL customerBLL)
        {
            _orderBLL = orderBLL;  
            _InvoiceBLL = InvoiceBLL;
            _VisitBLL = VisitBLL;
            _ValidationBLL = ValidationBLL;
            this.adminBLL = adminBLL;
            _customerBLL = customerBLL;
            _Cashe = cache;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DtoOrderRead>> GetAllOrder()
        {
            Cashkey= "AllOrders";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoOrderRead> Orders))
            {
                Orders = _orderBLL.GetAllOrder();
                if (Orders == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Orders, casheoption);
            }


            return Ok(Orders);
        }

        [HttpGet("Completed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]


        public ActionResult<IEnumerable<DtoOrderRead>> GetAllOrderCompleted()
        {
            Cashkey = "AllCompletedOrders";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoOrderRead> Orders))
            {
                Orders = _orderBLL.GetAllOrderCompleted();
                if (Orders == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Orders, casheoption);
            }


            return Ok(Orders);
        }
        [HttpGet("Canceled")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any, NoStore = false)]
        [OutputCache(Duration = 60)]

        public ActionResult<IEnumerable<DtoOrderRead>> GetAllOrderCanceled()
        {
            Cashkey = "AllCanceledOrders";
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoOrderRead> Orders))
            {
                Orders = _orderBLL.GetAllOrderCanceled();
                if (Orders == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Orders, casheoption);
            }


            return Ok(Orders);
        }



        [HttpGet("{ID}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]

        public ActionResult<DtoOrderRead> GetOrderById([FromRoute] int ID)
        {
            if (ID <= 0)
                return BadRequest("ID Must Be Positive Number");

            var Order = _orderBLL.GetOrderById(ID);
            if (Order == null)
            {
                return NotFound("Data Is Not Found.");
            }
          
                return Ok(Order);
            
        }

        [HttpDelete("{ID}/{InvoiceID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]

        public ActionResult DeleteOrder([FromRoute] int ID,[FromRoute] int InvoiceID)
        {
            if (ID <= 0 || InvoiceID<=0)
                return BadRequest("ID ,  Must Be Positive Number");

            if (!_orderBLL.DeleteOrder(ID, InvoiceID))
                return NotFound("The Order Is Not Found");

            if (Cashkey != "")
                _Cashe.Remove(Cashkey);
            var admins = adminBLL.GetAdmins();
            var Order = _orderBLL.GetOrderById(ID);
            try
            {
                foreach (var admin in admins)
                {
                    string emailBody = $@"
Dear {admin.FirstName} {admin.LastName},

I hope this email finds you well.

Please find new order details for {DateTime.Now}:

Customer Name: {Order.CustomerName}
Representative Name: {Order.SaleName}
Product Name: {Order.ProductName}
Payment Method: {Order.PaymentMethod}
Customer Phone: {Order.PhoneCustomer}
Quantity: {Order.Quantity}
Product Price: {Order.ProductPrice}
Total Price: {Order.TotalPrice}

[Optional: Any brief notes or highlights, e.g., 'The increase in sales in [Product/Service] contributed to a higher net profit today.']

Please let me know if you need any further details or breakdowns.

Thank you.

Best regards,
Juhina Company
";

                    EmailServiceBLL.SendEmail(admin.Email, "Order Canceled", emailBody);
                }

            }
            catch (Exception ex)
            {
                return CreatedAtRoute("GetOrderById", new { id = Order.Id }, Order);
            }
            return Ok("The Order Is Deleted Successfuly");

        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Sale")]


        public ActionResult<DtoVisitRead> AddOrder([FromBody]DtoOrderAdd dto)
        {
            try
            {
                var CreatedBySaleID = int.Parse(User.FindFirst("ID")!.Value);
                dto.CreatedBySaleID = CreatedBySaleID;
                if (dto.VisitID <= 0 || dto.InvoiceID <= 0 || dto.CreatedBySaleID <= 0 || dto.PaymentmethodID <= 0 || dto.Quantity <= 0 || dto.Status < 0)
                    return BadRequest("Must Be InvoiceID , CreatedBySaleID , VisitID , PaymentmethodID , Quantity , Status = 1 OR 2  Positive Number ");

                var Visit = _VisitBLL.GetVisitById(dto.VisitID);
                var Invoice = _InvoiceBLL.GetInvoiceBetweenSaleAdminstrativeByID(dto.InvoiceID);

                if (Visit == null)
                    return BadRequest($"There is not Visit By This ID : {dto.VisitID}");
                if(Visit.CraetedBySaleID != dto.CreatedBySaleID)
                    return BadRequest($"The Visit that ID : {dto.VisitID} Not Belong to the Sale that ID : {dto.CreatedBySaleID}");
                if (Invoice == null)
                    return BadRequest($"There is not Invoice By This ID : {dto.InvoiceID}");
                if (!_ValidationBLL.IsCanDoOrder(Invoice.Rest, dto.Quantity))
                    return BadRequest($"Cannot Do Order Because The Invoice that ID : {dto.InvoiceID} Not Enough to Exchange {dto.Quantity} The Rest in invoice only {Invoice.Rest}");

                var Order = _orderBLL.AddOrder(dto);
                if (Order == null)
                    return NotFound("The Order Is Not Found");
                var customer = _customerBLL.GetCustomerBYID(Visit.CustomerID);

                    var admins = adminBLL.GetAdmins();
                try
                {
                    foreach (var admin in admins)
                    {
                        string emailBody = $@"
Dear {admin.FirstName} {admin.LastName},

I hope this email finds you well.

Please find new order details for {DateTime.Now}:

Customer Name: {customer.FirstName} {customer.LastName}
Representative Name: {Order.SaleName}
Product Name: {Order.ProductName}
Payment Method: {Order.PaymentMethod}
Customer Phone: {Order.PhoneCustomer}
Quantity: {Order.Quantity}
Product Price: {Order.ProductPrice}
Total Price: {Order.TotalPrice}

[Optional: Any brief notes or highlights, e.g., 'The increase in sales in [Product/Service] contributed to a higher net profit today.']

Please let me know if you need any further details or breakdowns.

Thank you.

Best regards,
Juhina Company
";

                        EmailServiceBLL.SendEmail(admin.Email, "New Order Successful", emailBody);
                    }

                }catch(Exception ex)
                {
                    return CreatedAtRoute("GetOrderById", new { id = Order.Id }, Order);
                }
                if (Cashkey != "")
                    _Cashe.Remove(Cashkey);
                return CreatedAtRoute("GetOrderById", new { id = Order.Id }, Order);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           

        }



        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<DtoOrderRead> UpdateOrder(DToOrderUpdate dto)
        //{
        //    if (dto.ID <= 0 || dto.CreatedBySaleID <= 0)
        //        return BadRequest("ID Must Be Positive Number");

        //    var order=_orderBLL.GetOrderById(dto.ID);

        

        //        var Invoice = _InvoiceBLL.GetInvoiceBetweenSaleAdminstrativeByID(dto.InvoiceID);
        //    if (Invoice == null)
        //        return BadRequest("");
        //    else if (_ValidationBLL.IsCanUpdateOrder(order.Status))
        //        return BadRequest("The Order that Already Completed Cannot Update It");
        //    else if (!_ValidationBLL.IsCanDoOrder(Invoice.Rest + order.Quantity, dto.Quantity))
        //        return BadRequest($"Cannot Do Order Because The Invoice that ID : {dto.InvoiceID} Not Enough to Exchange {dto.Quantity}");
        //    var Order = _orderBLL.UpdateOrder(dto);
        //    if (Order == null)
        //        return NotFound("Falied In Update Order");
        //    else
        //        return Ok(Order);

        //}
    }
}
