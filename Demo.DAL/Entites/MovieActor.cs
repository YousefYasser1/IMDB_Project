using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class MovieActor
    {
        public int ID { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        [ForeignKey("Actor")]
        public int ActorID { get; set; }
        public virtual Actor Actor { get; set; }
    }
}
