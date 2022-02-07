using CalorieDiary.Application.Interfaces;
using CalorieDiary.Application.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalorieDiary.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var model = _productService.GetAllProductForList("");
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(string searchProduct)
        {
            if (searchProduct is null)
            {
                searchProduct = String.Empty;
            }
            var model = _productService.GetAllProductForList(searchProduct);
            return View(model);
        }
        public IActionResult GetProductToVerify()
        {
            var model = _productService.GetProductsForVerify();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct(int id)
        {


            return View(new NewProductVm());
        }
        [HttpPost]
        public IActionResult AddProduct(NewProductVm newProductVm)
        {
            var id = _productService.AddProduct(newProductVm);
            return RedirectToAction("Index");
        }
        public IActionResult VerifyProduct(int id)
        {
            _productService.VerifyProduct(id);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
 