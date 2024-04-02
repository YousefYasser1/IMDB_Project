using Demo.BLL.InterFace;
using Demo.DAL.Entites;
using Demo.PL.Const;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Demo.PL.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var movie = await _unitOfWork.Movie.GetAll();
            return View(movie);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.GenreList = await _unitOfWork.Genre.GetAll();
            ViewBag.DirectorList = await _unitOfWork.Director.GetAll();
            ViewBag.ActorList = await _unitOfWork.Actor.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel newModel, List<int> actorsIDs)
        {
            if(ModelState.IsValid)
            {
                if ((newModel.GenreID == 0) || (newModel.DirectorID == 0))
                    ModelState.AddModelError("", "Please Select Genre And Director");


                string ImageString = DocumentSetting.UploadFile(newModel.ImageFile, "MoviePosters");
                string videoString = DocumentSetting.UploadFile(newModel.VideoFile, "MovieVideos");

                var movie = new Movie();
                movie.Name = newModel.Name;
                movie.ImageName = ImageString;
                movie.VideoName= videoString;
                movie.GenreID = newModel.GenreID;
                movie.DirectorID = newModel.DirectorID;

                _unitOfWork.Movie.Create(movie);
                int count = await _unitOfWork.Complete();

                if(count > 0)
                {

                foreach (var item in actorsIDs)
                {
                    var actorId = await _unitOfWork.Actor.GetById(item);
                    if (actorId != null)
                    {
                        var MovieActor = new MovieActor() { MovieID = movie.ID, ActorID = actorId.ID };
                        _unitOfWork.MoviesActors.Create(MovieActor);
                        await _unitOfWork.Complete();
                    }
                }
                }

                TempData[NotificationMode.Successflly.ToString()] = "Movie Added Successflly...";
                return RedirectToAction("Index");

            }

            TempData[NotificationMode.ErrorData.ToString()] = "SomeThing Wrong....";
            ViewBag.GenreList = await _unitOfWork.Genre.GetAll();
            ViewBag.DirectorList = await _unitOfWork.Director.GetAll();
            return View(newModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _unitOfWork.Movie.GetById(id);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id, Movie newModel)
        {
            if (id != newModel.ID)
                return BadRequest();
            if (newModel is null)
                return BadRequest();


            _unitOfWork.Movie.Delete(newModel);
            int count = await _unitOfWork.Complete();

            if (count > 0)
            {
                DocumentSetting.DeleteFile(newModel.ImageName, "MoviePosters");
                DocumentSetting.DeleteFile(newModel.VideoName, "MovieVideos");
            }

            TempData["DeletedData"] = "Item Deleted Successfully....";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _unitOfWork.Movie.GetById(id);
            var actorMovie =  _unitOfWork.MoviesActors.GetMovieActorByMovieID(id);
            MovieDetailsViewModel VM = new() {Movie = movie, movieActors = actorMovie };

            return View(VM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _unitOfWork.Movie.GetById(id);
            EditMovieViewModel VM = new EditMovieViewModel() {Id = movie.ID , Name = movie.Name, DirectorID = movie.ID , GenreID = movie.GenreID, ImageName = movie.ImageName, VideoName = movie.VideoName};

            ViewBag.GenreList = await _unitOfWork.Genre.GetAll();
            ViewBag.DirectorList = await _unitOfWork.Director.GetAll();
            ViewBag.MovieActorList = _unitOfWork.MoviesActors.GetMovieActorByMovieID(id);
            ViewBag.AllActors = await _unitOfWork.Actor.GetAll();
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , EditMovieViewModel newModel, List<int> oldActors , List<int> newActors)
        {
            if (id != newModel.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                if ((newModel.GenreID == 0) || (newModel.DirectorID == 0))
                {
                    ModelState.AddModelError("", "Please Select Genre and Director");
                    ViewBag.GenreList = await _unitOfWork.Genre.GetAll();
                    ViewBag.DirectorList = await _unitOfWork.Director.GetAll();
                    return View(newModel);
                }

                DocumentSetting.DeleteFile(newModel.ImageName, "MoviePosters");
                DocumentSetting.DeleteFile(newModel.VideoName, "MovieVideos");

                string ImageString = DocumentSetting.UploadFile(newModel.ImageFile, "MoviePosters");
                string VideoString = DocumentSetting.UploadFile(newModel.VideoFile, "MovieVideos");

                var movie = await _unitOfWork.Movie.GetById(id);
                movie.Name = newModel.Name;
                movie.ImageName = ImageString; 
                movie.VideoName = VideoString;
                movie.GenreID = newModel.GenreID;
                movie.DirectorID = newModel.DirectorID;

                _unitOfWork.Movie.Edit(entity: movie);
                int count = await _unitOfWork.Complete();

                if (count > 0)
                {
                    if (newActors.Count > 0)
                    {
                        foreach (var item in newActors)// {1 3 4}
                        {
                            var ActorId = await _unitOfWork.Actor.GetById(item); // 1 3 
                            var IsExist = _unitOfWork.MoviesActors.GetMovieActorByMovieIdActorId(movie.ID, ActorId.ID); // true
                            if (IsExist == null)
                            {
                                var MovieActor = new MovieActor() { MovieID = movie.ID, ActorID = item };
                                _unitOfWork.MoviesActors.Create(MovieActor);
                                await _unitOfWork.Complete();
                            }
                        }
                        foreach (var item in oldActors) // 5
                        {
                            var ActorId = await _unitOfWork.Actor.GetById(item); // 5
                            var IsExist = _unitOfWork.MoviesActors.GetMovieActorByMovieIdActorId(movie.ID, ActorId.ID);
                            if (IsExist != null)
                            {
                                foreach (var item2 in newActors) // 1 3 4
                                {
                                    if (item != item2)  // 5 != 1
                                    {
                                        _unitOfWork.MoviesActors.Delete(IsExist);
                                        await _unitOfWork.Complete();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MovieActorList = _unitOfWork.MoviesActors.GetMovieActorByMovieID(id);
            ViewBag.AllActors = await _unitOfWork.Actor.GetAll();
            ViewBag.GenreList = await _unitOfWork.Genre.GetAll();
            ViewBag.DirectorList = await _unitOfWork.Director.GetAll();
            return View(newModel);
        }

    }
}
