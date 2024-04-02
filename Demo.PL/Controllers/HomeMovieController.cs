using Demo.BLL.InterFace;
using Demo.DAL.Entites;
using Demo.PL.Const;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.PL.Controllers
{
    public class HomeMovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeMovieController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string seacrh)
        {
            if(seacrh == null)
            {
                var movies = await _unitOfWork.Movie.GetAll();
                var user = await _userManager.GetUserAsync(User);
                var MoviesID = _unitOfWork.MovieUser.MoviesIds(user.Id);

                if (MoviesID == null)
                {
                    return View(movies);
                }

                var VM = new HomeMovieViewModel() { Movies = movies, MoviesIDs = MoviesID };


                return View(VM);
            }
            else
            {
               string namWithoutTrim =  seacrh.TrimStart();
                var movies =  _unitOfWork.Movie.GetListByName(namWithoutTrim);
                var user = await _userManager.GetUserAsync(User);
                var MoviesID = _unitOfWork.MovieUser.MoviesIds(user.Id);

                if (MoviesID == null)
                {
                    return View(movies);
                }

                var VM = new HomeMovieViewModel() { Movies = movies, MoviesIDs = MoviesID };


                return View(VM);
            }

        }




        public async Task<IActionResult> DisplayVideo(int id)
        {
            var movie = await _unitOfWork.Movie.GetById(id);

            var user = await _userManager.GetUserAsync(User);
            var listLikes = await _unitOfWork.LikeMovie.GetAll();


            var listComment = _unitOfWork.MovieComment.GetCommentOfMovie(id);

            LikeMovie isExistingLike = _unitOfWork.LikeMovie.GetFirstBasedOnCondition(id, user.Id);
            DisplayMovieViewModel VM = new();
            if (isExistingLike == null)
            {
                VM.ID = movie.ID; VM.ImageName = movie.ImageName; VM.VideoName = movie.VideoName; VM.Director = movie.Director; VM.Genre = movie.Genre; VM.UserHasLike = false; VM.Likes = _unitOfWork.LikeMovie.GetNumberLikes(listLikes, id); VM.Comments = listComment;
                return View(VM);
            }
            else
            {
                VM.ID = movie.ID; VM.ImageName = movie.ImageName; VM.VideoName = movie.VideoName; VM.Director = movie.Director; VM.Genre = movie.Genre; VM.UserHasLike = true; VM.Likes = _unitOfWork.LikeMovie.GetNumberLikes(listLikes, id); VM.Comments = listComment;
                return View(VM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int id, string Comment)
        {
            var movie = await _unitOfWork.Movie.GetById(id);
            if (User.IsInRole(RoleNames.User.ToString()))
            {
                if (Comment != null)
                {

                    var user = await _userManager.GetUserAsync(User);
                    var comment = new MovieComment() { MovieID = id, UserId = user.Id ,Comment = Comment };
                   _unitOfWork.MovieComment.Create(comment);
                   await _unitOfWork.Complete();

                return RedirectToAction(nameof(DisplayVideo), new { id = movie.ID });
                }
            }

            return RedirectToAction("Login", "Account");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike(int id)
        {
            if(User.IsInRole(RoleNames.User.ToString()))
            {
                var user = await _userManager.GetUserAsync(User);

                var like = new LikeMovie() { MovieID = id , UserId = user.Id};
                _unitOfWork.LikeMovie.Create(like);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(DisplayVideo), new { id = id });
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DisLike(int id)
        {
            if (User.IsInRole(RoleNames.User.ToString()))
            {
                var user = await _userManager.GetUserAsync(User);

                LikeMovie isExistingLike = _unitOfWork.LikeMovie.GetFirstBasedOnCondition(id, user.Id);

                if (isExistingLike == null)
                {
                    return NotFound();
                }

                _unitOfWork.LikeMovie.Delete(isExistingLike);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(DisplayVideo), new { id = id });
            }

            return RedirectToAction("Login", "Account");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFavoriteMovie(int id) // 1
        {
            if (User.IsInRole(RoleNames.User.ToString()))
            {
                var user = await _userManager.GetUserAsync(User); // GetUser  ID = 1
                var movie = await _unitOfWork.Movie.GetById(id); // User

                if((user == null) || (movie==null))
                    return BadRequest();

                var MovieUser =  _unitOfWork.MovieUser.GetItemByUserIdMovieId(movie.ID, user.Id); // false

                if (MovieUser == null)
                {
                    var newMovieUser = new MoviesUser() { MovieID = movie.ID, UserId = user.Id };
                    _unitOfWork.MovieUser.Create(newMovieUser);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFavoriteMovie(int id)
        {
            if (User.IsInRole(RoleNames.User.ToString()))
            {
                var user = await _userManager.GetUserAsync(User);
                var movie = await _unitOfWork.Movie.GetById(id);

                var MovieUser = _unitOfWork.MovieUser.GetItemByUserIdMovieId(movie.ID, user.Id);

                if (MovieUser != null)
                {
                    _unitOfWork.MovieUser.Delete(MovieUser);
                    await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }

            }
            return BadRequest();
        }



        public async Task<IActionResult> DisplayFavoriteMovie()
        {

            var user = await _userManager.GetUserAsync(User);

            var moviesId =  _unitOfWork.MovieUser.MoviesIds(user.Id); // {2 , 6}

            var AllMoviesIDs =  _unitOfWork.Movie.GetMovieIDs();  // {1 , 2 , 3 , 4 , 5}


            List<Movie> movies = new List<Movie>();

            for(int i = 0; i < moviesId.Count; i++) // 2
            {
                if (AllMoviesIDs.Contains(moviesId[i]))
                {
                    var movie = await _unitOfWork.Movie.GetById(moviesId[i]);
                    movies.Add(movie);
                }
            }

            return View(movies) ;
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _unitOfWork.MovieComment.GetById(id);

            var movieId = comment.MovieID;

            _unitOfWork.MovieComment.Delete(comment);
            await _unitOfWork.Complete();

            return RedirectToAction(nameof(DisplayVideo), new {id = movieId});
        }
    }
}
