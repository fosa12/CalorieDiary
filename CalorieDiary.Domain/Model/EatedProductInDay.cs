using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Domain.Model
{
    public class EatedProductInDay
    {
        public int Id { get; set; }
        public DateTime WhenEated { get; set; }
        public int EatedGramsProduct { get; set; }
        public int EatedProductId { get; set; }
        public string ProductName { get; set; }
        public int DaysId { get; set; }
        public virtual Day Days { get; set; }



    }
}
