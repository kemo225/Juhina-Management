using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Visits.DTO
{
    public class DtoVisitRead
    {
        public int Id { get; set; } 
        public string Status { get; set; }
        public string CraetedBySaleName { get; set; }
        public string CustomerName { get; set; }
        public string LocationCustomer { get; set; }
        public string PhonCustomer {  get; set; }
        public DateTime CreateAt {  get; set; }
    }
}
