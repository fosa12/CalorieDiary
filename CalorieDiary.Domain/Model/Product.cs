using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Protein { get; set; }
        public double Carbo { get; set; }
        public double Fat { get; set; }
        public bool IsVerified { get; set; }
        public int Kcal { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
