using AutoMapper;
using CalorieDiary.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.ViewModels.Product
{
    public class ProductForListVm : IMapFrom<CalorieDiary.Domain.Model.Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Protein { get; set; }
        public double Carbo { get; set; }
        public double Fat { get; set; }
        public bool IsVerified { get; set; }
        public int Kcal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CalorieDiary.Domain.Model.Product, ProductForListVm>();
        }
    }
}
