using LayIM.AspNetCore.Middleware;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LayIM.AspNetCore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ThrowIfNullTests()
        {
            try
            {
                string userName = null;
                Error.ThrowIfNull(null, nameof(userName));
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(typeof(ArgumentNullException), ex.GetType());
            }
        }
    }
}
