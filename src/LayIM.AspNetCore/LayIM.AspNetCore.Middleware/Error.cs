using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Middleware
{
    public sealed class Error
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
