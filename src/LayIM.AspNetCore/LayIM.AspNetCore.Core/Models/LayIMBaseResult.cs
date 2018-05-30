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
        public LayIMCommonResult() : this(0, null)
        {

        }
        public LayIMCommonResult(int code, string msg)
        {
            this.code = code;
            this.msg = msg;
        }

        
        public static LayIMCommonResult Error(string msg)
        {
            return new LayIMCommonResult(-1, msg);
        }
    }
}
