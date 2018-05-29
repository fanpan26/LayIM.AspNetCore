using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core
{
    internal sealed class Error
    {
        public static void ThrowIfNull(object obj,string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
