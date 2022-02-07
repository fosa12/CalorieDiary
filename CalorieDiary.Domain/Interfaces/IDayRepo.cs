using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Domain.Interfaces
{
    public interface IDayRepo
    {
        Day GetLastDay();
        int CreateNewDay(Day newDay);
        int AddEatedProductToDay(EatedProductInDay eatedProductInDay);
        void SaveAddedMakro(Day curentDay);
        Day GetLastDayWithIncludedEatedProduct();
        Day GetDayByDateWithIncludedEatedProduct(DateTime dateTime);
        void RemoveEatedProduct(EatedProductInDay product);
    }
}
