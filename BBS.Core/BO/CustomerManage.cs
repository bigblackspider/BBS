using System;
using System.Collections.Generic;
using BBS.Core.Models;
using BBS.Core.Models.Extentions;

namespace BBS.Core.BO
{
    public class CustomerManage
    {
        private readonly IBBSEntities _ctx;

        private CustomerManage()
        {
            _ctx = new BBSEntities();
        }

        public CustomerManage(IBBSEntities value)
        {
            _ctx = value;
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetCustomers(int i, int i1)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Customer CreateCustomer(Customer cust)
        {
            throw new NotImplementedException();
        }

        public Customer UpdateCustomer(Customer cust)
        {
            throw new NotImplementedException();
        }

        public void ActivateCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public void DeactivateCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public bool DoesEmailExist(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public void UndeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public int ArchiveCustomers()
        {
            throw new NotImplementedException();
        }

        public Customer RestoreCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}