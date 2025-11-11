using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Models
{
    public class TokenAdmin
    {
        [Key]
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessExpireAt { get; set; }
        public DateTime RefreshExpireAt { get; set; }
        public bool IsLogin { get; set; }
        [ForeignKey("Admin")]
        public int AdminID { get; set; }
        public string SaltAccessToken { get; set; }
        public string SaltRefreshToken { get; set; }

        public virtual Admin Admin { get; set; }

    }
}
