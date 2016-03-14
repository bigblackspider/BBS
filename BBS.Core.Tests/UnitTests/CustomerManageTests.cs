using System;
using System.Linq;
using BBS.Core.BO;
using BBS.Core.Extenders;
using BBS.Core.Lib;
using BBS.Core.Tests.TestData;
using NUnit.Framework;

namespace BBS.Core.Tests.UnitTests
{
    [TestFixture]
    internal class CustomerManageTests


    {
        [SetUp]
        public void Init()
        {
            var ctx = new BBSEntitiesMock();
            _cm = new CustomerManage(ctx);
        }

        
        private CustomerManage _cm;

        private static void ValidateCustomer(int id, string status, Models.Customer cust)
        {
            Assert.AreEqual($"FirstName_{id}", cust.FirstName);
            Assert.AreEqual($"MiddleNames_{id}", cust.MiddleNames);
            Assert.AreEqual($"FamilyName_{id}", cust.FamilyName);
            Assert.IsTrue(cust.Phone.IsValidPhoneNo());
            Assert.IsTrue(cust.Mobile.IsValidPhoneNo());
            Assert.IsTrue(cust.Fax.IsValidPhoneNo());
            Assert.IsTrue(cust.EmailAddress.IsValidEmail());
            Assert.IsTrue(cust.CustomerWebsite.IsValidUrl());
            Assert.AreEqual(status, cust.Status);
            Assert.Greater(DateTime.UtcNow, cust.DateCreated);
            Assert.GreaterOrEqual(cust.DateUpdated, cust.DateCreated);
        }

        [Test]
        public void CreateCustomerDeails()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadCustomers(ctx);

            //********** Insert Customer
            var cust = TestDataLoad.CreateCustomer(101);
            cust.CustomerId = -1;
            cust = _cm.CreateCustomer(cust);
            Assert.NotNull(cust);
            var lis = _cm.GetCustomers();
            Assert.AreEqual(101, lis.Count);
            foreach (var c in lis)
                ValidateCustomer(c.CustomerId, "Unregistered", c);

