using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Repository
{
    public class GroupMemberRepository : BaseRepository
    {
        public GroupMemberRepository(DBConnectionConfig dbConfig) : base(dbConfig)
        { }


        protected override string TableName => "layim_group_member";

        public Task<IEnumerable<long>> GetUserBigGroups(string userId)
        {
            var sql = "SELECT [group_id] FROM layim_group_member where member_id=@uid";
            return QueryAsync<long>(sql, new { uid = userId });
        }
    }
}
