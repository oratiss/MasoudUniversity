using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MasoudUniversity.Models
{
    public class Class
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Location")]
        [Required]
        public string Location { get; set; }
        [Display(Name = "Teacher")]
        [Required]
        public string TeacherName { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public Class()
        {

        }
    }
}
