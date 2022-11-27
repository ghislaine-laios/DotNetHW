using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1010.Models
{
    public enum Gender
    {
        Male, Female
    }

    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Gender Gender { get; set; }
        public required string? Note { get; set; }
    }
}
