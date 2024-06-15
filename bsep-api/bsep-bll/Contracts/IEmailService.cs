using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Contracts
{
    public interface IEmailService
    {
        void SendPasswordResetMessage(string recipient, string token);
        void SendBlockMessage(string recipient);
    }
}