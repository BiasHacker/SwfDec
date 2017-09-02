using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwfDec.AVM2.Types.Extended
{
    public interface IBrowseable
    {
        object this[String name] { get; set; }
    }
}
