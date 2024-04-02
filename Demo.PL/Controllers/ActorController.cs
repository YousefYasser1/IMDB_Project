using Demo.BLL.InterFace;
using Demo.DAL.Entites;
using Demo.PL.Const;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class ActorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }





        public async Task<IActionResult> Index()
        {
            var actors = await _unitOfWork.Actor.GetAll();
            return View(actors);
        }










        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorViewModel newModel)
        {
            if (ModelState.IsValid)
            {
                    Actor actor = new();
                    string imageString = DocumentSetting.UploadFile(newModel.ImageFile, "ActorImages");


                    actor.FirstName = newModel.FirstName;
                    actor.LastName = newModel.LastName;
                    actor.FullName = newModel.FirstName + " " + newModel.LastName;
                    actor.ImageName = imageString;


                    _unitOfWork.Actor.Create(actor);
                    await _unitOfWork.Complete();

                    TempData["Successflly"] = "Director Added Successfully";
                    return RedirectToAction(nameof(Index));
            }
            TempData["ErrorData"] = "SomeThing Wrong.....";
            return View(newModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _unitOfWork.Actor.GetById(id);
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id, Actor newModel)
        {
            if (id != newModel.ID)
                return BadRequest();
            if (newModel is null)
                return BadRequest();

            _unitOfWork.Actor.Delete(newModel);
            int count = await _unitOfWork.Complete();

            if (count > 0)
                DocumentSetting.DeleteFile(newModel.ImageName, "ActorImages");

            TempData["DeletedData"] = "Item Deleted Successfully....";
            return RedirectToAction("Index");
        }






        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return BadRequest();
            var actor = await _unitOfWork.Actor.GetById(id);
            return View(actor);
        }







        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return BadRequest();
            var actor = await _unitOfWork.Actor.GetById(id);
            EditActorViewModel VM = new() { ID = actor.ID, FirstName = actor.FirstName, LastName = actor.LastName, ImageName = actor.ImageName };

            return View(VM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditActorViewModel newModel)
        {
            if (id != newModel.ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var actor = await _unitOfWork.Actor.GetById(id);
                if (actor == null)
                    return BadRequest();
                   
                DocumentSetting.DeleteFile(newModel.ImageName, "ActorImages");
                    string ImageString = DocumentSetting.UploadFile(newModel.ImageFile, "ActorImages");
                    actor.FirstName = newModel.FirstName;
                    actor.LastName = newModel.LastName;
                    actor.FullName = newModel.FirstName + " " + newModel.LastName;
                    actor.ImageName = ImageString;

                    _unitOfWork.Actor.Edit(actor);
                    await _unitOfWork.Complete();
                    TempData["UpdateData"] = "Data Updated Successfully";
                    return RedirectToAction("Index");
            }
            return View(newModel);
        }


    }
}
