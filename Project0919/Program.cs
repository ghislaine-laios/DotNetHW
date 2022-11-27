using System.Text.RegularExpressions;

namespace Project0919;

public partial class Program
{
    public static Regex WordRegex = GenerateWordRegex();
    public static Regex NonEmptyRegex = GenerateNonEmptyRegex();
    public static Regex CommentRegex = GenerateCommentRegex();

    public static async Task<Result> Execute(string path)
    {
        var originalLines = await ReadLines(path);
        var (originalLineNum, originalWordNum) = CountLinesAndWords(originalLines);
        var formattedLines = FormatLines(originalLines);
        var formattedWords = GetWords(formattedLines);
        var (formattedLineNum, formattedWordNum) = CountLinesAndWords(formattedLines, formattedWords);
        var occurrences =
            (from occurrence in GetOccurrencesNum(formattedWords)
                select (occurrence.Key, occurrence.Value)).ToList();
        var result = new Result
        {
            Occurrences = occurrences,
            Original =
            {
                LineCount = originalLineNum,
                WordCount = originalWordNum
            },
            Formatted =
            {
                LineCount = formattedLineNum,
                WordCount = formattedWordNum
            }
        };
        return result;
    }

    private static async Task<IList<string>> ReadLines(string path)
    {
        using var sr = new StreamReader(path);
        var result = new List<string>();
        while (!sr.EndOfStream) result.Add(await sr.ReadLineAsync() ?? throw new InvalidOperationException());
        return result;
    }

    private static (int, int) CountLinesAndWords(IList<string> lines)
    {
        var words = GetWords(lines);
        return CountLinesAndWords(lines, words);
    }

    private static (int, int) CountLinesAndWords(IList<string> lines, IList<string> words)
    {
        return (lines.Count, words.Count);
    }

    private static IDictionary<string, int> GetOccurrencesNum(IList<string> words)
    {
        var result = new Dictionary<string, int>();
        foreach (var word in words)
        {
            var hasKey = result.ContainsKey(word);
            if (hasKey) result[word]++;
            else result[word] = 1;
        }

        return result;
    }

    private static IList<string> FormatLines(IList<string> lines)
    {
        var result = new List<string>();
        foreach (var line in lines)
        {
            if (!NonEmptyRegex.IsMatch(line)) continue;
            var commentMatch = CommentRegex.Match(line);
            if (commentMatch.Success)
            {
                var commentSymbolIndex = commentMatch.Index;
                var validStr = line.Substring(0, commentSymbolIndex);
                if (!NonEmptyRegex.IsMatch(validStr)) continue;
                result.Add(validStr);
            }
            else
            {
                result.Add(line);
            }
        }

        return result;
    }

    private static IList<string> GetWords(IList<string> lines)
    {
        var result = new List<string>();
        foreach (var line in lines)
        {
            var matches = WordRegex.Matches(line);
            result.AddRange(from match in matches select match.Value);
        }

        return result;
    }

    [GeneratedRegex(@"([\x30-\x39]|[\x41-\x5A]|[\x61-\x7A]|_)+")]
    private static partial Regex GenerateWordRegex();

    [GeneratedRegex(@"^(?!\s*$).+")]
    private static partial Regex GenerateNonEmptyRegex();

    [GeneratedRegex(@"\/\/")]
    private static partial Regex GenerateCommentRegex();

    public class Result
    {
        public Statistic Original { get; } = new();
        public Statistic Formatted { get; } = new();
        public required IList<(string, int)> Occurrences { get; set; }

        public class Statistic
        {
            public int LineCount { get; set; }
            public int WordCount { get; set; }
        }
    }
}