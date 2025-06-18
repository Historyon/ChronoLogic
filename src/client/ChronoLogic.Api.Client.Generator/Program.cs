using System.Text;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;

using var client = new HttpClient();

var swaggerDocument = await client
    .GetStringAsync("https://localhost:7175/swagger/v1/swagger.json");
var document = await OpenApiDocument.FromJsonAsync(swaggerDocument);

var settings = new CSharpClientGeneratorSettings
{
    ClassName = "ChronoLogicApiClient",
    CSharpGeneratorSettings =
    {
        Namespace = "ChronoLogic.Api.Client",
        JsonLibrary = CSharpJsonLibrary.SystemTextJson
    }
};

var generator = new CSharpClientGenerator(document, settings);
var code = generator.GenerateFile();
await File.WriteAllTextAsync(
    @"..\..\..\..\ChronoLogic.Api.Client\ChronoLogicApiClient.cs", code, Encoding.UTF8);

Console.WriteLine("Generated file");