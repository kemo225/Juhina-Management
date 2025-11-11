using AutoMapper;
using Juhyna_DAL.Catogrey.DTO;
using Juhyna_DAL.Catogrey.InterFace;
using Juhyna_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Catogrey.Catogrey
{
    public class CatogreyDAL:ICatogreyDAL
    {
        private readonly JuhinaDBContext _context;
        private readonly IMapper _mapper;   
        public CatogreyDAL(JuhinaDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<DtoCatogreyRead> GetAllCatogrey()
        {
            var catogreys = _context.Categories.ToList();
            if (catogreys == null)
                return null;
            return _mapper.Map<List<DtoCatogreyRead>>(catogreys);
        }
        public DtoCatogreyRead GetCatogreybyID(int id)
        {
            var catogrey = _context.Categories.FirstOrDefault(c => c.ID == id);
            if (catogrey == null)
                return null;
            return _mapper.Map<DtoCatogreyRead>(catogrey);
        }

    }
}
