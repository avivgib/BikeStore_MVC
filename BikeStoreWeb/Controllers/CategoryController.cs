using BikeStoreWeb.Data;
using BikeStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BikeStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.category_name == obj.category_id.ToString())
            {
                ModelState.AddModelError("category_name", "The Category Id cannot exactly match the Caegory Name.");
            }

            //if (obj.category_name != null && obj.category_name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value.");
            //}

            if (ModelState.IsValid)
            {
                obj.category_id = 0;
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Category");// To explicit go to category controller
            }
            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _db.Categories.Find(id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.category_id==id);
            //Category? categoryFromDB2 = _db.Categories.Where(u=>u.category_id==id).FirstOrDefault();

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _db.Categories.Find(obj.category_id);
                if (existingCategory != null)
                {
                    existingCategory.category_name = obj.category_name;

                    // Update other properties as needed

                    _db.Categories.Update(existingCategory);
                    _db.SaveChanges();
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the existing category is not found
                    return NotFound();
                }
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDB = _db.Categories.Find(id);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
