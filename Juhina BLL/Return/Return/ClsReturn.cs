using AutoMapper;
using Juhyna_BLL.Return.InterFace;
using Juhyna_DAL.Models;
using Juhyna_DAL.Return.DTO;
using Juhyna_DAL.Return.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Return.Return
{
    public class ClsReturn:IReturnBLL
    {
        private readonly IReturnDAL _RetiurnDal;
        private readonly IMapper mapper;
        public ClsReturn( IMapper mapper, IReturnDAL RetiurnDal)
        {
            this.mapper = mapper;
            _RetiurnDal = RetiurnDal;
        }
        public List<DtoReturnRead> GetAllReturns()
        {
            return _RetiurnDal.GetAllReturns();
        }
        public List<DtoReturnRead> GetAllUnConfirmedReturns()
        {
           return _RetiurnDal.GetAllUnConfirmedReturns();
        }
        public List<DtoReturnRead> GetAllConfirmedReturns()
        {
         return _RetiurnDal.GetAllConfirmedReturns();
        }
        public List<DtoReturnRead> GetAllReturnsBySaleID(int SaleID)
        {
            return _RetiurnDal.GetAllReturnsBySaleID(SaleID);
        }
        public List<DtoReturnRead> GetAllReturnsTodayBySaleID(int SaleID)
        {
            return _RetiurnDal.GetAllReturnsTodayBySaleID(SaleID);
        }
        public DtoReturnRead GetReturnById(int id)
        {
          return _RetiurnDal.GetReturnById(id);
        }
        public DtoReturnAddBySaleReturned AddReturn(DtoReturnAddBySale dtoReturnAddBySale)
        {
           return _RetiurnDal.AddReturn(dtoReturnAddBySale);
        }
        public bool IsEvaluateReturn(DtoReturnAddByAdminstrative dtoReturnAddByAdminstrative)
        {
          return _RetiurnDal.IsEvaluateReturn(dtoReturnAddByAdminstrative);
        }
        public bool DeleteReturnById(int returnId)
        {
          return _RetiurnDal.DeleteReturnById(returnId);
        }
        public bool UpdateReturnBySale(DtoReturnUpdateBySale dtoReturnUpdate)
        {
          return _RetiurnDal.UpdateReturnBySale(dtoReturnUpdate);   
        }
        public bool UpdateReturnByAdminstrative(DtoReturnUpdateByAdminstrarive dtoReturnUpdate)
        {
            return _RetiurnDal.UpdateReturnByAdminstrative(dtoReturnUpdate);
        }
        }
}
