using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.ViewModels.Start
{
    public class ListEatedProductForListVm
    {
        public List<EatedProductForListVm> Products { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}
