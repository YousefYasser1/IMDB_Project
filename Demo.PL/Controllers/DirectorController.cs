using Demo.BLL.InterFace;
using Demo.DAL.Entites;
using Demo.PL.Const;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DirectorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DirectorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var director = await _unitOfWork.Director.GetAll();
            return View(director);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorViewModel newModel)
        {
            if (ModelState.IsValid)
            {
                Director director = new();
                if (newModel.ImageFile != null)
                {
                  string imageString = DocumentSetting.UploadFile(newModel.ImageFile, "DirectorsImages");

                 
                  director.FirstName = newModel.FirstName;
                  director.LastName = newModel.LastName;
                  director.Age = newModel.Age;
                  director.FullName = newModel.FirstName+" "+newModel.LastName;
                  director.ImageName = imageString;


                    _unitOfWork.Director.Create(director);
                    await _unitOfWork.Complete();

                    TempData["Successflly"] = "Director Added Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string imageString = DefaultImageName.ProfilePictuer.ToString()+".jpg";
                    director.FirstName = newModel.FirstName;
                    director.LastName = newModel.LastName;
                    director.Age = newModel.Age;
                    director.ImageName = imageString;

                    _unitOfWork.Director.Create(director);
                    await _unitOfWork.Complete();

                    TempData["Successflly"] = "Director Added Successfully";
                    return RedirectToAction(nameof(Index));
                }

            }
            TempData["ErrorData"] = "SomeThing Wrong.....";
            return View(newModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var director = await _unitOfWork.Director.GetById(id);
            return View(director);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id, Director newModel)
        {
            if (id != newModel.ID)
                return BadRequest();
            if (newModel is null)
                return BadRequest();

            _unitOfWork.Director.Delete(newModel);
            int count = await _unitOfWork.Complete();

            if ((count > 0) && (newModel.ImageName != DefaultImageName.ProfilePictuer.ToString()+".jpg")) 
                DocumentSetting.DeleteFile(newModel.ImageName, "DirectorsImages");

            TempData["DeletedData"] = "Item Deleted Successfully....";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return BadRequest();
            var director = await _unitOfWork.Director.GetById(id);
            return View(director);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return BadRequest();
            var director = await _unitOfWork.Director.GetById(id);
            EditDirectorViewModel VM = new() { ID = director.ID, FirstName = director.FirstName, LastName = director.LastName, Age = director.Age, ImageName = director.ImageName};

            return View(VM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDirectorViewModel newModel)
        {
            if (id != newModel.ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var director = await _unitOfWork.Director.GetById(id);
                if(director == null)
                    return BadRequest();
                if (newModel.ImageFile != null)
                {
                    DocumentSetting.DeleteFile(newModel.ImageName, "DirectorsImages");
                    string ImageString = DocumentSetting.UploadFile(newModel.ImageFile, "DirectorsImages");
                    director.FirstName = newModel.FirstName;
                    director.LastName = newModel.LastName;
                    director.Age = newModel.Age;
                    director.FullName = newModel.FirstName + " " + newModel.LastName;
                    director.ImageName = ImageString;

                    _unitOfWork.Director.Edit(director);
                    await _unitOfWork.Complete();
                    TempData["UpdateData"] = "Data Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    director.FirstName = newModel.FirstName;
                    director.LastName = newModel.LastName;
                    director.FullName = newModel.FirstName + " " + newModel.LastName;
                    director.Age = newModel.Age;

                    _unitOfWork.Director.Edit(director);
                    await _unitOfWork.Complete();
                    TempData["UpdateData"] = "Data Updated Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(newModel);
        }

    }
}
