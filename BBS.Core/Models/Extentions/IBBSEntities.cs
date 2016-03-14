using System;
using System.Data.Entity;

namespace BBS.Core.Models.Extentions
{
    public interface IBBSEntities : IDisposable
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerPreoduct> CustomerPreoducts { get; set; }
        DbSet<MailAccount> MailAccounts { get; set; }
        DbSet<MailAliasName> MailAliasNames { get; set; }
        DbSet<MailBox> MailBoxes { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<WebsiteItem> WebsiteItems { get; set; }
        DbSet<Website> Websites { get; set; }
        int SaveChanges();
        void MarkAsModified(Website item);
    }
}