using LayIM.AspNetCore.Core.Application;
using LayIM.AspNetCore.Core.Extensions;
using LayIM.AspNetCore.Core.IM;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Contexts;

namespace LayIM.AspNetCore.IM.RongCloud
{
    public class ApiFilter : IApiActionFilter
    {
        private readonly ILayIMAppInfo appInfo;

        private ThreadLocal<Random> randoms = new ThreadLocal<Random>(() =>
        {
            return new Random();
        });

        public ApiFilter(ILayIMAppInfo appInfo)
        {
            this.appInfo = appInfo;
        }
        /// <summary>
        /// 构造融云请求头
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnBeginRequestAsync(ApiActionContext context)
        {
           
            int rdlNext = randoms.Value.Next();

            string nonce = rdlNext.ToString();
            string timestamp = DateTime.Now.ToTimestamp().ToString();
            string signature = GetHash(appInfo.AppSecret + nonce + timestamp).ToLowerInvariant();

            context.RequestMessage.Headers.Add("App-Key", appInfo.AppKey);
            context.RequestMessage.Headers.Add("Nonce", nonce);
            context.RequestMessage.Headers.Add("Timestamp", timestamp);
            context.RequestMessage.Headers.Add("Signature", signature);

            return Task.CompletedTask;
        }

        public Task OnEndRequestAsync(ApiActionContext context)
        {
            return Task.CompletedTask;
        }

        
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetHash(string input)
        {
            //建立 SHA1 对象
            SHA1 sha = new SHA1CryptoServiceProvider();
            var enc = new UTF8Encoding();
            var dataToHash = enc.GetBytes(input);

            var dataHashed = sha.ComputeHash(dataToHash);

            var hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;
        }
    }
}
