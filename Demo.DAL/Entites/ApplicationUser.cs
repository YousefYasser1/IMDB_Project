using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    [Table(name: "Users")]
    public class ApplicationUser: IdentityUser<int>
    {

        public int Age { get; set; }
        public string? Address { get; set; }
        public string? ImageName { get; set; }

        public virtual ICollection<LikeMovie> Likes { get; set; }
        public virtual List<MoviesUser> MoviesUsers { get; set; }
    }
}
