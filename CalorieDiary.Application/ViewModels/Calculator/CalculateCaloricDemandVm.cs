using AutoMapper;
using CalorieDiary.Application.Mapping;
using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.ViewModels.Calculator
{
    public class CalculateCaloricDemandVm : IMapFrom<CaloricDemand>
    {
        public int Id { get; set; }
        public double DailyRequirementKcal { get; set; }
        public double DailyRequirementFat { get; set; }
        public double DailyRequirementProtein { get; set; }
        public double DailyRequirementCarbo { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Sex { get; set; }
        public string Activity { get; set; }
        public string Goal { get; set; }
        public int Age { get; set; }
        public DateTime CreatedTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CalculateCaloricDemandVm, CalorieDiary.Domain.Model.CaloricDemand>();
        }
    }
}
