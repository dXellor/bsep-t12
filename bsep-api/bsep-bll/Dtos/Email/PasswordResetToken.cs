using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Dtos.Email
{
    public class PasswordResetToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string TokenHash { get; set; }
    }
}
