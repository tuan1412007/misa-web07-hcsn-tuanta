using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.Common.Entity
{
    public class ErrorService
    {
        public string DevMsg { get; set; }
        public string UserMsg { get; set; }
        public List<string> data { get; set; }

        public ErrorService()
        {

        }

        public ErrorService(string? userMsg, string? devMsg, List<string>? data)
        {
            UserMsg = userMsg;
            DevMsg = devMsg;
            data = data;
        }
    }
}