            //********** Reject insert existing email address
            lis = _cm.GetCustomers();
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.EmailAddress = lis[67].EmailAddress;
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created duplicate entry.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_EMAIL_EXISTS.Fmt(cust.EmailAddress), ex.Message);
            }

            //********** Reject insert invalid phone number 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.Phone = "xxxxxxxx";
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with invalid phone number.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_PHONENO.Fmt(cust.Phone), ex.Message);
            }

            //********** Reject insert invalid mobile number 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.Mobile = "xxxxxxxx";
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with invalid mobile number.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_MOBILE.Fmt(cust.Mobile), ex.Message);
            }

            //********** Reject insert invalid fax number 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.Fax = "xxxxxxxx";
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with invalid fax number.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_FAX.Fmt(cust.Fax), ex.Message);
            }

            //********** Reject insert invalid Email address 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.EmailAddress = "INVALID";
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with invalid Email address.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_EMAIL.Fmt(cust.EmailAddress), ex.Message);
            }

            //********** Reject insert invalid customer website 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.CustomerWebsite = "INVALID";
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with invalid customer website address.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_WEBSITE.Fmt(cust.CustomerWebsite), ex.Message);
            }

            //********** Allow blank values
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.MiddleNames = string.Empty;
            cust.CustomerWebsite = string.Empty;
            cust.Mobile = string.Empty;
            cust.Fax = string.Empty;
            cust = _cm.CreateCustomer(cust);
            Assert.NotNull(cust);

            //********** Reject insert first name missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.FirstName = string.Empty;
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_FIRSTNAME_BLANK, ex.Message);
            }

            //********** Reject insert family name missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.FamilyName = string.Empty;
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_FAMILYNAME_BLANK, ex.Message);
            }

            //********** Reject insert phone number is missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.Phone = string.Empty;
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_PHONENO_BLANK, ex.Message);
            }

            //********** Reject insert Email address is missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.EmailAddress = string.Empty;
            try
            {
                _cm.CreateCustomer(cust);
                Assert.Fail("Created with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_EMAIL_BLANK, ex.Message);
            }
        }

        [Test]
        public void DeleteArchive()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadCustomers(ctx);

            //********** Basic Delete
            _cm.DeleteCustomer(52);
            Assert.IsTrue(_cm.GetCustomers().Any(o => o.CustomerId == 52 && o.Status == "Delete"));
            try
            {
                _cm.DeleteCustomer(9999);
                Assert.Fail("Deleted a non existant customer.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_MISSING.Fmt(9999), ex.Message);
            }

            //********** Basic Undelete
            _cm.UndeleteCustomer(52);
            Assert.IsFalse(_cm.GetCustomers().Any(o => o.CustomerId == 52 && o.Status == "Delete"));
            try
            {
                _cm.UndeleteCustomer(9999);
                Assert.Fail("Undeleted a non existant customer.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_MISSING.Fmt(9999), ex.Message);
            }

            //********** Archive 10 customers
            var oldCust = _cm.GetCustomer(12);
            for (var i = 10; i < 20; i++)
                _cm.DeleteCustomer(i);
            var cnt = _cm.ArchiveCustomers();
            Assert.AreEqual(10, cnt);
            Assert.AreEqual(90, _cm.GetCustomers().Count);

            //********** Restore Customer
            var cust = _cm.RestoreCustomer(12);
            Assert.AreEqual(91, _cm.GetCustomers().Count);
            Assert.AreEqual(oldCust.FirstName, cust.FirstName);
            Assert.AreEqual(oldCust.MiddleNames, cust.MiddleNames);
            Assert.AreEqual(oldCust.FamilyName, cust.FamilyName);
            Assert.AreEqual(oldCust.Phone, cust.Phone);
            Assert.AreEqual(oldCust.Mobile, cust.Mobile);
            Assert.AreEqual(oldCust.Fax, cust.Fax);
            Assert.AreEqual(oldCust.EmailAddress, cust.EmailAddress);
            Assert.AreEqual(oldCust.CustomerWebsite, cust.CustomerWebsite);
            Assert.AreEqual(oldCust.DateCreated, cust.DateCreated);
            Assert.AreNotEqual(oldCust.DateUpdated, cust.DateUpdated);
            Assert.AreEqual("Deactive", cust.Status);
        }

        [Test]
        public void ReadCustomers()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadCustomers(ctx);

            //********** Test list all
            var lis = _cm.GetCustomers();
            Assert.AreEqual(100, lis.Count);
            foreach (var c in lis)
                ValidateCustomer(c.CustomerId, "Unregistered", c);

            //********** Test list Range
            lis = _cm.GetCustomers(10, 20);
            Assert.AreEqual(10, lis.Count);
            foreach (var c in lis)
                ValidateCustomer(c.CustomerId, "Unregistered", c);

            //********** Get a customer
            var cust = _cm.GetCustomer(89);
            Assert.NotNull(cust);
            ValidateCustomer(89, "Unregistered", cust);
        }

        [Test]
        public void UpdateCustomer()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadCustomers(ctx);

            //********** Update Customer
            var cust = TestDataLoad.CreateCustomer(101);
            cust.CustomerId = 66;
            cust = _cm.UpdateCustomer(cust);
            Assert.NotNull(cust);
            ValidateCustomer(66, "Unregistered", cust);
            Assert.Greater(cust.DateUpdated, cust.DateCreated);

            //********** Reject update missing CustomerId
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = 9999;
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated a missing entry.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_MISSING.Fmt(cust.CustomerId), ex.Message);
            }

            //********** Reject update invalid phone number 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.Phone = "xxxxxxxx";
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated customer with a invalid value.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_PHONENO.Fmt(cust.Phone), ex.Message);
            }

            //********** Reject update invalid mobile number 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.Mobile = "xxxxxxxx";
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated customer with a invalid value.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_MOBILE.Fmt(cust.Mobile), ex.Message);
            }

            //********** Reject update invalid fax number 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.Fax = "xxxxxxxx";
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated customer with a invalid value.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_FAX.Fmt(cust.Fax), ex.Message);
            }

            //********** Reject update invalid Email address 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.EmailAddress = "INVALID";
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated customer with a invalid value.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_EMAIL.Fmt(cust.EmailAddress), ex.Message);
            }

            //********** Reject update invalid customer website 
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.CustomerWebsite = "INVALID";
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated customer with a invalid value.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_WEBSITE.Fmt(cust.CustomerWebsite), ex.Message);
            }

            //********** Allow blank values
            cust = TestDataLoad.CreateCustomer(102);
            cust.CustomerId = -1;
            cust.MiddleNames = string.Empty;
            cust.CustomerWebsite = string.Empty;
            cust.Mobile = string.Empty;
            cust.Fax = string.Empty;
            cust = _cm.UpdateCustomer(cust);
            Assert.NotNull(cust);

            //********** Reject update first name missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.FirstName = string.Empty;
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_FIRSTNAME_BLANK, ex.Message);
            }

            //********** Reject update family name missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.FamilyName = string.Empty;
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_FAMILYNAME_BLANK, ex.Message);
            }

            //********** Reject update phone number is missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.Phone = string.Empty;
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_PHONENO_BLANK, ex.Message);
            }

            //********** Reject update Email address is missing 
            cust = TestDataLoad.CreateCustomer(103);
            cust.CustomerId = -1;
            cust.EmailAddress = string.Empty;
            try
            {
                _cm.UpdateCustomer(cust);
                Assert.Fail("Updated with fields missing.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_EMAIL_BLANK, ex.Message);
            }
        }

        [Test]
        public void VariousCustomerFunctions()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadCustomers(ctx);

            //********** ActivateCustomer Customer
            _cm.ActivateCustomer(66);
            var lis = _cm.GetCustomers();
            Assert.IsTrue(lis.Any(o => o.CustomerId == 66 && o.Status == "Active" && o.DateUpdated > o.DateCreated));

            //********** Reject activate if customer is missing 
            try
            {
                _cm.ActivateCustomer(9999);
                Assert.Fail("Activated a non existant customer.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_MISSING.Fmt(9999), ex.Message);
            }

            //********** DeactivateCustomer Customer
            _cm.DeactivateCustomer(66);
            lis = _cm.GetCustomers();
            Assert.IsTrue(lis.Any(o => o.CustomerId == 66 && o.Status == "Deactive" && o.DateUpdated > o.DateCreated));

            //********** Reject deactivate if customer is missing 
            try
            {
                _cm.DeactivateCustomer(9999);
                Assert.Fail("Deactivated a non existant customer.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_MISSING.Fmt(9999), ex.Message);
            }

            //********** Test email exists, dont exist, invalid email addrtess
            lis = _cm.GetCustomers();
            Assert.IsTrue(_cm.DoesEmailExist(lis[12].EmailAddress));
            Assert.IsFalse(_cm.DoesEmailExist(@"dontexist@bigblackspider.com"));
            try
            {
                _cm.DoesEmailExist("xxxxxx");
                Assert.Fail("Found a invalid Email address.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_EMAIL.Fmt("xxxxx"), ex.Message);
            }
            try
            {
                _cm.DoesEmailExist(string.Empty);
                Assert.Fail("Found a empty Email address.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_CUSTOMER_EMAIL_BLANK, ex.Message);
            }
        }
    }
}