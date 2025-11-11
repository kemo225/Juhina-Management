using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Visits.DTO
{
    public class DtoVisitUpdate
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int CraetedBySaleID { get; set; }
        public int CustomerID { get; set; }
        public int PlaceID { get; set; }
    }
}
