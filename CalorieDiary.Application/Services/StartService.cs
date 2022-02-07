using AutoMapper;
using AutoMapper.QueryableExtensions;
using CalorieDiary.Application.Interfaces;
using CalorieDiary.Application.ViewModels.Product;
using CalorieDiary.Application.ViewModels.Start;
using CalorieDiary.Domain.Interfaces;
using CalorieDiary.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieDiary.Application.Services
{
    public class StartService : IStartService
    {
        private readonly ICalculatorRepository _calculatorRepository;
        private readonly IDayRepo _dayRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        public StartService(ICalculatorRepository calculatorRepository,IDayRepo dayRepo,IMapper mapper, IProductRepository productRepository)
        {
            _calculatorRepository = calculatorRepository;
            _dayRepo = dayRepo;
            _mapper = mapper;
            _productRepo = productRepository;
        }

        public int AddProductToCurrentDay(ProductToAddVm productToAdd)
        {
            var curentDay = _dayRepo.GetLastDay();
            var product = _productRepo.GetProductById(productToAdd.EatedProductId);

            EatedProductInDay eatedProductInDay = new EatedProductInDay();
            eatedProductInDay.DaysId = productToAdd.DaysId;
            eatedProductInDay.ProductName = productToAdd.ProductName;
            eatedProductInDay.WhenEated = DateTime.Now;
            eatedProductInDay.Id = 0;
            eatedProductInDay.EatedGramsProduct = productToAdd.EatedGramsProduct;
            eatedProductInDay.EatedProductId = productToAdd.EatedProductId;

            _dayRepo.AddEatedProductToDay(eatedProductInDay);
            curentDay.EatedKcal = ((product.Kcal / 100.0) * eatedProductInDay.EatedGramsProduct)+curentDay.EatedKcal;
            curentDay.EatedFat = ((product.Fat / 100.0) * eatedProductInDay.EatedGramsProduct)+curentDay.EatedFat;
            curentDay.EatedProtein = ((product.Protein / 100.0) * eatedProductInDay.EatedGramsProduct)+curentDay.EatedProtein;
            curentDay.EatedCarbo = ((product.Carbo / 100.0) * eatedProductInDay.EatedGramsProduct)+curentDay.EatedCarbo;

            _dayRepo.SaveAddedMakro(curentDay);

            return productToAdd.Id;
        }

        public ProductToAddVm AddProductToCurrentDay(int productId)
        {
            var curentDay = _dayRepo.GetLastDay();
            var product = _productRepo.GetProductById(productId);
            ProductToAddVm productToAddVm = new ProductToAddVm();
            productToAddVm.ProductName = product.Name;
            productToAddVm.Id = 0;
            productToAddVm.DaysId = curentDay.Id;
            productToAddVm.EatedProductId= productId;
            return productToAddVm;

            
        }

        public void DeleteEatedProduct(int id)
        {
            var eatedProduct = _dayRepo.GetLastDayWithIncludedEatedProduct().EatedProductInDays
                .FirstOrDefault(x => x.Id == id);
            //var productToDelete = eatedProduct.EatedProductInDays.FirstOrDefault(x => x.Id == id);
            if (eatedProduct != null)
            {
                _dayRepo.RemoveEatedProduct(eatedProduct);
            }
        }

        public IndexVm GetDataForIndex()
        {
            var lastCalculate = _calculatorRepository.GetLastCalculation();
            var lastDay = _dayRepo.GetLastDay();
            if (lastDay.DayDate.Day != DateTime.Now.Day)
            {
                lastDay = new Domain.Model.Day();
                lastDay.DayDate = DateTime.Now;
                lastDay.Id = 0;
                lastDay.Id = _dayRepo.CreateNewDay(lastDay);
            }
            //lastDay.PercentEatedCarbo = (lastDay.EatedCarbo / lastCalculate.DailyRequirementCarbo) * 100;
            //lastDay.PercentEatedFat = (lastDay.EatedFat / lastCalculate.DailyRequirementFat) * 100;
            //lastDay.PercentEatedKcal = (lastDay.EatedKcal / lastCalculate.DailyRequirementKcal) * 100;

            double carbo = UptoOneDecimalPoints(lastDay.EatedCarbo / lastCalculate.DailyRequirementCarbo) * 100;
            double fat = UptoOneDecimalPoints(lastDay.EatedFat / lastCalculate.DailyRequirementFat) * 100;
            double kcal = ((Math.Ceiling(lastDay.EatedKcal) / Math.Ceiling(lastCalculate.DailyRequirementKcal)) * 100);
            double protein = UptoOneDecimalPoints(lastDay.EatedProtein / lastCalculate.DailyRequirementProtein) * 100;

            

            var indexVm = _mapper.Map<IndexVm>(lastDay);
            indexVm.PercentEatedCarbo = carbo.ToString() + "%";
            indexVm.PercentEatedFat = fat.ToString() + "%";
            indexVm.PercentEatedKcal = kcal.ToString() + "%";
            indexVm.PercentEatedProtein = protein.ToString() + "%";
            indexVm.EatedKcal= Math.Ceiling(indexVm.EatedKcal);
            indexVm.EatedFat = Math.Ceiling(indexVm.EatedFat);
            indexVm.EatedCarbo = Math.Ceiling(indexVm.EatedCarbo);
            indexVm.EatedProtein = Math.Ceiling(indexVm.EatedProtein);
            return indexVm;
        }

        public ListEatedProductForListVm GetEatedProductByDate(DateTime date)
        {
            Day currentDay;
            if (date.Date.Day == 1 && date.Date.Month == 1 && date.Date.Year == 1)
            {
                currentDay = _dayRepo.GetLastDayWithIncludedEatedProduct();
            }
            else
            {
                currentDay = _dayRepo.GetDayByDateWithIncludedEatedProduct(date.Date);
            }
            
            List<Product> products = new List<Product>();

            if (currentDay != null)
            {
                foreach (var item in currentDay.EatedProductInDays)
                {
                    var product = _productRepo.GetProductById(item.EatedProductId);
                    if (product != null)
                    {
                        products.Add(product);
                    }

                }
            }


            var productsList = new ListEatedProductForListVm();

            var gramsForEatedProduct = new List<int>();


            productsList.Products = new List<EatedProductForListVm>();
            var idEatedProduct = new List<int>();
            if (currentDay != null)
            {
                foreach (var item in currentDay.EatedProductInDays)
                {
                    gramsForEatedProduct.Add(item.EatedGramsProduct);
                }

                foreach (var item in currentDay.EatedProductInDays)
                {
                    idEatedProduct.Add(item.Id);
                }
            }





            int iteration = 0;
            foreach (var product in products)
            {

                var produVm = new EatedProductForListVm()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Kcal = (product.Kcal / 100) * gramsForEatedProduct[iteration],
                    Protein = UptoOneDecimalPoints((product.Protein / 100) * gramsForEatedProduct[iteration]),
                    Carbo = UptoOneDecimalPoints((product.Carbo / 100) * gramsForEatedProduct[iteration]),
                    Fat = UptoOneDecimalPoints((product.Fat / 100) * gramsForEatedProduct[iteration]),
                    IsVerified = product.IsVerified,
                    Gram = gramsForEatedProduct[iteration],
                    EatedProductId = idEatedProduct[iteration],

                };

                iteration++;

                productsList.Products.Add(produVm);
            }
            productsList.Count = productsList.Products.Count;


            //var productsList = new ListProductForListVm();





            return productsList;
        }

        public ListEatedProductForListVm GetEatedProductByDate()
        {
            Day currentDay;
            
            currentDay = _dayRepo.GetLastDayWithIncludedEatedProduct();


            List<Product> products = new List<Product>();

            foreach (var item in currentDay.EatedProductInDays)
            {
                var product = _productRepo.GetProductById(item.EatedProductId);
                products.Add(product);
            }

            var productsList = new ListEatedProductForListVm();

            var gramsForEatedProduct = new List<int>();


            productsList.Products = new List<EatedProductForListVm>();
            foreach (var item in currentDay.EatedProductInDays)
            {
                gramsForEatedProduct.Add(item.EatedGramsProduct);
            }
            var idEatedProduct = new List<int>();
            foreach (var item in currentDay.EatedProductInDays)
            {
                idEatedProduct.Add(item.Id);
            }


            int iteration = 0;
            foreach (var product in products)
            {

                var produVm = new EatedProductForListVm()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Kcal = (product.Kcal / 100) * gramsForEatedProduct[iteration],
                    Protein = UptoOneDecimalPoints((product.Protein / 100) * gramsForEatedProduct[iteration]),
                    Carbo = UptoOneDecimalPoints((product.Carbo / 100) * gramsForEatedProduct[iteration]),
                    Fat = UptoOneDecimalPoints((product.Fat / 100) * gramsForEatedProduct[iteration]),
                    IsVerified = product.IsVerified,
                    Gram = gramsForEatedProduct[iteration],
                    EatedProductId = idEatedProduct[iteration],

                };

                iteration++;

                productsList.Products.Add(produVm);
            }
            productsList.Count = productsList.Products.Count;


            //var productsList = new ListProductForListVm();





            return productsList;
        }

        public ListEatedProductForListVm GetEatedProductInCurrentDay()
        {
            var currentDay = _dayRepo.GetLastDayWithIncludedEatedProduct();
            List<Product> products = new List<Product>();

            foreach (var item in currentDay.EatedProductInDays)
            {
                var product = _productRepo.GetProductById(item.EatedProductId);
                products.Add(product);
            }

            var productsList = new ListEatedProductForListVm();
            
            var gramsForEatedProduct = new List<int>();


            productsList.Products = new List<EatedProductForListVm>();
            foreach (var item in currentDay.EatedProductInDays)
            {
                gramsForEatedProduct.Add(item.EatedGramsProduct);
            }
            var idEatedProduct = new List<int>();
            foreach (var item in currentDay.EatedProductInDays)
            {
                idEatedProduct.Add(item.Id);
            }


            int iteration = 0;
            foreach (var product in products)
            {
                
                var produVm = new EatedProductForListVm()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Kcal = (product.Kcal/100) * gramsForEatedProduct[iteration],
                    Protein = UptoOneDecimalPoints((product.Protein / 100) *gramsForEatedProduct[iteration]),
                    Carbo = UptoOneDecimalPoints((product.Carbo / 100) * gramsForEatedProduct[iteration]),
                    Fat = UptoOneDecimalPoints((product.Fat / 100) * gramsForEatedProduct[iteration]),
                    IsVerified = product.IsVerified,
                    Gram = gramsForEatedProduct[iteration],
                    EatedProductId = idEatedProduct[iteration],
                    
            };

                iteration++;

                productsList.Products.Add(produVm);
            }
            productsList.Count = productsList.Products.Count;


            //var productsList = new ListProductForListVm();





            return productsList;
        }
        public double UptoOneDecimalPoints(double numToConvert)
        {
            var convertNumber = Convert.ToDouble(String.Format("{0:0.0}", numToConvert));
            return convertNumber;
        }
    }

}
