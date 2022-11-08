using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET 
        public IActionResult Upsert(int? id)
        {
            Company company = new();
 
            if (id == null || id == 0)
            {
                // create product
                return View(company);
            }
            else
            {
                // update product
                company = _unitOfWork.Company.GetFirstOrDefault(p => p.Id == id);
                return View(company);
            }
            return View(company);
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created successfuly";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company updated successfuly";
                }
                _unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var compnayList = _unitOfWork.Company.GetAll();
            return Json(new { data = compnayList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
