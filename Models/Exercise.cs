using System.Collections.Generic;

namespace nss.Data
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public HashSet<Student> AssignedStudents { get; set; }
    }

}