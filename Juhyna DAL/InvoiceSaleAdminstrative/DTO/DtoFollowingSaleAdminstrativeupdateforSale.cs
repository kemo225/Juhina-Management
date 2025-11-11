using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.FollowingSaleAdminstrative.DTO
{
    public class DtoFollowingSaleAdminstrativeupdateforSale
    {
        public int Id { get; set; } 
        public bool IsConfirm {  get; set; }
        public DateTime ConfirmAt { get; set; }
    }
}
