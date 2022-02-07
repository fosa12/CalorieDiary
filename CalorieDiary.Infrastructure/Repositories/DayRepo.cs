using CalorieDiary.Domain.Interfaces;
using CalorieDiary.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Infrastructure.Repositories
{
    public class DayRepo : IDayRepo
    {
        private readonly Context _context;
        public DayRepo(Context context)
        {
            _context = context;
        }

        public int AddEatedProductToDay(EatedProductInDay eatedProductInDay)
        {
            _context.Add(eatedProductInDay);
            _context.SaveChanges();
            return eatedProductInDay.Id;
        }

        public int CreateNewDay(Day newDay)
        {
            _context.Days.Add(newDay);
            _context.SaveChanges();
            return newDay.Id;
        }

        public Day GetDayByDateWithIncludedEatedProduct(DateTime dateTime)
        {
            var lastDay = _context.Days.Include(x => x.EatedProductInDays).FirstOrDefault(x => x.DayDate.Day == dateTime.Day && x.DayDate.Month == dateTime.Month && x.DayDate.Year == dateTime.Year);
            return lastDay;
        }

        public Day GetLastDay()
        {
            var lastDay = _context.Days.OrderBy(x=> x.Id).Last();
            return lastDay;
        }
        public Day GetLastDayWithIncludedEatedProduct()
        {
            var lastDay = _context.Days.Include(x => x.EatedProductInDays).OrderBy(x=>x.Id).Last();
            return lastDay;
        }

        public void RemoveEatedProduct(EatedProductInDay product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public void SaveAddedMakro(Day curentDay)
        {
            _context.Attach(curentDay);
            _context.Entry(curentDay).Property("PercentEatedKcal").IsModified = true;
            _context.Entry(curentDay).Property("PercentEatedCarbo").IsModified = true;
            _context.Entry(curentDay).Property("PercentEatedFat").IsModified = true;
            _context.Entry(curentDay).Property("PercentEatedProtein").IsModified = true;
            _context.SaveChanges(true);

        }
    }
}
