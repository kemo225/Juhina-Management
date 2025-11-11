using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_BLL.Hashing.NewFolder
{
    public interface IHashingBLL
    {
        public string GenerateHashedPassword(string Password, string Salt);
        public bool IsPasswordCorrect(string CheckPassword, string CurrentPassword, string Salt);
        public string GenerateSaltString(int size = 16);

    }
}
