using LayIM.AspNetCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;
using WebApiClient.Attributes;

namespace LayIM.AspNetCore.RongCloud
{
    [HttpHost("https://api.cn.ronghub.com/")]
    public interface IRongCloudApi : IHttpApi
    {
        [HttpPost("/user/getToken.json")]
        ITask<TokenResult> GetToken([FormField]string userId);
    }
}
