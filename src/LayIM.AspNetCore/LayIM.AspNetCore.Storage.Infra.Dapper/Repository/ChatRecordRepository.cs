using LayIM.AspNetCore.Core.Models.Base;
using LayIM.AspNetCore.Storage.Infra.Dapper.Models;
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

        public Task<IEnumerable<ChatRecord>> GetList(string roomId, int page, long fromTimestamp)
        {
            var where = fromTimestamp == 0 ? "" : "and addtime < " + fromTimestamp;
            var sql = $"SELECT top {page} from_id as FromId,[msg] as Msg,addtime as AddTime FROM layim_chat_record where room_id=@roomid {where} order by Id desc";
            return QueryAsync<ChatRecord>(sql, new { roomid = roomId, page = page });
        }

    }
}
