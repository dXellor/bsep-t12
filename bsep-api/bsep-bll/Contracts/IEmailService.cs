using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Contracts
{
    public interface IEmailService
    {
        void SendTest();
        void SendActivationMessage(string recipient, string token);
        void SendOTPMessage(string recipient, string otp);
        void SendBlockMessage(string recipient);
    }
}
