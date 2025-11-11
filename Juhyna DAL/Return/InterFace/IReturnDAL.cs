using AutoMapper;
using Juhyna_DAL.Return.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.InterFace
{
    public interface IReturnDAL
    {
        public List<DtoReturnRead> GetAllReturnsBySaleID(int SaleID);
        public List<DtoReturnRead> GetAllReturnsTodayBySaleID(int SaleID);

        public List<DtoReturnRead> GetAllReturns();
        public List<DtoReturnRead> GetAllUnConfirmedReturns();
        public List<DtoReturnRead> GetAllConfirmedReturns();
        public DtoReturnRead GetReturnById(int id);
        public DtoReturnAddBySaleReturned AddReturn(DtoReturnAddBySale dtoReturnAddBySale);
        public bool IsEvaluateReturn(DtoReturnAddByAdminstrative dtoReturnAddByAdminstrative);
        public bool DeleteReturnById(int returnId);
        public bool UpdateReturnBySale(DtoReturnUpdateBySale dtoReturnUpdate);
        public bool UpdateReturnByAdminstrative(DtoReturnUpdateByAdminstrarive dtoReturnUpdate);
    }
}
