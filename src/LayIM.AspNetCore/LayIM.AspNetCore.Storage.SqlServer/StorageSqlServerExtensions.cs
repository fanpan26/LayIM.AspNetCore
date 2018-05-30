using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Storage;
using LayIM.AspNetCore.Storage.Infra.Dapper;
using LayIM.AspNetCore.Storage.Infra.Dapper.Repository;
using System;
using System.Collections.Generic;
using System.Text;


namespace Microsoft.Extensions.DependencyInjection
{

    public static class StorageSqlServerExtensions
    {
        /// <summary>
        /// 使用融云通信
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setConfig"></param>
        public static IServiceCollection AddSqlServer(this IServiceCollection services,string connectionString)
        {
            var dbConfig = new DBConnectionConfig(DBType.SqlServer);
            dbConfig.ConnectionString = connectionString;

            services.AddSingleton(dbConfig);

            services.AddSingleton<ILayIMStorage, LayIMDapperStorage>();

            RegisterRepositories(services);

            return services;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<BigGroupRepository>();
            services.AddSingleton<FriendGroupRepository>();
            services.AddSingleton<FriendRelationRepository>();
            services.AddSingleton<GroupMemberRepository>();
            services.AddSingleton<UserRepository>();
        }

    }
}

