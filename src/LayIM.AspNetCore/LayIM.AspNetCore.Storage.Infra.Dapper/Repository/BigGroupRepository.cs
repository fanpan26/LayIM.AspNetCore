using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Repository
{
    public class BigGroupRepository : BaseRepository
    {
        protected override string TableName => "layim_big_group";

        public BigGroupRepository(DBConnectionConfig dbConfig) : base(dbConfig)
        { }

        public Task<IEnumerable<BigGroupModel>> GetBigGroups(IEnumerable<long> groupIds)
        {
            if (groupIds?.Count() > 0)
            {
                var strs = string.Join(",", groupIds);
                var sql = $"SELECT [id],[name] as groupname,[avatar] FROM layim_big_group where id in ({strs})";
                return QueryAsync<BigGroupModel>(sql);
            }
            return Task.FromResult<IEnumerable<BigGroupModel>>(new List<BigGroupModel>());
        }
    }
}
