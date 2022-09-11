using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_1
{
    internal class PrimeNumberAsserter
    {
        public PrimeNumberAsserter(int max)
        {
            _max = max;
            _primes = new HashSet<int>();
            var primes = new bool[max];
            Array.Fill(primes, true);
            var sqrt = Math.Sqrt(_max);
            for (int i = 2; i < sqrt; i++)
            {
                if (primes[i])
                {
                    for (int j = i * i; j < max; j += i)
                    {
                        primes[j] = false;
                    }
                }
            }
            for (int i = 2; i < primes.Length; i++)
            {
                if (primes[i]) _primes.Add(i);
            }
        }

        private int _max;

        private ISet<int> _primes;

        public ISet<int> Primes
        {
            get { return _primes; }
        }

    }
}
