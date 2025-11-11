using Juhyna_DAL.Visits.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Visit.InterFace
{
    public interface IVisitBLL
    {

        public DtoVisitUpdate GetVisitById(int id);
        public List<DtoVisitRead> GetAllVisits();
        public List<DtoVisitRead> GetAllVisitForCustomerId(int CustomerId, DateTime Date);
        public List<DtoVisitRead> GetAllVisitForSaleId(int SaleId, DateTime Date);
        public DtoVisitRead GetVisitReadById(int id);
        public DtoVisitRead UpdateVisit(DtoVisitUpdate dto);
        public DtoVisitUpdate AddVisit(DtoVisitAdd dto);
        public bool DeleteVisit(int Id);
    }
}
