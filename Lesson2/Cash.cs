using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2
{
    internal interface ICash
    {
        long Id { get; }
        int Denomination { get; }
    }

    internal struct Cash100 : ICash
    {
        public long Id { get; init; }
        public int Denomination { get => 100; }
    }
}
