using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;
using WebApiClient.Attributes;

namespace LayIM.AspNetCore.RongCloud
{
    [HttpHost("")]
    public interface IRongCloudApi : IHttpApi
    {
    }
}
