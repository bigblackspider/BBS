using System.Data.Entity;
using BBS.Core.Models;
using BBS.Core.Models.Extentions;

namespace BBS.Core.Tests.TestData
{
    public class BBSEntitiesMock : IBBSEntities
    {
        public BBSEntitiesMock()
        {
            Websites = new WebsiteDbSetMock();
        }


        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerPreoduct> CustomerPreoducts { get; set; }
        public virtual DbSet<MailAccount> MailAccounts { get; set; }
        public virtual DbSet<MailAliasName> MailAliasNames { get; set; }
        public virtual DbSet<MailBox> MailBoxes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<WebsiteItem> WebsiteItems { get; set; }
        public virtual DbSet<Website> Websites { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Website item)
        {
        }

        public void Dispose()
        {
        }
    }
}