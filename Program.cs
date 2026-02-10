
var options = new EnumerationOptions
{
    RecurseSubdirectories = true,
    AttributesToSkip = FileAttributes.None, 
    IgnoreInaccessible = true 
};

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
        var dirSize = dirInfo.EnumerateFiles("*", options) //SearchOption.AllDirectories
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
    Console.WriteLine($"{name,-20}|{FormatSize(size), 20}");
    Console.WriteLine($"--------------------------------------------");
}
return 0;


static string FormatSize(long number)
{
    if(number <= 0)
        return "0 octets";
    
    string[] units = ["octets","Ko", "Mo", "Go", "To"];
    int divIterations = (int) Math.Log(number, 1024);
    int index = (divIterations<units.Length)? divIterations : units.Length-1;

    double size = number / Math.Pow(1024, index);

    return $"{size:N2} {units[index]}";
}