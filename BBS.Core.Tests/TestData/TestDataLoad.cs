using System;
using BBS.Core.Models;

namespace BBS.Core.Tests.TestData
{
    internal static class TestDataLoad
    {
        private static DateTime _timestamp = DateTime.UtcNow;


        internal static void LoadCustomers(BBSEntitiesMock ctx)
        {
            for (var i = 1; i < 101; i++)
                ctx.Customers.Add(CreateCustomer(i));
        }

        internal static Customer CreateCustomer(int id)
        {
            return new Customer
            {
                CustomerId = id,
                FirstName = $"FirstName_{id}",
                MiddleNames = $"MiddleNames_{id}",
                FamilyName = $"FamilyName_{id}",
                Phone = "0711111111",
                Mobile = "0422222222",
                Fax = "0233333333",
                EmailAddress = $"customer{id}@bigblackspider.com",
                CustomerWebsite = "http://www.bigblackspider.com",
                Status = "Unregistered",
                DateCreated = _timestamp, // todo change to DateCreated
                DateUpdated = _timestamp
            };
        }

        internal static void LoadProducts(BBSEntitiesMock ctx)
        {
            //********** Create dummy Customer
            var cust = CreateCustomer(1);

            //********** Create Products
            var p1 =
                ctx.Products.Add(new Product
                {
                    ProductId = 1,
                    Name = "Web Starter",
                    Description = "Test Data",
                    DateCreated = _timestamp
                });
            var p2 =
                ctx.Products.Add(new Product
                {
                    ProductId = 2,
                    Name = "Small Buisness Web",
                    Description = "Test Data",
                    DateCreated = _timestamp
                });
            var p3 =
                ctx.Products.Add(new Product
                {
                    ProductId = 3,
                    Name = "Professional Build",
                    Description = "Test Data",
                    DateCreated = _timestamp
                });
            var p4 =
                ctx.Products.Add(new Product
                {
                    ProductId = 4,
                    Name = "Web Makeover",
                    Description = "Test Data",
                    DateCreated = _timestamp
                });
            var p5 =
                ctx.Products.Add(new Product
                {
                    ProductId = 5,
                    Name = "Web Update Service",
                    Description = "Test Data",
                    DateCreated = _timestamp
                });
            var p6 =
                ctx.Products.Add(new Product
                {
                    ProductId = 6,
                    Name = "Hosting Options",
                    Description = "Test Data",
                    DateCreated = _timestamp
                });

            //********** Add products to customer
            cust.CustomerPreoducts.Add(ctx.CustomerPreoducts.Add(new CustomerPreoduct
            {
                CustomerId = cust.CustomerId,
                Customer = cust,
                ProductId = p2.ProductId,
                Product = p2,
                DateCreated = _timestamp
            }));
            cust.CustomerPreoducts.Add(ctx.CustomerPreoducts.Add(new CustomerPreoduct
            {
                CustomerId = cust.CustomerId,
                Customer = cust,
                ProductId = p4.ProductId,
                Product = p4,
                DateCreated = _timestamp
            }));
            cust.CustomerPreoducts.Add(ctx.CustomerPreoducts.Add(new CustomerPreoduct
            {
                CustomerId = cust.CustomerId,
                Customer = cust,
                ProductId = p5.ProductId,
                Product = p5,
                DateCreated = _timestamp
            }));
        }

        internal static void LoadWebsites(BBSEntitiesMock ctx)
        {
            //********** Create dummy Customer


            //********** Create 5 Test Websites Customer 1
            var customer = CreateCustomer(1);
            for (var i = 1; i < 6; i++)
                customer.Websites.Add(CreateWebsite(customer, i));
            
            //********** Create 10 Test Websites Customer 2
            customer = CreateCustomer(2);
            for (var i = 1; i < 11; i++)
                customer.Websites.Add(CreateWebsite(customer, i));
        }

        internal static Website CreateWebsite(Customer customer, int id)
        {
            var website = new Website
            {
                WebsiteId = id,
                CustomerId = customer.CustomerId,
                Customer = customer,
                DomainName = $"test.{customer.CustomerId}.{id}@domainname.com",
                DateCreated = _timestamp,
                DateUpdated = _timestamp,
                ExpiryDate = _timestamp.AddDays(10),
                Status = "Inactive",
                Notes = "Test Data!!!"
            };
            //********** Add 10 test Web Items
            for (var i = 1; i < 11; i++)
                website.WebsiteItems.Add(CreateWebsiteItem(website, i));

            //********** Add to customer website collection 
            customer.Websites.Add(website);

            //********** Final
            return website;
        }

        internal static WebsiteItem CreateWebsiteItem(Website website, int id)
        {
            var websiteItem = new WebsiteItem
            {
                WebsiteItemId = id,
                WebsiteId = website.WebsiteId,
                Website = website,
                ItemName = $"item.{website.WebsiteId}.{id}",
                ItemValue = $"Website item for web '{website.WebsiteId}' item '{id}'."
            };
            website.WebsiteItems.Add(websiteItem);
            return websiteItem;
        }


        internal static void LoadMailboxes(BBSEntitiesMock ctx)
        {
            //********** Create dummy Customer
            var customer = CreateCustomer(1);

            //********** Create 5 Test Mailboxes
            for (var i = 1; i < 6; i++)
                customer.MailBoxes.Add(CreateMailbox(customer, i));
        }

        internal static MailBox CreateMailbox(Customer customer, int id)
        {
            //********** Create Mail Box
            var mailbox = new MailBox
            {
                MailBoxId = id,
                DomainName = $"test.{customer.CustomerId}.{id}@domainname.com",
                CustomerId = customer.CustomerId,
                Customer = customer,
                MaxMessageSizeKB = 9999,
                MaxAccountSizeMB = 100,
                MaxAccounts = 10,
                CatchAllAddress = $"catchall.{customer.CustomerId}.{id}@domainname.com",
                DateCreated = _timestamp,
                DateUpdated = _timestamp,
                ExpiryDate = _timestamp.AddDays(10),
                Status = "Inactive"
            };

            //********** Create 10 test mail accounts
            for (var i = 1; i < 11; i++)
                mailbox.MailAccounts.Add(CreateMailAccount(mailbox, i));

            //********** Create 15 test alias names
            for (var i = 1; i < 16; i++)
                mailbox.MailAliasNames.Add(CreateMailAliasName(mailbox, i));
            return mailbox;
        }

        internal static MailAccount CreateMailAccount(MailBox mailbox, int id)
        {
            return new MailAccount
            {
                MailAccountId = id,
                MailBoxId = mailbox.MailBoxId,
                MailBox = mailbox,
                AccountName = $"AccountName_{id}",
                Forwarding = $"forward.{mailbox.Customer.CustomerId}.{mailbox.MailBoxId}.{id}@domainname.com",
                MaxSizeMB = 5,
                DateCreated = _timestamp,
                DateUpdated = _timestamp,
                Status = "Inactive"
            };
        }

        internal static MailAliasName CreateMailAliasName(MailBox mailbox, int id)
        {
            return new MailAliasName
            {
                MailAliasNameId = id,
                MailBoxId = mailbox.MailBoxId,
                MailBox = mailbox,
                AliasName = $"AlaisName{id}"
            };
        }
    }
}