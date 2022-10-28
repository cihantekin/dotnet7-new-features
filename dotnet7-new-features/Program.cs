using dotnet7_new_features;
using dotnet7_new_features.Json.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // with that way [FromServices] aatribute no longer necessary in the controller
    options.DisableImplicitFromServicesParameters = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/EndpointWithDescription", () => { return "OK"; }).WithDescription("Test description for that endpoint");

app.MapGet("/EndpointWithSummary", [EndpointSummary("Endpoint summary attribute example")] () => { });

// See: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-7-preview-2/#binding-arrays-and-stringvalues-from-headers-and-query-strings-in-minimal-apis
// Bind query string values to a primitive type array
// GET  /tags?q=1&q=2&q=3
app.MapGet("/tags", (int[] q) => $"tag1: {q[0]} , tag2: {q[1]}, tag3: {q[2]}");

// Bind to a string array
// GET /tags?names=john&names=jack&names=jane
app.MapGet("/tags", (string[] names) => $"tag1: {names[0]} , tag2: {names[1]}, tag3: {names[2]}");

// Bind to StringValues
// GET /tags?names=john&names=jack&names=jane
app.MapGet("/tags", (StringValues names) => $"tag1: {names[0]} , tag2: {names[1]}, tag3: {names[2]}");

var options = new JsonSerializerOptions { TypeInfoResolver = new UpperCasePropertyContractResolver() };
var test = JsonSerializer.Serialize(new { value = "uppercase test" }, options);

JsonSerializerOptions optionsExclude = new()
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers = { ExcludeMemberModifier.ExcludeOldMember }
    }
};
ExcludeMemberTestClass testClass = new() { StringProp = "test", StringPropOld = "test2" };

var result = JsonSerializer.Serialize(testClass, optionsExclude);

app.Run();
