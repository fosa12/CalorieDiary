using CalorieDiary.Application.ViewModels.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.Interfaces
{
    public interface ICalculatorDemand
    {
        int CalculateCaloricDemnad(CalculateCaloricDemandVm calculateCaloricDemandVm);
        CalculatedDetailsVm GetCalculationById(int id);
    }
}
