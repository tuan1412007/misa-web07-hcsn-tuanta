using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.Common.Entity
{
    public class KeyAttribute : Attribute
    {
        public string Key { get; set; }
        public KeyAttribute(string key)
        {
            Key = key;
        }
    }
}
