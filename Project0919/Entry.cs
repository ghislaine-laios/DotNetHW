using Project0919;

var result = await Project0919.Program.Execute("./Tests/Assets/HelloWorld.cs");
Console.WriteLine($"原始行数: {result.Original.LineCount}");
Console.WriteLine($"原始单词数: {result.Original.WordCount}");
Console.WriteLine($"格式化后行数: {result.Formatted.LineCount}");
Console.WriteLine($"格式化后单词数: {result.Formatted.WordCount}");
Console.WriteLine($"单词出现次数: ");
foreach (var resultOccurrence in result.Occurrences)
{
    Console.WriteLine(resultOccurrence);
}