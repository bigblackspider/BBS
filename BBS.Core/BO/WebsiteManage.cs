using System;
using System.Collections.Generic;
using BBS.Core.Models;
using BBS.Core.Models.Extentions;

namespace BBS.Core.BO
{
    public class WebsiteManage
    {
        private readonly IBBSEntities _ctx;

        private WebsiteManage()
        {
            _ctx = new BBSEntities();
        }

        public WebsiteManage(IBBSEntities value)
        {
            _ctx = value;
        }

        public List<Website> GetWebsites()
        {
            throw new NotImplementedException();
        }

        public List<Website> GetWebsites(int startId, int count)
        {
            throw new NotImplementedException();
        }

        public List<Website> GetCustomerWebsites(int customerId)
        {
            throw new NotImplementedException();
        }

        public List<Website> GetCustomerWebsites(int customerId, int startId, int count)
        {
            throw new NotImplementedException();
        }

        public Website GetWebsite(int websiteId)
        {
            throw new NotImplementedException();
        }

        public Website CreateWebsite(Website website)
        {
            throw new NotImplementedException();
        }

        public Website UpdateWebsite(Website website)
        {
            throw new NotImplementedException();
        }

        public void ActivateWebsite(int websiteId)
        {
            throw new NotImplementedException();
        }

        public void DeactivateWebsite(int websiteId)
        {
            throw new NotImplementedException();
        }

        public void DeleteWebsite(int websiteId)
        {
            throw new NotImplementedException();
        }

        public void UndeleteWebsite(int websiteId)
        {
            throw new NotImplementedException();
        }

        public void PergeWebsites()
        {
            throw new NotImplementedException();
        }
    }
}