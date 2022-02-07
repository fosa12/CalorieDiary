using CalorieDiary.Application.Interfaces;
using CalorieDiary.Application.ViewModels.Calculator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieDiary.Web.Controllers
{
    [Authorize]
    public class CalculatorController : Controller
    {
        private readonly ICalculatorDemand _calculatorDemand;
        public CalculatorController(ICalculatorDemand calculatorDemand)
        {
            _calculatorDemand = calculatorDemand;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Calculator()
        {

            return View(new CalculateCaloricDemandVm());
        }
        [HttpPost]
        public IActionResult Calculator(CalculateCaloricDemandVm CalculateCaloricDemandVm)
        {
            var id = _calculatorDemand.CalculateCaloricDemnad(CalculateCaloricDemandVm);    
            return RedirectToAction("LastCalculationDeatils", id);
        }
        public IActionResult LastCalculationDeatils(int id)
        {
            var model = _calculatorDemand.GetCalculationById(id);

            return View(model);
        }
    }
}
