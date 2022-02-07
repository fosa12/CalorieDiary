using AutoMapper;
using CalorieDiary.Application.Interfaces;
using CalorieDiary.Application.ViewModels.Calculator;
using CalorieDiary.Domain.Interfaces;
using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.Services
{
    public class CalculatorDemandService : ICalculatorDemand
    {
        private readonly IMapper _mapper;
        private readonly ICalculatorRepository _calculatorRepository;

        public CalculatorDemandService(IMapper mapper, ICalculatorRepository calculatorRepository)
        {
            _mapper = mapper;
            _calculatorRepository = calculatorRepository;   
        }

        public int CalculateCaloricDemnad(CalculateCaloricDemandVm calculateCaloricDemandVm)
        {
            //Ustalono, że każdy z makroskładników dostarcza nam pewną dawkę energii:
            //– 1 g białka – 4 kcal; – 1g tłuszczów – 9 kcal; – 1 g węglowodanów – 4 kcal.
            //Wzór dla kobiet to: BMR = 655 + (9,6 × waga w kg) + (1,8 × wysokość w cm) - (4,7 × wiek w latach).
            if (calculateCaloricDemandVm.Sex == "Woman")
            {
                double kcalDemandWoman = 665 + (9.6 * calculateCaloricDemandVm.Weight) + (1.8 * calculateCaloricDemandVm.Height) - (4.7 * calculateCaloricDemandVm.Age);

                kcalDemandWoman = KcalPAL(kcalDemandWoman, calculateCaloricDemandVm.Activity);

                calculateCaloricDemandVm.DailyRequirementKcal = kcalDemandWoman;
                var carboDemand = (kcalDemandWoman * 0.5) / 4;
                calculateCaloricDemandVm.DailyRequirementCarbo = carboDemand;

                var proteinDemand = (kcalDemandWoman * 0.2) / 4;
                calculateCaloricDemandVm.DailyRequirementProtein = proteinDemand;

                var fatDemand = (kcalDemandWoman * 0.3) / 9;
                calculateCaloricDemandVm.DailyRequirementFat = fatDemand;
            }
            //Wzór dla mężczyzn to: BMR = 66 + (13,7 × waga w kg) + (5 × wysokość w cm) - (6,8 × wiek w latach).
            if (calculateCaloricDemandVm.Sex == "Man")
            {
                double kcalDemandWoman = 66 + (13.7 * calculateCaloricDemandVm.Weight) + (5 * calculateCaloricDemandVm.Height) - (6.8 * calculateCaloricDemandVm.Age);

                kcalDemandWoman = KcalPAL(kcalDemandWoman, calculateCaloricDemandVm.Activity);

                calculateCaloricDemandVm.DailyRequirementKcal = kcalDemandWoman;

                var carboDemand = (kcalDemandWoman * 0.5) / 4;
                calculateCaloricDemandVm.DailyRequirementCarbo = carboDemand;

                var proteinDemand = (kcalDemandWoman * 0.2) / 4;
                calculateCaloricDemandVm.DailyRequirementProtein = proteinDemand;

                var fatDemand = (kcalDemandWoman * 0.3) / 9;
                calculateCaloricDemandVm.DailyRequirementFat = fatDemand;
            }
            calculateCaloricDemandVm.CreatedTime = DateTime.Now;
            var calculated = _mapper.Map<CaloricDemand>(calculateCaloricDemandVm);

            var id =  _calculatorRepository.AddCalculatedDemand(calculated);
            return id;
        }

        public CalculatedDetailsVm GetCalculationById(int id)
        {
            CaloricDemand model = null;
            if(id == 0)
            {
                model = _calculatorRepository.GetLastCalculation();
            }
            else
            {
                model = _calculatorRepository.GetCalculationById(id);
            }
           // var model = _calculatorRepository.GetCalculationById(id);
            var calculation = _mapper.Map<CalculatedDetailsVm>(model);




            return calculation;
        }
        private double KcalPAL(double kcal, string PALActivity)
        {
            if (PALActivity == "Lackofphysicalactivity")
            {
                kcal = kcal * 1.2;
            }
            else if (PALActivity == "Lightactivity")
            {
                kcal = kcal * 1.4;
            }
            else if (PALActivity == "Averageactivity")
            {
                kcal = kcal * 1.6;
            }
            else if (PALActivity == "Highactivity")
            {
                kcal = kcal * 1.8;
            }
            else if(PALActivity == "Veryhighphysicalactivity")
            {
                kcal = kcal * 2.0;
            }
            return kcal;
        }
    }
}
