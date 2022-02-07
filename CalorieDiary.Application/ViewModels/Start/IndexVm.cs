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
    public class IndexVm:IMapFrom<Day>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double EatedProtein { get; set; }
        public double EatedCarbo { get; set; }
        public double EatedFat { get; set; }
        public double EatedKcal { get; set; }
        public DateTime DayDate { get; set; }
        public string PercentEatedKcal { get; set; }
        public string PercentEatedCarbo { get; set; }
        public string PercentEatedFat { get; set; }
        public string PercentEatedProtein { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Day, IndexVm>();
        }
    }
}
