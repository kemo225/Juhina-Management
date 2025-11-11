using Juhyna_BLL.CustomerPlace.InterFace;
using Juhyna_BLL.Customers.InterFace;
using Juhyna_BLL.Return.InterFace;
using Juhyna_BLL.Visit.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.Return.DTO;
using Juhyna_DAL.Visits.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;

namespace Juhyna_Api.Controllers
{
    // Response Api
    // Outout Api
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public class VisitsController : ControllerBase
    {
        private readonly IVisitBLL _Visit;
        private readonly IMemoryCache _Cashe;
        private readonly ICustomerPlaceBLL _customerPlaceBLL;
        private readonly ICustomerBLL _CustomerBLL;
        public string Cashkey = "JuhinaVisits";
        public VisitsController(IVisitBLL Visit,IMemoryCache Cashe, ICustomerPlaceBLL customerPlaceBLL, ICustomerBLL CustomerBLL)
        {
            _Visit = Visit;
            _Cashe = Cashe;
            _customerPlaceBLL = customerPlaceBLL;
            _CustomerBLL = CustomerBLL;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ResponseCache(Duration =30, Location = ResponseCacheLocation.Any, NoStore = false)]// SLOWER STORE IN CLIENT
        [OutputCache(Duration = 60)]// FASTER STORE IN SERVER
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DtoVisitRead>> GetAllVisits()
        {
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoVisitRead> Visits))
            {
                Visits = _Visit.GetAllVisits();
                if (Visits == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Visits, casheoption);
            }
            return Ok(Visits);
        }



        [HttpGet("ForSale/{SaleID}/{Date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
 

        [Authorize(Roles = "Admin,Sale")]

        public ActionResult<IEnumerable<DtoVisitRead>> GetAllVisitForSaleby([FromRoute]int SaleID,[FromRoute] DateTime Date)
        {
            if (SaleID <= 0)
                return BadRequest("ID Must Be Positive Number");
            if(Date == null)
                return BadRequest("Date Is Not Valid");

          var  Visits = _Visit.GetAllVisitForSaleId(SaleID,Date);
                if (Visits == null)
                    return NotFound("Data Is Not Found");
          
            return Ok(Visits);
        }

        [HttpGet("ForCustomer/{CustomerID}/{Date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<IEnumerable<DtoVisitRead>> GetAllVisitForCustomerby([FromRoute]int CustomerID,[FromRoute] DateTime Date)
        {
            if (CustomerID <= 0)
                return BadRequest("Data Is Not Valid");
            if (!_Cashe.TryGetValue(Cashkey, out List<DtoVisitRead> Visits))
            {
                Visits = _Visit.GetAllVisitForCustomerId(CustomerID,Date);
                if (Visits == null)
                    return NotFound("Data Is Not Found");
                var casheoption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _Cashe.Set(Cashkey, Visits, casheoption);
            }
            return Ok(Visits);
        }

        [HttpGet("{ID}",Name = "GetVisitById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [OutputCache(Duration = 60, VaryByRouteValueNames = new[] { "ID" })]


        public ActionResult<DtoVisitRead> GetVisitById([FromRoute]int ID)
        {
            if (ID <= 0)
                return BadRequest("ID Must Be Positive Number");

            var visits = _Visit.GetVisitById(ID);
            if ( visits == null)
            {
                return NotFound("Data Is Not Found.");
            }
          
                return Ok(visits);
            
        }

        [HttpDelete("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Sale")]

        public ActionResult<DtoVisitRead> DeleteVisit([FromRoute]int ID)
        {
            if (ID <= 0)
                return BadRequest("ID Must Be Positive Number");
            var visit = _Visit.GetVisitById(ID);
            if (visit == null)
                return NotFound("The Visit Is Not Found");
            if(visit.Status == Convert.ToInt32(enStatusVisit.Bought))
                return BadRequest("You Cannot Delete A Bought Visit");
            if (visit.Status == Convert.ToInt32(enStatusVisit.Canceled))
                return BadRequest("You Cannot Delete An Canceled Visit");
            if (visit.CraetedBySaleID ==Convert.ToInt32(User.Claims.FirstOrDefault()))
                return BadRequest("You Cannot Delete An Canceled Visit");
            if (!_Visit.DeleteVisit(ID))
                return BadRequest("Failed in Delete");
            
            _Cashe.Remove(Cashkey); 
            return Ok("The Visit Is Deleted Successfuly");
            
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Sale")]
        
        public ActionResult<DtoVisitRead> AddVisit([FromBody] DtoVisitAdd dto)
        {
            var createdBySaleID = int.Parse(User.FindFirst("ID")!.Value);
            if (createdBySaleID <= 0)
                return BadRequest("ID Must Be Positive Number");
            var customer = _CustomerBLL.GetCustomerBYID(dto.CustomerID);
            if (customer == null)
                return BadRequest("This Customer Is Not Registered");
            var place = _customerPlaceBLL.GetShopsByCistomerID(dto.CustomerID);
            if (place == null)
                return BadRequest("This Customer Does Not Have Any Place Registered");
            if (dto.Status<=0)
                return BadRequest("Status Is Not Valid");
            if(dto.Status>4)
                return BadRequest("Status Is Not Valid");
            dto.CraetedBySaleID = createdBySaleID;

          

            var visit = _Visit.AddVisit(dto);
            if (visit==null)
                return BadRequest("error when add visits");

                _Cashe.Remove(Cashkey);
            return CreatedAtRoute("GetVisitById", new {id=visit.Id},visit);

        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Sale")]


        public ActionResult<DtoVisitRead> UpdateVisit([FromBody]DtoVisitUpdate dto)
        {
            if (dto.CraetedBySaleID <= 0 || dto.CustomerID <= 0)
                return BadRequest("ID Must Be Positive Number");
            var visit = _Visit.GetVisitById(dto.Id);
            if (visit == null)
                return NotFound("The Visit Is Not Found");
            if (visit.Status == Convert.ToInt32(enStatusVisit.Bought))
                return BadRequest("You Cannot Update A Bought Visit");
            if (visit.Status == Convert.ToInt32(enStatusVisit.Canceled))
                return BadRequest("You Cannot update An Canceled Visit");
            if (visit.CraetedBySaleID == Convert.ToInt32(User.Claims.FirstOrDefault()))
                return BadRequest("You Cannot Upate An Canceled Visit");
            var visitreturn = _Visit.UpdateVisit(dto);
            if (visitreturn == null)
                return NotFound("The Visit Is Not Found");
            else
                _Cashe.Remove(Cashkey);
            return Ok(visitreturn);

        }
    }
}
