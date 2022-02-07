using CalorieDiary.Application.ViewModels.Calculator;
using CalorieDiary.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.Interfaces
{
    public interface IProductService
    {
        ListProductForListVm GetAllProductForList(string searchProduct);
        int AddProduct(NewProductVm newProduct);
        ListProductForListVm GetProductsForVerify();
        void VerifyProduct(int id);
    }
}
