using CalorieDiary.Application.ViewModels.Product;
using CalorieDiary.Application.ViewModels.Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.Interfaces
{
    public interface IStartService
    {
        IndexVm GetDataForIndex();
        int AddProductToCurrentDay(ProductToAddVm productToAdd);
        ProductToAddVm AddProductToCurrentDay(int productId);
        ListEatedProductForListVm GetEatedProductInCurrentDay();
        ListEatedProductForListVm GetEatedProductByDate(DateTime date);
        ListEatedProductForListVm GetEatedProductByDate();
        void DeleteEatedProduct(int id);
    }
}
