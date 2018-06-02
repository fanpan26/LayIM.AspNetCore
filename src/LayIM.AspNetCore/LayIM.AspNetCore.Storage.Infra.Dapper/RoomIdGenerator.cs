using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Storage.Infra.Dapper
{
    internal static class RoomIdGenerator
    {
        public static string RoomId(string from, string to, string type)
        {
            return type == "group" ? to : string.Compare(from, to) > 0 ? $"{to}{from}" : $"{from}{to}";
        }
    }
}
