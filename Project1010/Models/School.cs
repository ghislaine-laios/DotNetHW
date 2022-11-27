using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1010.Models
{
    public class School
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required IList<Class> Classes { get; set; }
    }
}
