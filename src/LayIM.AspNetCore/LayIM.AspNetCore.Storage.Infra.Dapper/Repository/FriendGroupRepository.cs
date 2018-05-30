using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Repository
{
    public class FriendGroupRepository : BaseRepository
    {
        public FriendGroupRepository(DBConnectionConfig dbConfig) : base(dbConfig)
        { }

        protected override string TableName => "layim_friend_group";


        public Task<IEnumerable<FriendGroupModel>> GetUserGroups(string userId)
        {
            var sql = "SELECT [id],[name] as groupname FROM layim_friend_group WHERE create_by=@uid";
            return QueryAsync<FriendGroupModel>(sql, new { uid = userId });
        }
    }
}
