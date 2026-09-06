using Newtonsoft.Json;
using System.Text;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

// File created by the Microsoft Learn module
File.WriteAllText(
    Path.Combine(salesTotalDir, "totals.txt"),
    $"{salesTotal}{Environment.NewLine}"
);

// Additional CSE 325 requirement
GenerateSalesSummary(
    salesFiles,
    salesTotal,
    Path.Combine(salesTotalDir, "sales-summary.txt"),
    storesDirectory
);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(
        folderName,
        "*",
        SearchOption.AllDirectories
    );

    foreach (var file in foundFiles)
    {
        if (Path.GetFileName(file)
        .Equals("sales.json", StringComparison.OrdinalIgnoreCase))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);

        SalesData? data =
            JsonConvert.DeserializeObject<SalesData?>(salesJson);

        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

void GenerateSalesSummary(
    IEnumerable<string> salesFiles,
    double salesTotal,
    string outputFile,
    string storesDirectory)
{
    var report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {salesTotal:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);

        SalesData? data =
            JsonConvert.DeserializeObject<SalesData?>(salesJson);

        double fileTotal = data?.Total ?? 0;

        string fileName =
            Path.GetRelativePath(storesDirectory, file);

        report.AppendLine($"{fileName}: {fileTotal:C}");
    }

    File.WriteAllText(outputFile, report.ToString());
}

record SalesData(double Total);