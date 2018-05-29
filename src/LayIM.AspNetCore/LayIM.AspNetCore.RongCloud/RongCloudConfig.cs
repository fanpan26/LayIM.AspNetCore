using System;
using System.Collections.Generic;
using System.Text;

namespace LayIM.AspNetCore.RongCloud
{
    public class RongCloudConfig
    {
        private string appKey;
        private string appSecret;
        public string AppKey
        {
            get
            {
                return appKey;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("rongcloud appkey must not be null or empty");
                }
                appKey = value;
            }
        }
        public string AppSecret
        {
            get
            {
                return appSecret;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("rongcloud appsecret must not be null or empty");
                }
                appSecret = value;
            }
        }
    }
}
