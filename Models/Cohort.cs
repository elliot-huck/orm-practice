using System.Collections.Generic;

namespace nss.Data
{
    public class Cohort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<Student> Students { get; set; } = new HashSet<Student>();
        public HashSet<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
    }

}