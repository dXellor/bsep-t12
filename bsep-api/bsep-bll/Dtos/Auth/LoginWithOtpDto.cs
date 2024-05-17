using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Dtos.Auth
{
    public class LoginWithOtpDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }

        public LoginWithOtpDto(string email, string otp)
        {
            Email = email;
            Otp = otp;
        }
    }
}
