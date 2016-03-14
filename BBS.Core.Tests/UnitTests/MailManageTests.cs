using System;
using System.Linq;
using BBS.Core.BO;
using BBS.Core.Extenders;
using BBS.Core.Lib;
using BBS.Core.Models;
using BBS.Core.Tests.TestData;
using NUnit.Framework;

namespace BBS.Core.Tests.UnitTests
{
    [TestFixture]
    internal class MailManageTests

    {
        [SetUp]
        public void Init()
        {
            var ctx = new BBSEntitiesMock();
            _mm = new MailBoxManage(ctx);
        }

        private MailBoxManage _mm;

        private static void ValidateMailBox(int id, string status, MailBox mailBox)
        {
            Assert.AreEqual(id, mailBox.MailBoxId);
            Assert.AreEqual($"DomainName{id}.com", mailBox.DomainName);
            Assert.AreEqual(9999, mailBox.MaxMessageSizeKB);
            Assert.AreEqual(100, mailBox.MaxAccountSizeMB);
            Assert.AreEqual(10, mailBox.MaxAccounts);
            Assert.AreEqual($"catchall@DomainName{id}.com", mailBox.CatchAllAddress);
            Assert.NotNull(mailBox.DateCreated);
            Assert.GreaterOrEqual(mailBox.DateUpdated, mailBox.DateCreated);
            Assert.AreEqual(mailBox.DateCreated.AddDays(10), mailBox.ExpiryDate);
            Assert.AreEqual(status, mailBox.Status);
            var childId = 0;
            foreach (var ma in mailBox.MailAccounts)
                ValidateMailAccount(childId++, status, mailBox, ma);
            childId = 0;
            foreach (var ma in mailBox.MailAliasNames)
                ValidateMailAlias(childId++, mailBox, ma);
        }

        private static void ValidateMailAccount(int id, string status, MailBox mailBox, MailAccount ma)
        {
            Assert.AreEqual(id, ma.MailAccountId);
            Assert.AreEqual($"AccountName_{id}", ma.AccountName);
            Assert.AreEqual($"forward.{mailBox.MailBoxId}.{id}@{mailBox.DomainName}", ma.Forwarding);
            Assert.AreEqual(5, ma.MaxSizeMB);
            Assert.NotNull(ma.DateCreated);
            Assert.GreaterOrEqual(ma.DateUpdated, mailBox.DateCreated);
            Assert.AreEqual(status, ma.Status);
            Assert.AreEqual(mailBox.MailBoxId, ma.MailBoxId);
            Assert.AreSame(mailBox, ma.MailBox);
        }

        private static void ValidateMailAlias(int id, MailBox mailBox, MailAliasName ma)
        {
            Assert.AreEqual(id, ma.MailAliasNameId);
            Assert.AreEqual($"AlaisName_{id}", ma.AliasName);
            Assert.AreEqual(mailBox.MailBoxId, ma.MailBoxId);
            Assert.AreSame(mailBox, ma.MailBox);
        }

        [Test]
        public void CreateMailBox()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadMailboxes(ctx);
            var customer = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            Assert.NotNull(customer);

            //********** Insert Mail Box 
            var mailbox = TestDataLoad.CreateMailbox(customer, 11);
            mailbox.MailBoxId = -1;
            mailbox = _mm.CreateMailbox(mailbox);
            Assert.NotNull(mailbox);
            var lis = _mm.GetCustomerMailBoxs(customer.CustomerId);
            Assert.AreEqual(11, lis.Count);
            ValidateMailBox(mailbox.MailBoxId, "Inactive", mailbox);

