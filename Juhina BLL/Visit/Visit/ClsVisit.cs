using Juhyna_BLL.Visit.InterFace;
using Juhyna_DAL.Visits.DTO;
using Juhyna_DAL.Visits.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Visit.Visit
{
    public class ClsVisit: IVisitBLL
    {
        private readonly IVisitDAL _Visit;
        public ClsVisit(IVisitDAL visit)
        {
            _Visit = visit; 
        }
        public List<DtoVisitRead> GetAllVisits()
        {
            return _Visit.GetAllVisits();    
        }
        public DtoVisitRead GetVisitReadById(int ID)
        {
            return _Visit.GetVisitReadById(ID);
        }
        public DtoVisitUpdate GetVisitById(int ID)
        {
            return _Visit.GetVisitById(ID);
        }

        public List<DtoVisitRead> GetAllVisitForSaleId(int SaleId,DateTime Date)
        {
            return _Visit.GetAllVisitForSaleId(SaleId,Date);
        }
        public List<DtoVisitRead> GetAllVisitForCustomerId(int CustomerId,DateTime Date)
        {
            return _Visit.GetAllVisitForCustomerId(CustomerId, Date);
        }
        public bool DeleteVisit(int ID)
        {
            return _Visit.DeleteVisit(ID);
        }
        public DtoVisitRead UpdateVisit(DtoVisitUpdate dto)
        {
            return _Visit.UpdateVisit(dto);
        }
        public DtoVisitUpdate AddVisit(DtoVisitAdd dto)
        {
            return _Visit.AddVisit(dto);
        }
    }
}
