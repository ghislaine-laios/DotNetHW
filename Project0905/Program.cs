using Lesson1_1;

Console.WriteLine("Input the number: ");
string? input = null;
while (input is null)
{
    input = Console.ReadLine();
}
int inputNumber = Int32.Parse(input);
Console.WriteLine("All prime factors of this number are: ");
foreach (var number in findPrimeFactor(inputNumber))
{
    Console.Write(number);
    Console.Write(" ");
}

static IList<int> findPrimeFactor(int number)
{
    ISet<int> result = new HashSet<int>();
    var sqrt = (int)Math.Ceiling(Math.Sqrt(number));
    for (int i = 2; i <= sqrt; i++)
    {
        if (number % i == 0)
        {
            result.Add(i);
        }
    }
    var primerNumberAsserter = new PrimeNumberAsserter(sqrt);
    result.UnionWith(primerNumberAsserter.Primes);
    return result.ToList<int>();
}
