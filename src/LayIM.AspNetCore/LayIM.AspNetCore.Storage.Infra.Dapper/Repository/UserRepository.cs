using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Repository
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DBConnectionConfig dbConfig) : base(dbConfig)
        { }


        protected override string TableName => "layim_user";
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<MineUserModel> GetUserById(string userId)
        {
            var sql = "SELECT [id],[name] as username,[avatar],[sign] FROM layim_user where id=@uid";
            return QuerySingleAsync<MineUserModel>(sql, new { uid = userId });
        }

        public Task<IEnumerable<UserModel>> GetUsersByIds(IEnumerable<long> userIds)
        {
            if (userIds?.Count() > 0)
            {
                var strs = string.Join(",", userIds);
                var sql = $"SELECT [id],[name] as username,[avatar],[sign] FROM layim_user where id in ({strs})";
                return QueryAsync<UserModel>(sql);
            }
            return Task.FromResult(LayIMNoData.NoUser);
        }
    }
}
