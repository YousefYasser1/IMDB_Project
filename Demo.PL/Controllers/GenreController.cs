using Demo.BLL.InterFace;
using Demo.DAL.Entites;
using Demo.PL.Const;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            var genreItems = await _unitOfWork.Genre.GetAll();
            return View(genreItems);
        }

        public async Task<IActionResult> Create()
        {
            return  View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(GenreViewModel newModel)
        {
            if (ModelState.IsValid)
            {
                Genre genre = new();
                genre.Name = newModel.Name;

                _unitOfWork.Genre.Create(genre);
                await _unitOfWork.Complete();

                TempData[NotificationMode.Successflly.ToString()] = "Genre Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData[NotificationMode.ErrorData.ToString()] = "SomeThing Wrong.....";
            return View(newModel);
        }





        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _unitOfWork.Genre.GetById(id);
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id, Genre newModel)
        {
            if (id != newModel.ID)
                return BadRequest();
           if(newModel is null)
                return BadRequest();

            _unitOfWork.Genre.Delete(newModel);
            await _unitOfWork.Complete();

            TempData[NotificationMode.DeletedData.ToString()] = "Item Deleted Successfully....";
            return RedirectToAction("Index");
        }








        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
                return BadRequest();
            var genre = await _unitOfWork.Genre.GetById(id);
            return View(genre);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return BadRequest();
            var genre = await _unitOfWork.Genre.GetById(id);

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Genre newModel)
        {
            if(id != newModel.ID) 
                return BadRequest();
            if (ModelState.IsValid)
            {
                var genre = await _unitOfWork.Genre.GetById(id);
                genre.Name = newModel.Name;
                _unitOfWork.Genre.Edit(genre);
                await _unitOfWork.Complete();
                TempData[NotificationMode.UpdateData.ToString()] = "Data Updated Successfully";

                return RedirectToAction("Index");
            }
            return View(newModel);
        }

    }
}
