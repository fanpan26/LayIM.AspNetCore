using LayIM.AspNetCore.IM.RongCloud;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LayIM.AspNetCore.RongCloudTests
{
    public class RongCloudServerTests
    {

        [Fact]
        public void GetTokenTest()
        {
            RongCloudServer server =
                new RongCloudServer(
                    new ApiFilter(
                        new RongCloudConfig
                        {
                            AppKey = "pvxdm17jpv1or",
                            AppSecret = "I8a4qFGzFe8"
                        }));

            var tokenResult = server.GetToken("123456");
            Assert.NotEqual(tokenResult, null);
            Assert.Equal(tokenResult.code, 200);
        }
    }
}
