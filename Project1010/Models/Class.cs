using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1010.Models
{
    public class Class
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required int StartYear { get; set; }
        public required IList<Student> Students { get; set; }
    }
}
