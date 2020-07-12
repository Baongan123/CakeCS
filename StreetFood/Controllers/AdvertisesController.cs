using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StreetFood.Models;
using StreetFood.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreetFood.Controllers
{
    public class AdvertisesController:Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAdvertisesRepository advertisesRepository;

        public AdvertisesController(IWebHostEnvironment webHostEnvironment,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IAdvertisesRepository advertisesRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.advertisesRepository = advertisesRepository;
        }
        public IActionResult Index()
        {
            List<Advertises> advertises = advertisesRepository.Gets().ToList();
            return View(advertises);
        }
        public IActionResult Edit(int id)
        {
            Advertises advertises = advertisesRepository.Get(id);
            if (advertises == null)
            {
                return RedirectToAction("");
            }
            var model = new EditAdvertisesModel()
            {
                Id = advertises.Id,
                Img = advertises.Img,
                Title = advertises.Title
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditAdvertisesModel model)
        {
            if (ModelState.IsValid)
            {
                Advertises advertises = new Advertises()
                {
                    Id = model.Id,
                    Title=model.Title,
                    Img=model.Img
                };
                var fileName = string.Empty;
                if (model.ImgParth != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Advertises");
                    fileName = $"{Guid.NewGuid()}_{model.ImgParth.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImgParth.CopyTo(fs);
                    }
                    advertises.Img = fileName;
                    if (!string.IsNullOrEmpty(model.Img))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath,
                                            "Images/Advertises", model.Img);
                        System.IO.File.Delete(delFile);
                    }
                }
                var editEmp = advertisesRepository.Edit(advertises);
                if (editEmp != null)
                {
                    return RedirectToAction("index", "Advertises");
                }
            }
            return View();
        }
    }
}
