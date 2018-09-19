using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.Storage.Infra.Dapper
{
    public enum DBType
    {
        SqlServer = 1,
        MySql = 2
    }
    public class DBConnectionConfig
    {
        public DBConnectionConfig(DBType dbType)
        {
            DbType = dbType;
        }
        internal DBType DbType { get; }
        public string ConnectionString { get; set; }
    }
}
