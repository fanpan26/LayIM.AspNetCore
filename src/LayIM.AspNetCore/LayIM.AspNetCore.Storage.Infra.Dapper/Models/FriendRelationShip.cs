using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Models
{
    public class FriendRelationShip
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public long FriendId { get; set; }
    }
}
