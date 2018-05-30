using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LayIM.AspNetCore.Storage.Infra.Dapper.Repository
{
    public abstract class BaseRepository
    {
        private readonly DBConnectionConfig dbConfig;

        public BaseRepository(DBConnectionConfig dbConfig)
        {
            this.dbConfig = dbConfig;
        }

        protected abstract string TableName { get; }
        protected virtual string KeyColumn => "id";

        protected virtual Task<TResult> GetById<TKey,TResult>(TKey key)
        {
            string sql = $"SELECT * FROM {TableName} WHERE [{KeyColumn}]=@keyId";
            return QuerySingleAsync<TResult>(sql, new { keyId = key });
        }


        #region protected

        protected Task<TResult> QuerySingleAsync<TResult>(string sql, object param = null)
            => Execute(connection => connection.QueryFirstOrDefaultAsync<TResult>(BuildCommand(sql, param)));

        protected Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null)
            => Execute(connection => connection.QueryAsync<TResult>(BuildCommand(sql, param)));

        protected Task<int> ExecuteSqlAsync<TResult>(string sql, object param = null)
            => Execute(connection => connection.ExecuteAsync(BuildCommand(sql, param)));

        protected Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null)
           => Execute(connection => connection.ExecuteScalarAsync<TResult>(BuildCommand(sql, param)));
        #endregion

        #region private
        private CommandDefinition BuildCommand(string sql, object param = null)
        {
            return new CommandDefinition(sql, param);
        }

        private IDbConnection GetConnection()
        {
            switch (dbConfig.DbType)
            {
                case DBType.MySql:

                    return new MySqlConnection(dbConfig.ConnectionString);
                case DBType.SqlServer:
                    return new SqlConnection(dbConfig.ConnectionString);
            }
            throw new NotSupportedException("invalid DbType");
        }

        private async Task<TResult> Execute<TResult>(Func<IDbConnection, Task<TResult>> func)
        {
            using (IDbConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    return await func(connection);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
