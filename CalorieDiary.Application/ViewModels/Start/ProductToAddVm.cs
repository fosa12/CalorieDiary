using AutoMapper;
using CalorieDiary.Application.Mapping;
using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.ViewModels.Start
{
    public class ProductToAddVm:IMapFrom<EatedProductInDay>
    {
        public int Id { get; set; }
        public DateTime WhenEated { get; set; }
        public int EatedGramsProduct { get; set; }
        public int EatedProductId { get; set; }
        public int DaysId { get; set; }
        public string ProductName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductToAddVm, EatedProductInDay>().ReverseMap();
        }
    }

}
