using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.Visits.DTO
{
    public enum enStatusVisit { UnInterest=1,Interest=2,Bought=3,Canceled=4}
    public class DtoVisitAdd
    {
        [JsonIgnore]
        public DateTime CreateAt {  get; set; }
        public int Status { get; set; }
        [JsonIgnore]
        public int CraetedBySaleID { get; set; }
        public int CustomerID { get; set; }
        public int PlaceId { get; set; }

    }
}
