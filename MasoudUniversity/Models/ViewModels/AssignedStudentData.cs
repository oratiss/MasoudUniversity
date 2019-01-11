using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasoudUniversity.Models.ViewModels
{
    public class AssignedStudentData
    {
        public int SrudentID { get; set; }
        public string StudentFullName { get; set; }
        public bool Assigned { get; set; }
    }
}
