using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasoudUniversity.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        public string StudentFullName { get; set; }

        public string FamilyName
        {
            get
            {
                string[] Names = this.StudentFullName.Split(' ');
                return Names[1];
            }
        }


        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "GPA")]
        public double GPA { get; set; }

        //navigation Properties
        public ICollection<Enrollment> Enrollments { get; set; }

        public Student()
        {

        }

    }
}
