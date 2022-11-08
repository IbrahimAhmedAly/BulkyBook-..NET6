using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitFoWork;
        public CategoryController(IUnitOfWork unitFoWork)
        {
            _unitFoWork = unitFoWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitFoWork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET 
        public IActionResult Create()
        {
            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitFoWork.Category.Add(obj);
                _unitFoWork.Save();
                TempData["success"] = "Category created successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET 
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitFoWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitFoWork.Category.Update(obj);
                _unitFoWork.Save();
                TempData["success"] = "Category updated successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitFoWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitFoWork.Category.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitFoWork.Category.Remove(obj);
            _unitFoWork.Save();
            TempData["success"] = "Category removed successfuly";
            return RedirectToAction("Index");
        }
    }
}
