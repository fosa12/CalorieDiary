using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.ViewModels.Product
{
    public class ListProductForListVm
    {
        public List<ProductForListVm> Products { get; set; }
        public int Count { get; set; }
        public string SearchProduct { get; set; }
    }
}
