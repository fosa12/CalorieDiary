using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllProducts();
        IQueryable<Product> GetNotVerifyProducts();
        int AddProduct(Product product);
        void RemoveProduct(Product product);
        Product GetProductById(int id);
        void VerifyProduct(Product product);
        
    }
}
