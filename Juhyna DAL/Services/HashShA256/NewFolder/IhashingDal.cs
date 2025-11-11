using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Services.HashShA256.NewFolder
{
    public interface IhashingDal
    {
        public  string GenerateHashedPassword(string Password, string Salt);
        public  bool IsPasswordCorrect(string CheckPassword, string CurrentPassword, string Salt);
        public  string GenerateSaltString(int size = 16);
    }
}
