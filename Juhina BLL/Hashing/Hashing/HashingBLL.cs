using Juhyna_BLL.Hashing.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Hashing.Hashing
{
    public class HashingBLL: IHashingBLL
    {
        private readonly Juhyna_DAL.Services.HashShA256.NewFolder.IhashingDal _hashingDal;
        public HashingBLL(Juhyna_DAL.Services.HashShA256.NewFolder.IhashingDal hashingDal)
        {
            _hashingDal = hashingDal;
        }
        public string GenerateHashedPassword(string Password, string Salt)
        {
            return _hashingDal.GenerateHashedPassword(Password, Salt);
        }
        public bool IsPasswordCorrect(string CheckPassword, string CurrentPassword, string Salt)
        {
            return _hashingDal.IsPasswordCorrect(CheckPassword, CurrentPassword, Salt);
        }
        public string GenerateSaltString(int size = 16)
        {
            return _hashingDal.GenerateSaltString(size);
        }
    }
}
