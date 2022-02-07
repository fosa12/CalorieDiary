using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Domain.Interfaces
{
    public interface ICalculatorRepository
    {
        int AddCalculatedDemand(CaloricDemand caloricDemand);
        CaloricDemand GetCalculationById(int id);
        CaloricDemand GetLastCalculation();
    }
}
