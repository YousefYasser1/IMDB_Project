using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class MoviesUser
    {
        public int ID { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }

        public virtual Movie Movie { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
