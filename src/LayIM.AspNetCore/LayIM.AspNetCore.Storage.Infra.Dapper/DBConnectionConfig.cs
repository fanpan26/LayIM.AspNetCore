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
        private DBType dbType;
        public DBConnectionConfig(DBType dbType)
        {
            this.dbType = dbType;
        }
        internal DBType DbType => dbType;
        public string ConnectionString { get; set; }
    }
}
