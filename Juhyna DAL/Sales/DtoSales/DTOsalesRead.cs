using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Sales.DtoSales
{
    public class DTOsalesRead
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; }
        public string CreatedByAdmin { get; set; }
        public string InventoryName { get; set; }
    }
}
