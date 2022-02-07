using CalorieDiary.Application.Interfaces;
using CalorieDiary.Application.ViewModels.Start;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalorieDiary.Web.Controllers
{
    [Authorize]
    public class StartController : Controller
    {
        private readonly IStartService _startService;
        public StartController(IStartService startService)
        {
            _startService = startService;
        }
        public IActionResult Index()
        {
            var model = _startService.GetDataForIndex();

            return View(model);
        }
        [HttpGet]
        public IActionResult AddProductToCurrentDay(int id)
        {
            var model = _startService.AddProductToCurrentDay(id); 
            return View(model);
        }
        [HttpPost]
        public IActionResult AddProductToCurrentDay(ProductToAddVm productToAddVm)
        {
            var model = _startService.AddProductToCurrentDay(productToAddVm);
            return RedirectToAction("Index", "Start");
        }
        public IActionResult ShowEatedProductInCurrentDay()
        {
            var model = _startService.GetEatedProductInCurrentDay();

            return View(model);
        }
        [HttpGet]
        public IActionResult ShowEatedProductByDate(DataForEatedProductInDayVm data)
        {
            
            var model = _startService.GetEatedProductByDate(data.Date);
            return View(model);
        }

        //[HttpPost]
        //public IActionResult ShowEatedProductByDate(ListEatedProductForListVm data)
        //{
        //    var model = _startService.GetEatedProductByDate(data.Date);
        //    ShowEatedProductInCurrentDay()
        //    return View(model);
        //}
        [HttpGet]
        public IActionResult ShowEatedProductDate()
        {

            //var model = _startService.GetEatedProductDate();
            return View(new DataForEatedProductInDayVm());
        }

        [HttpPost]
        public IActionResult ShowEatedProductDate(DataForEatedProductInDayVm data)
        {

            
            return RedirectToAction("ShowEatedProductByDate", data);
        }
        public IActionResult DeleteEatedProduct(int id)
        {
            _startService.DeleteEatedProduct(id);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
