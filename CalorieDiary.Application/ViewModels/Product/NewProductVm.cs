using AutoMapper;
using CalorieDiary.Application.Mapping;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.ViewModels.Product
{
    public class NewProductVm : IMapFrom<CalorieDiary.Domain.Model.Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Protein { get; set; }
        public double Carbo { get; set; }
        public double Fat { get; set; }
        public bool IsVerified { get; set; }
        public int Kcal { get; set; }
        public DateTime CreatedTime { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewProductVm, CalorieDiary.Domain.Model.Product>();
        }
    }
    public class NewProductValidation : AbstractValidator<NewProductVm>
    {
        public NewProductValidation()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }
}
