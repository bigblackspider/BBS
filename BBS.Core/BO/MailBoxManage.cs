using System;
using System.Collections.Generic;
using BBS.Core.Models;
using BBS.Core.Models.Extentions;

namespace BBS.Core.BO
{
    public class MailBoxManage
    {
        private readonly IBBSEntities _ctx;

        private MailBoxManage()
        {
            _ctx = new BBSEntities();
        }

        public MailBoxManage(IBBSEntities value)
        {
            _ctx = value;
        }

        

        public List<MailBox> GetMailBoxs()
        {
            throw new NotImplementedException();
        }

        public List<MailBox> GetMailBoxs(int startId, int count)
        {
            throw new NotImplementedException();
        }

        public List<MailBox> GetCustomerMailBoxs(int customerId)
        {
            throw new NotImplementedException();
        }

        public List<MailBox> GetCustomerMailBoxs(int customerId, int startId, int count)
        {
            throw new NotImplementedException();
        }

        public MailBox GetMailbox(int mailBoxId)
        {
            throw new NotImplementedException();
        }

        public MailBox CreateMailbox(MailBox mailBox)
        {
            throw new NotImplementedException();
        }

        public MailBox UpdateMailbox(MailBox mailbox)
        {
            throw new NotImplementedException();
        }


        public void ActivateMailbox(int mailBoxId)
        {
            throw new NotImplementedException();
        }

        public void DeactivateMailbox(int mailBoxId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMailbox(int mailBoxId)
        {
            throw new NotImplementedException();
        }

        public void UndeleteMailbox(int mailBoxId)
        {
            throw new NotImplementedException();
        }

        public void PergeMailboxs()
        {
            throw new NotImplementedException();
        }

        public List<MailAccount> GetMailAccounts(int mailBoxId)
        {
            throw new NotImplementedException();
        }

        public List<MailAccount> GetMailAccounts(int mailBoxId, int startId, int count)
        {
            throw new NotImplementedException();
        }

        public MailAccount GetMailAccount(int mailAccountId)
        {
            throw new NotImplementedException();

        }

        public MailAccount CreateMailAccount(MailAccount mailAccount)
        {
            throw new NotImplementedException();
        }

        public MailAccount UpdateMailAccount(MailAccount mailAccount)
        {
            throw new NotImplementedException();
        }

        public void DeleteMailAccount(int mailAccountId)
        {
            throw new NotImplementedException();
        }

        public List<MailAliasName> GetMailAliasNames(int mailBoxId)
        {
            throw new NotImplementedException();
        }

        public List<MailAliasName> GetMailAliasNames(int mailBoxId, int v1, int v2)
        {
            throw new NotImplementedException();
        }

        public MailAliasName GetMailAliasName(int aliasNameId)
        {
            throw new NotImplementedException();
        }

        public MailAliasName CreateMailAliasName(MailAliasName mailAliasName)
        {
            throw new NotImplementedException();
        }

        public MailAliasName UpdateMailAliasName(MailAliasName mailAliasName)
        {
            throw new NotImplementedException();
        }

        public void DeleteMailAliasName(int mailAliasNameId)
        {
            throw new NotImplementedException();
        }
    }
}