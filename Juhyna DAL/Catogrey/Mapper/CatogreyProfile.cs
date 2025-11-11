using AutoMapper;
using Juhyna_DAL.Catogrey.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Catogrey.Mapper
{
    public class CatogreyProfile:Profile
    {
        public CatogreyProfile()
        {
            CreateMap<Models.Category, DtoCatogreyRead>()
                .ForMember(to => to.ID, from => from.MapFrom(from => from.ID))
                .ForMember(to => to.name, from => from.MapFrom(from => from.Name));
        }
    }
}
