using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.CustomerPlace.DTO
{
    public class DTOCustomerPlaceRead
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int PlaceID { get; set; }    
        public string PlaceName { get; set; }
        public string PlaceAddress { get; set; }
    }
}
