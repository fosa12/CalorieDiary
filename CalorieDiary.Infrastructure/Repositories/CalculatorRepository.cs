using CalorieDiary.Domain.Interfaces;
using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Infrastructure.Repositories
{
    public class CalculatorRepository : ICalculatorRepository
    {
        private readonly Context _context;
        public CalculatorRepository(Context context)
        {
            _context = context; 
        }
        public int AddCalculatedDemand(CaloricDemand caloricDemand)
        {
            _context.CaloricDemands.Add(caloricDemand);
            _context.SaveChanges();
            return caloricDemand.Id;
        }

        public CaloricDemand GetCalculationById(int id)
        {
            var calculation = _context.CaloricDemands.FirstOrDefault(x => x.Id == id);
            return calculation;
        }

        public CaloricDemand GetLastCalculation()//LAST CALCULATION BY ID
        {
            var lastCalculation = _context.CaloricDemands.OrderBy(x => x.Id).Last();
            return lastCalculation;
        }
    }
}
