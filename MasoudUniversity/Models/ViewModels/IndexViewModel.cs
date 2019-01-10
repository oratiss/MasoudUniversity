using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasoudUniversity.Models.ViewModels
{
    public class SchoolIndexData
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Class> Classes { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
