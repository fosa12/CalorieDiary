using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Domain.Model
{
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double EatedProtein { get; set; }
        public double EatedCarbo { get; set; }
        public double EatedFat { get; set; }
        public double EatedKcal { get; set; }
        public DateTime DayDate { get; set; }
        public double PercentEatedKcal { get; set; }
        public double PercentEatedCarbo { get; set; }
        public double PercentEatedFat { get; set; }
        public double PercentEatedProtein { get; set; }
        public virtual ICollection<EatedProductInDay> EatedProductInDays { get; set; }

    }
}
