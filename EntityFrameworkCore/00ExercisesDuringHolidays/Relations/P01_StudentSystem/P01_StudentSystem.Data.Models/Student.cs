using System;
using System.Collections.Generic;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public Student()
        {
            Homeworks = new HashSet<Homework>();
        }

        public int StudentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Homework> Homeworks { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
