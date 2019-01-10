using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasoudUniversity.Models;


namespace MasoudUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
             new Student{StudentFullName="David Jackson",Age=18,GPA=3.4},
             new Student{StudentFullName="Peter Parker",Age=18,GPA=3.4},
             new Student{StudentFullName="Robert Smith",Age=17,GPA=3.4},
             new Student{StudentFullName="Olivia Dale",Age=19,GPA=3.4},
             new Student{StudentFullName="Barak Trump",Age=19,GPA=3.4},
             new Student{StudentFullName="Donalad SpilBurg",Age=18,GPA=3.4},
             new Student{StudentFullName="Sarah Borneroth",Age=18,GPA=3.4},
             new Student{StudentFullName="Abel moouche",Age=18,GPA=3.4}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var Classes = new Class[]
            {
            new Class{Id=1050,Title="Chemistry",Location="Building A, Room 102", TeacherName="Mr. Branson" },
            new Class{Id=4022,Title="Microeconomics",Location="Building A, Room 105",TeacherName="Mr. Pateriani"},
            new Class{Id=4041,Title="Macroeconomics",Location="Building A, Room 102",TeacherName="Mr. Jackson"},
            new Class{Id=1045,Title="Calculus",Location="Building A, Room 103",TeacherName="Mr. Bale"},
            new Class{Id=3141,Title="Trigonometry",Location="Building A, Room 88",TeacherName="Mr. Atkinson"},
            new Class{Id=2021,Title="Composition",Location="Building A, Room 95",TeacherName="Ms. Pacino"},
            new Class{Id=2042,Title="Literature",Location="Building A, Room 212",TeacherName="Mr. Deniro"}
            };
            foreach (Class c in Classes)
            {
                context.Classes.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,ClassID=1050},
            new Enrollment{StudentID=1,ClassID=4022},
            new Enrollment{StudentID=1,ClassID=4041},
            new Enrollment{StudentID=2,ClassID=1045},
            new Enrollment{StudentID=2,ClassID=3141},
            new Enrollment{StudentID=2,ClassID=2021},
            new Enrollment{StudentID=3,ClassID=1050},
            new Enrollment{StudentID=4,ClassID=1050},
            new Enrollment{StudentID=4,ClassID=4022},
            new Enrollment{StudentID=5,ClassID=4041},
            new Enrollment{StudentID=6,ClassID=1045},
            new Enrollment{StudentID=7,ClassID=3141}
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }


    }
}