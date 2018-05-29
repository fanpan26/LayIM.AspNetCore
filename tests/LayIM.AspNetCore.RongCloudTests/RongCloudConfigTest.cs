using LayIM.AspNetCore.RongCloud;
using System;
using Xunit;

namespace LayIM.AspNetCore.RongCloudTests
{
    public class RongCloudConfigTest
    {
        [Fact]
        public void RongCloudSetAppKeyTest()
        {
            try
            {
                var appKey = "";

                var config = new RongCloudConfig();
                config.AppKey = appKey;
            }
            catch (Exception ex)
            {
                Assert.Equal(ex.Message, "rongcloud appkey must not be null or empty");
            }
        }


        [Fact]
        public void RongCloudSetAppSecretTest()
        {
            try
            {
                var appSecret = "";

                var config = new RongCloudConfig();
                config.AppSecret = appSecret;
            }
            catch (Exception ex)
            {
                Assert.Equal(ex.Message, "rongcloud appsecret must not be null or empty");
            }
        }
    }
}
