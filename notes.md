# W01 .NET Applications

## Part 1: Create a Web API with ASP.NET Core Controllers

### Existing Pizzas

````csharp
new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }

### Additional Pizza Record

```markdown
### Additional Pizza Record

```csharp
new Pizza { Id = 3, Name = "Margherita", IsGlutenFree = false }

### API Verification

#### GET
Request:
GET http://localhost:5153/Pizza

Response:
[
  {"id":1,"name":"Classic Italian","isGlutenFree":false},
  {"id":2,"name":"Veggie","isGlutenFree":true},
  {"id":3,"name":"Margherita","isGlutenFree":false}
]

Status Code:
200 OK

#### POST
Request:
POST http://localhost:5153/Pizza

Body:
{"name":"Pepperoni","isGlutenFree":false}

Response:
{"id":4,"name":"Pepperoni","isGlutenFree":false}

Status Code:
201 Created

#### PUT
Request:
PUT http://localhost:5153/Pizza/4

Body:
{"id":4,"name":"Pepperoni Updated","isGlutenFree":true}

Response:
No content returned.

Status Code:
204 No Content

#### DELETE

```markdown
#### DELETE
Request:
DELETE http://localhost:5153/Pizza/4

Response:
No content returned.

Status Code:
204 No Content

## Part 2: Sales Summary Function

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
Sales Summary
----------------------------
Total Sales: $2,012.20

Details:
sales.json: $88.88
201\sales.json: $501.22
202\sales.json: $1,234.22
203\sales.json: $99.00
204\sales.json: $88.88
````
