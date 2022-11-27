using Xunit.Abstractions;

namespace Project0919.Tests;

public class UnitTest1
{
    private const string AssetsPath = "./Tests/Assets";
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public static IEnumerable<object[]> CanProgramWorkData => new List<object[]>
    {
        new object[]
        {
            Path.Join(AssetsPath, "HelloWorld.cs"),
            new Program.Result
            {
                Original = { LineCount = 14, WordCount = 20 },
                Formatted = { LineCount = 10, WordCount = 14 },
                Occurrences = new List<(string, int)>
                {
                    ("namespace", 1),
                    ("HelloWorld", 2),
                    ("class", 1),
                    ("Hello", 2),
                    ("static", 1),
                    ("void", 1),
                    ("string", 1),
                    ("args", 1),
                    ("System", 1),
                    ("Console", 1),
                    ("WriteLine", 1),
                    ("World", 1)
                }
            }
        }
    };

    [Fact]
    public void DemoTestCase1()
    {
        _testOutputHelper.WriteLine(Directory.GetCurrentDirectory());
    }

    [Theory]
    [MemberData(nameof(CanProgramWorkData))]
    public async Task CanProgramWork(string path, Program.Result expectedResult)
    {
        var result = await Program.Execute(path);
        Assert.Equivalent(result.Original, expectedResult.Original);
        Assert.Equivalent(result.Formatted, expectedResult.Formatted);
        var occurrences = result.Occurrences.ToHashSet();
        var expectedOccurrences = expectedResult.Occurrences.ToHashSet();
        Assert.Equivalent(occurrences, expectedOccurrences);
    }
}