            //********** Reject insert existing domain name
            lis = _mm.GetCustomerMailBoxs(customer.CustomerId);
            mailbox = TestDataLoad.CreateMailbox(customer, 11);
            mailbox.MailBoxId = -1;
            mailbox.DomainName = lis[2].DomainName;
            try
            {
                _mm.CreateMailbox(mailbox);
                Assert.Fail("Created duplicate entry.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILBOX_DOMAIN_EXISTS.Fmt(mailbox.DomainName), ex.Message);
            }

            //********** Reject invalid domain name
            mailbox = TestDataLoad.CreateMailbox(customer, 11);
            mailbox.MailBoxId = -1;
            mailbox.DomainName = "xxxx";
            try
            {
                _mm.CreateMailbox(mailbox);
                Assert.Fail("Created a invalid mail box.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILBOX_DOMAIN.Fmt(mailbox.DomainName), ex.Message);
            }

            //********** Reject invalid catch all address
            mailbox = TestDataLoad.CreateMailbox(customer, 11);
            mailbox.MailBoxId = -1;
            mailbox.CatchAllAddress = "xxxx";
            try
            {
                _mm.CreateMailbox(mailbox);
                Assert.Fail("Created a invalid mail box.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILBOX_CATCHALL.Fmt(mailbox.DomainName), ex.Message);
            }

            //********** Reject invalid expiry date
            mailbox = TestDataLoad.CreateMailbox(customer, 11);
            mailbox.MailBoxId = -1;
            mailbox.ExpiryDate = mailbox.DateCreated.AddDays(-1);
            try
            {
                _mm.CreateMailbox(mailbox);
                Assert.Fail("Created a invalid mail box.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILBOX_EXPIRYDATE.Fmt(mailbox.DomainName), ex.Message);
            }
        }

        [Test]
        public void CreateMailAccounts()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadMailboxes(ctx);
            var cust = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            Assert.NotNull(cust);
            var mail = _mm.GetMailbox(10);
            mail.MaxAccounts = 11;
            mail = _mm.UpdateMailbox(mail);

            //********** Create New Account
            var acc = new MailAccount
            {
                MailAccountId = 11,
                MailBoxId = mail.MailBoxId,
                AccountName = "AccountName_11",
                Forwarding = $"forward.{mail.MailBoxId}.11@{mail.DomainName}"
            };
            acc = _mm.CreateMailAccount(acc);
            ValidateMailAccount(acc.MailAccountId, "Inactive", mail, acc);

            //********** Reject Max accounts reached
            acc = new MailAccount
            {
                MailAccountId = 12,
                MailBoxId = mail.MailBoxId,
                AccountName = "AccountName_12",
                Forwarding = $"forward.{mail.MailBoxId}.12@{mail.DomainName}"
            };
            try
            {
                _mm.CreateMailAccount(acc);
                Assert.Fail("Created a mail account when max accounts has been reached.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILACCOUNT_MAXREACHED, ex.Message);
            }

            //********** Reload new mail box
            mail = _mm.GetMailbox(9);
            mail.MaxAccounts = 11;
            mail = _mm.UpdateMailbox(mail);

            //********** Reject invalid account name
            acc = new MailAccount
            {
                MailAccountId = 11,
                MailBoxId = mail.MailBoxId,
                AccountName = "!@#$",
                Forwarding = $"forward.{mail.MailBoxId}.11@{mail.DomainName}"
            };
            try
            {
                _mm.CreateMailAccount(acc);
                Assert.Fail("Created a mail account with invalid values.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILACCOUNT_ACCOUNTNAME, ex.Message);
            }
        }

        [Test]
        public void UpdateMailBox()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadMailboxes(ctx);
            var cust = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            Assert.NotNull(cust);

            //********** Test Mail Box expiry
            var mail = _mm.GetMailbox(10);
            mail.ExpiryDate = DateTime.UtcNow.AddSeconds(-1);
            Assert.AreEqual("Expired", mail.Status);

            //********** Reject ativation on expired mail box
            try
            {
                _mm.ActivateMailbox(mail.MailBoxId);
                Assert.Fail("Acticated an expired mail box.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILBOX_ACTIVATE.Fmt(mail.MailBoxId), ex.Message);
            }

            //********** ActivateCustomer Mail Box
            mail = _mm.GetMailbox(9);
            var dateUpdated = mail.DateUpdated;
            _mm.ActivateMailbox(mail.MailBoxId);
            Assert.IsTrue(_mm.GetCustomerMailBoxs(cust.CustomerId)
                .Any(o => o.MailBoxId == 9 && o.Status == "Active" && o.DateUpdated > dateUpdated));

            //********** Update Mail Box
            mail = _mm.GetMailbox(8);
        }

        [Test]
        public void ReadCustomerMailboxes()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadMailboxes(ctx);
            var cust = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            Assert.NotNull(cust);

            //********** List All Mailboxes
            var mailBoxes = _mm.GetCustomerMailBoxs(cust.CustomerId);
            Assert.AreEqual(10, mailBoxes.Count);
            foreach (var mail in mailBoxes)
                ValidateMailBox(mail.MailBoxId, "Inactive", mail);

            //********** Test list Range
            mailBoxes = _mm.GetCustomerMailBoxs(cust.CustomerId, 10, 20);
            Assert.AreEqual(10, mailBoxes.Count);
            foreach (var mail in mailBoxes)
                ValidateMailBox(mail.MailBoxId, "Inactive", mail);

            //********** Get a Mailbox
            var mailBox = _mm.GetMailbox(9);
            Assert.NotNull(mailBox);
            ValidateMailBox(mailBox.MailBoxId, "Inactive", mailBox);

            //********** List All Mail Accounts for MailBox
            var mailAccounts = _mm.GetMailAccounts(mailBox.MailBoxId);
            Assert.AreEqual(5, mailAccounts.Count);
            foreach (var mailAccount in mailAccounts)
                ValidateMailAccount(mailAccount.MailAccountId, "Inactive", mailBox, mailAccount);

            //********** List All Mail Accounts for MailBox
            var mailAliasNames = _mm.GetMailAliasNames(mailBox.MailBoxId);
            Assert.AreEqual(5, mailAliasNames.Count);
            foreach (var mailAliasName in mailAliasNames)
                ValidateMailAlias(mailAliasName.MailAliasNameId, mailBox, mailAliasName);

            //********** Reject Mail Box Missing
            try
            {
                _mm.GetMailbox(989); ;
                Assert.Fail("Managed to read a mail box that dont exist.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILBOX_MISSING.Fmt(99), ex.Message);
            }
        }

        [Test]
        public void ReadMailAccounts()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadMailboxes(ctx);
            var cust = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            Assert.NotNull(cust);
            var mailBox = _mm.GetMailbox(9);
            Assert.NotNull(mailBox);

            //********** List All Accounts
            var accounts = _mm.GetMailAccounts(mailBox.MailBoxId);
            Assert.AreEqual(10, accounts.Count);
            foreach (var acc in accounts)
                ValidateMailAccount(acc.MailAccountId, "Inactive", mailBox, acc);

            //********** Test list Range
            accounts = _mm.GetMailAccounts(mailBox.MailBoxId, 2, 6);
            Assert.AreEqual(6, accounts.Count);
            foreach (var acc in accounts)
                ValidateMailAccount(acc.MailAccountId, "Inactive", mailBox, acc);

            //********** Get a Account
            var account = _mm.GetMailAccount(3);
            Assert.NotNull(account);
            ValidateMailAccount(account.MailAccountId, "Inactive", mailBox, account);

            //********** Reject Account Missing
            try
            {
                _mm.GetMailAccount(99);
                Assert.Fail("Managed to read a mail account that dont exist.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILACCOUNT_MISSING.Fmt(99), ex.Message);
            }


        }

        [Test]
        public void ReadMailAliasNames()
        {
            //********** Load Test data
            var ctx = new BBSEntitiesMock();
            TestDataLoad.LoadMailboxes(ctx);
            var cust = ctx.Customers.FirstOrDefault(o => o.CustomerId == 1);
            Assert.NotNull(cust);
            var mailBox = _mm.GetMailbox(7);
            Assert.NotNull(mailBox);

            //********** List All Alias Names
            var aliasNames = _mm.GetMailAliasNames(mailBox.MailBoxId);
            Assert.AreEqual(5, aliasNames.Count);
            foreach (var an in aliasNames)
                ValidateMailAlias(an.MailAliasNameId, mailBox, an);

            //********** Test list Range
            aliasNames = _mm.GetMailAliasNames(mailBox.MailBoxId, 2, 6);
            Assert.AreEqual(6, aliasNames.Count);
            foreach (var an in aliasNames)
                ValidateMailAlias(an.MailAliasNameId,  mailBox, an);

            //********** Get a Alias Name
            var aliasName = _mm.GetMailAliasName(3);
            Assert.NotNull(aliasName);
            ValidateMailAlias(aliasName.MailAliasNameId, mailBox, aliasName);

            //********** Reject Alias Name Missing
            try
            {
                _mm.GetMailAliasName(88);
                Assert.Fail("Managed to read a mail alias name account that dont exist.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ErrorMsg.ERR_MAILALIAS_MISSING.Fmt(99), ex.Message);
            }
        }
    }
}