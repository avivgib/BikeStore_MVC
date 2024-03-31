using BikeStore.DataAccess.Repository.IRepository;
using BikeStore.Models;
using BikeStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace BikeStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) // Dependency Injection
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public ActionResult Index()
        {
            List<Products> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Upsert(int? id) // Update + Insert
        {
            ProductVM productVM = new()
            {
                //CategoryList = CategoryList,
                CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.category_name,
                    Value = u.category_id.ToString()
                }),
                Product = new Products()
            };

            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.product_id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // Get the root path of the wwwroot folder
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    // Create a random name for the new file
                    string fileName  = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Combine with the relative path to your directory
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.product_id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

                productVM.Product.product_id = 0;
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.category_name,
                        Value = u.category_id.ToString()
                    });
                return View(productVM);
            }
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productFromDB = _unitOfWork.Product.Get(u => u.product_id == id);
        //    if (productFromDB == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDB);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product Updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Products? productFromDB = _unitOfWork.Product.Get(u => u.product_id == id);

            if (productFromDB == null)
            {
                return NotFound();
            }

            return View(productFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Products? obj = _unitOfWork.Product.Get(u => u.product_id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
