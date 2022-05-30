using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Common
{
    internal class BTL_KTPMException:Exception
{
    public BTL_KTPMException()
    {
    }

    public BTL_KTPMException(string message)
        : base(message)
    {
    }

    public BTL_KTPMException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
}
