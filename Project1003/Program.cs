using System.Collections.Concurrent;
using System.Net;
using System.Text.RegularExpressions;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

Console.WriteLine("请输入关键字：");
var keyword = Console.ReadLine();
if (keyword is null) return;
var searchStr = $"https://www.google.com/search?q={keyword}";
var urls = new ConcurrentQueue<string>();
var urlsSemaphore = new SemaphoreSlim(0);
for (int i = 0; i < 50; i++)
{
    urls.Enqueue($"{searchStr}&start={i*10}");
    urlsSemaphore.Release();
}
var visitedUrls = new ConcurrentDictionary<string, byte>();
var result = new ConcurrentDictionary<(string, string), byte>();
var tasks = new List<Task>();
for (int i = 0; i < 10; i++)
{
    tasks.Add(Work(i));
}

await Task.WhenAll(tasks.ToArray());

async Task Work(int workerIndex)
{
    Console.WriteLine($"[{workerIndex}] Start work.");
    var webClient = new HttpClient();
    var urlPattern = @"(?<url>http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?)";
    var phonePattern = @"0[\d]{2}-[\d]{8}";
    var searchUrlPattern = @"www\.google";
    var urlRegex = new Regex(urlPattern);
    var phoneRegex = new Regex(phonePattern);
    var searchRegex = new Regex(searchUrlPattern);
    var random = new Random();
    await Task.Delay(100 * random.Next(1, 40));
    for (int i = 0; i < 5000 && result!.Count <= 100; i++)
    {
        try
        {
            await urlsSemaphore.WaitAsync();
            var anything = urls!.TryDequeue(out var url);
            if (!anything) break;
            if (visitedUrls!.ContainsKey(url!)) continue;
            visitedUrls![url!] = 1;
            if (url!.Contains("google"))
            {
                if (!searchRegex.IsMatch(url)) continue;
                await Task.Delay(500 * random.Next(1, 10));
            }
            Console.WriteLine($"===爬取链接=({workerIndex}, {i})");
            Console.WriteLine(url);
            Console.WriteLine();
            var str = await webClient.GetStringAsync(url);
            var urlMatches = urlRegex.Matches(str);
            foreach (Match urlMatch in urlMatches)
            {
                var nextUrl = urlMatch.Value;
                urls.Enqueue(nextUrl);
                urlsSemaphore.Release();
            }

            var phoneMatches = phoneRegex.Matches(str);
            foreach (Match phoneMatch in phoneMatches)
            {
                var phone = phoneMatch.Value;
                result![(phone, url!)] = 1;
                Console.WriteLine($"找到电话: {phone}， 来自: {url}");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"[ERROR] {e}");
        }
    }
    Console.WriteLine($"[{workerIndex}] Work end.");
}
Console.Write("\n\n\n");
var phones = result.Keys;
foreach (var phone in phones)
{
    Console.WriteLine(phone);
}