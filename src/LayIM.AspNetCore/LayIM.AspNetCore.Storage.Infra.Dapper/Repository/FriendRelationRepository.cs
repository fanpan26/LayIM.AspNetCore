using LayIM.AspNetCore.Storage.Infra.Dapper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Repository
{
    public class FriendRelationRepository : BaseRepository
    {
        public FriendRelationRepository(DBConnectionConfig dbConfig) : base(dbConfig)
        { }

        protected override string TableName => "layim_friend_relation";

        public Task<IEnumerable<FriendRelationShip>> GetFriendRelations(string userId)
        {
            var sql = @"select uid1 as userId,friend_group_1 as groupId,uid2 as friendId from layim_friend_relation where uid1=@uid and friend_group_1 >0
  union all select uid2 as userId,friend_group_2 as groupId,uid1 as friendId from layim_friend_relation where uid2 = @uid and friend_group_2 > 0";
            return QueryAsync<FriendRelationShip>(sql, new { uid = userId });
        }
    }
}
