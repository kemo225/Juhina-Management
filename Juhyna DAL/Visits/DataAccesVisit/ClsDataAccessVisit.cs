using AutoMapper;
using Juhyna_DAL.Models;
using Juhyna_DAL.Visits.DTO;
using Juhyna_DAL.Visits.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Visits.DataAccesVisit
{
    public class ClsDataAccessVisit: IVisitDAL
    {
        private readonly JuhinaDBContext _juhinaDB;
        private readonly IMapper _mapper;
        public ClsDataAccessVisit(JuhinaDBContext juhinaDB,IMapper mapper)
        {
            _juhinaDB = juhinaDB;
            _mapper = mapper;
        }

        public List<DtoVisitRead> GetAllVisits()
        {
            var Visits = _juhinaDB.Visits.AsNoTracking().Include(v => v.CreatedBySale)
                .ThenInclude(c => c.Person).Include(v => v.Customer)
                .ThenInclude(c => c.Person).Include(V=>V.Place).ToList();
            if (Visits == null)
                return null;
            else
            {
                return _mapper.Map<List<DtoVisitRead>>(Visits); 
            }
        }

        public List<DtoVisitRead> GetAllVisitForCustomerId(int CustomerId , DateTime Date)
        {
            var Visits = _juhinaDB.Visits.Include(v => v.CreatedBySale)
                .ThenInclude(c => c.Person).Include(v => v.Customer)
                .ThenInclude(c => c.Person).Include(V => V.Place).Where(v => v.CreatedBySaleID == CustomerId && v.CreateAt.HasValue && v.CreateAt.Value.Date == Date.Date).ToList();
            if (Visits == null || Visits.Count == 0)
                return null;
            else
            {
                return _mapper.Map<List<DtoVisitRead>>(Visits);
            }
        }

        public List<DtoVisitRead> GetAllVisitForSaleId(int SaleId, DateTime Date)
        {
            var Visits = _juhinaDB.Visits.Include(v => v.CreatedBySale)
                .ThenInclude(c => c.Person).Include(v => v.Customer)
                .ThenInclude(c => c.Person).Include(V => V.Place).Where(v => v.CreatedBySaleID == SaleId&&v.CreateAt.HasValue &&v.CreateAt.Value.Date==Date.Date).ToList();
            if (Visits == null||Visits.Count==0)
                return null;
            else
            {
                return _mapper.Map<List<DtoVisitRead>>(Visits);
            }
        }

        public DtoVisitRead GetVisitReadById(int id)
        {
            var Visits = _juhinaDB.Visits.Include(v => v.CreatedBySale)
                .ThenInclude(c => c.Person).Include(v => v.Customer)
                .ThenInclude(c => c.Person).Include(V => V.Place).Where(v=>v.ID==id).FirstOrDefault();
            if (Visits == null)
                return null;
            
           
                return _mapper.Map<DtoVisitRead>(Visits);
            
        }
        public DtoVisitUpdate GetVisitById(int id)
        {
            var Visits = _juhinaDB.Visits.FirstOrDefault();
            if (Visits == null)
                return null;
            else
            {
                return _mapper.Map<DtoVisitUpdate>(Visits);
            }
        }

        public DtoVisitRead UpdateVisit(DtoVisitUpdate dto)
        {
            var visit = _juhinaDB.Visits.Include(v => v.CreatedBySale)
                .ThenInclude(c => c.Person).Include(v => v.Customer)
                .ThenInclude(c => c.Person).Include(V => V.Place).Where(v => v.ID == dto.Id).FirstOrDefault();
            if (visit == null)
                return null;
            else
            {
                visit.CreatedBySaleID = dto.CraetedBySaleID;
                visit.CustomerID=dto.CustomerID;
                visit.Status=dto.Status;
                _juhinaDB.SaveChanges();
                return _mapper.Map<DtoVisitRead>(visit);   
            }
        }

        public DtoVisitUpdate AddVisit(DtoVisitAdd dto)
        {
            try
            {
                var visit = new Visit()
                {
                    CreateAt = DateTime.Now,
                    CustomerID = dto.CustomerID,
                    CreatedBySaleID = dto.CraetedBySaleID,
                    Status = dto.Status,
                    PlaceID = dto.PlaceId,
                };
                _juhinaDB.Add(visit);
                _juhinaDB.SaveChanges();
                return _mapper.Map<DtoVisitUpdate>(visit);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool DeleteVisit(int Id)
        {
            var visit = _juhinaDB.Visits.Find(Id);
            if (visit == null) return false;
            else
            {
                _juhinaDB.Remove(visit);
                _juhinaDB.SaveChanges();
                return true;
            }

        }
        
    }
}
