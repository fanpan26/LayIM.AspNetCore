using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Storage.Infra.Dapper
{
    internal class LayIMNoData
    {
        public static readonly IEnumerable<UserModel> NoUser = new List<UserModel>();
    }
}
