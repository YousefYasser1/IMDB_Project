using Demo.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser,ApplicationRole, int>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }



        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<LikeMovie> LikesMovie { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<MovieComment> MovieComments { get; set; }
        public virtual DbSet<MovieActor> MoviesActors { get; set; }
        public virtual DbSet<MoviesUser> MoviesUsers { get; set; }
    }
}
