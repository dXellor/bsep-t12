using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Contracts
{
    public interface INotificationService
    {
        void Connect();
        Task SendMessage(string message);
    }
}
