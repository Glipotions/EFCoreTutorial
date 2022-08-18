using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorial.Data.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }


        public int Number { get; set; }


        public int AddressId { get; set; }

        //public virtual StudentAddress Address { get; set; }

        //public virtual ICollection<Book> Books { get; set; }


        //public virtual ICollection<Course> Courses { get; set; }
    }
}
