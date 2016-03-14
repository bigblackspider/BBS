using System;
using System.Collections.Generic;
using BBS.Core.Models;
using BBS.Core.Models.Extentions;

namespace BBS.Core.BO
{
    public class ProductManage
    {
        private readonly IBBSEntities _ctx;

        private ProductManage()
        {
            _ctx = new BBSEntities();
        }

        public ProductManage(IBBSEntities value)
        {
            _ctx = value;
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(int startId, int count)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Product CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}