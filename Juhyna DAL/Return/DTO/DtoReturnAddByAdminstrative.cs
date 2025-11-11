using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Juhyna_DAL.Return.DTO
{
    public class DtoReturnAddByAdminstrative
    {
        public int ID { get; set; }
        [JsonIgnore]
        public DateOnly ConfirmedAt { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
