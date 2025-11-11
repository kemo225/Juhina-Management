using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Sales.DtoSales
{
    public class DTOSalesAdd
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; }
        public string UsertName { get; set; }
        public string password { get; set; }
        public int CreatedBYAdminID { get; set; }
    }
}
