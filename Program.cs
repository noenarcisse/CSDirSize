
string currDir = Directory.GetCurrentDirectory();
var subdirs = Directory.EnumerateDirectories(currDir).ToList();

if (subdirs.Count <= 0)
    return 1;

var results = new List<(string name, long size)>();
foreach (string d in subdirs)
{
    try
    {
        var dirInfo = new DirectoryInfo(d);
        var dirSize = dirInfo.EnumerateFiles("*", SearchOption.AllDirectories)
                                                                            .Sum(file => file.Length);
        results.Add((Path.GetFileName(d), dirSize));
    }
    catch
    {
        Console.WriteLine($"{d} => Acces refusé.");
    }
}


var sorted = results.OrderByDescending(r => r.size);

Console.WriteLine($"CURR DIR: {currDir}");
Console.WriteLine($"--------------------------------------------");
foreach ((string name, long size) in sorted)
{
    Console.WriteLine($"{name,-20}|{size / 1024d,10:N0} ko");
    Console.WriteLine($"--------------------------------------------");
}
return 0;