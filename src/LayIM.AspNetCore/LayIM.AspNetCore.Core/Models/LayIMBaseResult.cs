using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Core.Models
{
    public abstract class LayIMBaseResult
    {
        public int code { get; set; }
        public string msg { get; set; }
    }

    public abstract class LayIMBaseResult<T> : LayIMBaseResult
    {
        public T data { get; set; }
    }

    public class LayIMCommonResult : LayIMBaseResult<object>
    {

    }
}
