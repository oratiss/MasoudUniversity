using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasoudUniversity.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public int ClassID { get; set; }
        public int StudentID { get; set; }

        //navigation properties
        [ForeignKey("ClassID")]
        public Class Class { get; set; }
        [ForeignKey("StudentID")]
        public Student Student { get; set; }

        public Enrollment()
        {

        }
    }
}
