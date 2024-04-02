using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites
{
    public class Actor
    {
        public int ID { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string FullName{ get; set; }
        public string ImageName { get; set; }

        public virtual List<MovieActor> MovieActors { get; set; }
        
    }
}
