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

namespace LayIM.AspNetCore.RongCloud
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
           
            int rd_i = randoms.Value.Next();

            string nonce = rd_i.ToString();
            string timestamp = DateTime.Now.ToTimestamp().ToString();
            string signature = GetHash(appInfo.AppSecret + nonce + timestamp).ToLowerInvariant();
            //appkey
            context.RequestMessage.Headers.Add("App-Key", appInfo.AppKey);
            //随机字符串
            context.RequestMessage.Headers.Add("Nonce", nonce);
            //时间戳
            context.RequestMessage.Headers.Add("Timestamp", timestamp);
            //根据字符串获取的签名
            context.RequestMessage.Headers.Add("Signature", signature);
            return Task.CompletedTask;
        }

        public Task OnEndRequestAsync(ApiActionContext context)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 将字符串进行sha1加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetHash(string input)
        {
            //建立SHA1对象
            SHA1 sha = new SHA1CryptoServiceProvider();

            //将mystr转换成byte[]
            UTF8Encoding enc = new UTF8Encoding();
            byte[] dataToHash = enc.GetBytes(input);

            //Hash运算
            byte[] dataHashed = sha.ComputeHash(dataToHash);

            //将运算结果转换成string
            string hash = BitConverter.ToString(dataHashed).Replace("-", "");

            return hash;
        }
    }
}
