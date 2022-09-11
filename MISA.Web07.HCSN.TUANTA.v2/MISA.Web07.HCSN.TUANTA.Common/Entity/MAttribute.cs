using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.HCSN.TUANTA.Common.Entity
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName:Attribute
    {
        public string Name;
        public TableName(string name)
        {
            Name = name;
        }
    }
}
