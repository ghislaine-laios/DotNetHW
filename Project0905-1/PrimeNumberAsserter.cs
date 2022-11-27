namespace Lesson1_1;

internal class PrimeNumberAsserter
{
    private readonly int _max;

    public PrimeNumberAsserter(int max)
    {
        _max = max;
        Primes = new HashSet<int>();
        var primes = new bool[max];
        Array.Fill(primes, true);
        var sqrt = Math.Sqrt(_max);
        for (var i = 2; i < sqrt; i++)
            if (primes[i])
                for (var j = i * i; j < max; j += i)
                    primes[j] = false;
        for (var i = 2; i < primes.Length; i++)
            if (primes[i])
                Primes.Add(i);
    }

    public ISet<int> Primes { get; }
}