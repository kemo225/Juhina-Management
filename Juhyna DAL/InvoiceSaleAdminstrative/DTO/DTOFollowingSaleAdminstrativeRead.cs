using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.FollowingSaleAdminstrative.DTO
{
    public class DTOFollowingSaleAdminstrativeRead
    {
        public int ID {  get; set; }
        public DateOnly CreatedAt { get; set; }
        public int Quantity { get; set; }
        public int Bought { get; set; }
        public int Rest { get; set; }
        public string EmailAdminstrative { get; set; }
        public string ProductName { get; set; } //DataBase Name Description
        public string Size { get; set; }
        public string Categorey { get; set; }
        public string CreatedByAdminstrativeName { get; set; }
        public string SaleName { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public bool IsConfrmed { get; set; }= false;

    }
}
