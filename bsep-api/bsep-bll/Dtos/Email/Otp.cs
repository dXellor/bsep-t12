using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Dtos.Email
{
    public class Otp
    {
        public string OtpCode { get; set; }
        public DateTime Expires { get; set; }
        public string OtpCodeHash { get; set; }
    }
}
