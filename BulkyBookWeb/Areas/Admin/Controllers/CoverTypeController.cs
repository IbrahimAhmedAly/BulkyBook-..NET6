using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitFoWork;
        public CoverTypeController(IUnitOfWork unitFoWork)
        {
            _unitFoWork = unitFoWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitFoWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //GET 
        public IActionResult Create()
        {
            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitFoWork.CoverType.Add(obj);
                _unitFoWork.Save();
                TempData["success"] = "CoverType created successfuly";
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
            var coverTypeFromDb = _unitFoWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDb);
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitFoWork.CoverType.Update(obj);
                _unitFoWork.Save();
                TempData["success"] = "CoverType updated successfuly";
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
            var coverTypeFromDb = _unitFoWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDb);
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitFoWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitFoWork.CoverType.Remove(obj);
            _unitFoWork.Save();
            TempData["success"] = "CoverType removed successfuly";
            return RedirectToAction("Index");
        }
    }
}
