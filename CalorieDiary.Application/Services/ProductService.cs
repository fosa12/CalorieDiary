using AutoMapper;
using AutoMapper.QueryableExtensions;
using CalorieDiary.Application.Interfaces;
using CalorieDiary.Application.ViewModels.Calculator;
using CalorieDiary.Application.ViewModels.Product;
using CalorieDiary.Domain.Interfaces;
using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository; 
        }
        public int AddProduct(NewProductVm newProduct)
        {
            newProduct.CreatedTime = DateTime.Now;
            var product = _mapper.Map<Product>(newProduct);
            var id = _productRepository.AddProduct(product);


            return id;
        }

        public ListProductForListVm GetAllProductForList(string searchString)
        {
            var products = _productRepository.GetAllProducts().Where(x => x.Name.StartsWith(searchString))
                .ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider).ToList();

            var productsList = new ListProductForListVm()
            {
                Products = products,
                Count = products.Count
            };




            return productsList;
        }

        public ListProductForListVm GetProductsForVerify()
        {
            var products = _productRepository.GetAllProducts().Where(x=> x.IsVerified == false)
                .ProjectTo<ProductForListVm>(_mapper.ConfigurationProvider).ToList();

            var productsList = new ListProductForListVm()
            {
                Products = products,
                Count = products.Count
            };




            return productsList;
        }

        public void VerifyProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            _productRepository.VerifyProduct(product);
        }
    }
}
