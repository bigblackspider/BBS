using System.Linq;
using BBS.Core.BO;
using BBS.Core.Models;
using BBS.Core.Tests.TestData;
using NUnit.Framework;

namespace BBS.Core.Tests.UnitTests
{
    [TestFixture]
    internal class WebsiteManageTests

    {
        [SetUp]
        public void Init()
        {
            var ctx = new BBSEntitiesMock();
            _wm = new WebsiteManage(ctx);
        }

        private WebsiteManage _wm;

        private static void ValidateWebsite(Customer customer, int id, string status, Website website)
        {
            //********** validate Website
            Assert.AreEqual(id, website.WebsiteId);
            Assert.AreEqual($"test.{customer.CustomerId}.{id}@domainname.com", website.DomainName);
            Assert.NotNull(website.DateCreated);
            Assert.GreaterOrEqual(website.DateUpdated, website.DateCreated);
            Assert.AreEqual(website.DateCreated.AddDays(10), website.ExpiryDate);
            Assert.AreEqual(status, website.Status);
            Assert.IsNotEmpty(website.Notes);
            Assert.AreSame(customer, website.Customer);
            Assert.AreEqual(customer.CustomerId, website.CustomerId);

            //********** Validate Website Items
            foreach (var websiteItem in website.WebsiteItems)
            {
                Assert.IsNotEmpty(websiteItem.ItemName);
                Assert.IsNotEmpty(websiteItem.ItemName);
                Assert.AreSame(website, websiteItem.Website);
                Assert.AreEqual(website.WebsiteId, websiteItem.WebsiteId);
            }
        }


        [Test]
        public void ListWebsites()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadWebsites(ctx);

            //********** List All
            var lis = _wm.GetWebsites();
            Assert.AreEqual(15, lis.Count);
            var customer = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            var id = 0;
            foreach (var website in lis.Where(o => o.CustomerId == 1))
                ValidateWebsite(customer, id++, "Inactive", website);
            customer = ctx.Customers.FirstOrDefault(o => o.CustomerId == 2);
            id = 0;
            foreach (var website in lis.Where(o => o.CustomerId == 2))
                ValidateWebsite(customer, id++, "Inactive", website);

            //********** List All Range
            lis = _wm.GetWebsites(2, 3);
            Assert.AreEqual(3, lis.Count);

            //********** List Customer Websites
            lis = _wm.GetCustomerWebsites(1);
            Assert.AreEqual(5, lis.Count);
            customer = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            id = 0;
            foreach (var website in lis)
                ValidateWebsite(customer, id++, "Inactive", website);

            //********** List Customer Range
            lis = _wm.GetWebsites(2, 3);

            Assert.AreEqual(3, lis.Count);
        }
    }
}