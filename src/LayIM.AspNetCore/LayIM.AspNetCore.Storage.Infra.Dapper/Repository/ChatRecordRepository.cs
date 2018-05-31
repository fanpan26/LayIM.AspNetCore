using LayIM.AspNetCore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Repository
{
    public class ChatRecordRepository : BaseRepository
    {
        public ChatRecordRepository(DBConnectionConfig config) : base(config) {

        }
        protected override string TableName => "layim_chat_record";


        public Task<int> Add(LayIMMessageModel msg)
        {
            var sql = "insert into layim_chat_record values (@From,@To,@RoomId,@Type,@Msg,@AddTime)";

            return ExecuteSqlAsync<int>(sql, msg);
        }

    }
}
