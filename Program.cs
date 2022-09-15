using System.Diagnostics;

async void Test1()
{
    var sw = Stopwatch.StartNew();
    byte s = 0;
    for (var i = 0; i < 1_000; i++) Delay();


    sw.Stop();
    Console.WriteLine(sw.ElapsedMilliseconds);

    await Task.Delay(TimeSpan.FromSeconds(600));


    async Task Delay()
    {
        var strings = new string[1024 * 64];
        for (var i = 0; i < strings.Length; i++)
        {
            strings[i] = s.ToString();
            s++;
        }

        await Task.Delay(TimeSpan.FromSeconds(30));
    }
}

void Test2()
{
    var n = 1000000;
    var tasks = new Task[n];
    var sw = Stopwatch.StartNew();
    for (var i = 0; i < n; i++)
        tasks[i] = Task.Run(async () =>
            await Task.Delay(TimeSpan.FromSeconds(1)));
    Task.WaitAll(tasks);
    sw.Stop();
    Console.WriteLine(sw.Elapsed.TotalSeconds);
}

async void Test3()
{
    for (var i = 0; i < 1_000; i++) Delay();
    await Task.Delay(TimeSpan.FromSeconds(60));

    async void Delay()
    {
        var bytes = new byte[1024 * 1024];
        await Task.Delay(TimeSpan.FromSeconds(10));
        Console.WriteLine(bytes.Length);
    }
}