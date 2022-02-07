using CalorieDiary.Domain.Interfaces;
using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context)
        {
            _context = context;
        }
        


        public IQueryable<Product> GetNotVerifyProducts()
        {
            var products = _context.Products.Where(x => x.IsVerified == false);
            return products;
        }
        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }
        public void RemoveProduct(Product product)
        {
            product.IsRemoved = true;
            _context.Attach(product);
            _context.Entry(product).Property("IsRemoved").IsModified=true;
            _context.SaveChanges(); 

        }

        public IQueryable<Product> GetAllProducts()
        {
            var products = _context.Products.Where(x => x.IsRemoved == false);
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        public void VerifyProduct(Product product)

        {
            if (product != null)
            {
                product.IsVerified = true;
                _context.Attach(product);
                _context.Entry(product).Property("IsVerified").IsModified = true;
                _context.SaveChanges();
            }




        }
    }
}
