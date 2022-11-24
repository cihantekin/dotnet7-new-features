using dotnet7_new_features.Cache;
using dotnet7_new_features.EndpointFilters;
using dotnet7_new_features.Json.Text;
using dotnet7_new_features.Model;
using dotnet7_new_features.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(e =>
{
    e.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All; //Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestBody;
    e.RequestBodyLogLimit = 1024;
});
builder.Services.AddProblemDetails(CustomizeProblemDetails);

builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);
static void CustomizeProblemDetails(ProblemDetailsOptions obj)
{
    obj.CustomizeProblemDetails = action => action.ProblemDetails.Extensions.Add("customAddition", Environment.MachineName);
}

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // with that way [FromServices] aatribute no longer necessary in the controller
    options.DisableImplicitFromServicesParameters = true;
});

builder.Services.AddDbContext<QueryEnhancementsContext>(opt => opt.UseInMemoryDatabase("TestDb"));
builder.Services.AddScoped<QueryEnhancementsContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Using with problem details send user interface a json like not found or what is the error...
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseAuthorization();

app.MapControllers();

app.UseResponseCaching();

app.MapGet("/EndpointWithDescription", () => { return "OK"; }).WithDescription("Test description for that endpoint");

app.MapGet("/EndpointWithSummary", [EndpointSummary("Endpoint summary attribute example")] () => { });

// when you hit the load button it's coming from the cache for 5 seconds
app.MapGet("/ResponseCaching", (int? size, HttpContext context) =>
{
    const string type = "monsterid";
    size ??= 200;
    var hash = Guid.NewGuid().ToString("n");

    if (context.Features.Get<IResponseCachingFeature>() is { } responseCaching)
    {
        responseCaching.VaryByQueryKeys = new[] { "size" };
        context.Response.GetTypedHeaders().CacheControl = new()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(5)
        };
    }

    var html = $"""
                    <img src="https://www.gravatar.com/avatar/{hash}?s={size}&d={type}" width="{size}" /><pre>Generated at {DateTime.Now:O}</pre><a href="/ResponseCaching?size={size}">Load</a>
                """;

    return Results.Text(html, "text/html");
});

//app.MapGet("/problem", () => Results.Problem("that is a problem")).AddEndpointFilter<ProblemDetailsServiceEndpointFilter>();

// See: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-7-preview-2/#binding-arrays-and-stringvalues-from-headers-and-query-strings-in-minimal-apis
// Bind query string values to a primitive type array
// GET  /tags?q=1&q=2&q=3
//app.MapGet("/tags", (int[] q) => $"tag1: {q[0]} , tag2: {q[1]}, tag3: {q[2]}");

//// Bind to a string array
//// GET /tags?names=john&names=jack&names=jane
//app.MapGet("/tags", (string[] names) => $"tag1: {names[0]} , tag2: {names[1]}, tag3: {names[2]}");

//// Bind to StringValues
//// GET /tags?names=john&names=jack&names=jane
//app.MapGet("/tags", (StringValues names) => $"tag1: {names[0]} , tag2: {names[1]}, tag3: {names[2]}");

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

var options2 = new JsonSerializerOptions
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers =
        {
            static typeInfo =>
            {
                if (typeInfo.Kind != JsonTypeInfoKind.Object)
                    return;

                foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
                {
                    // strip IsRequired constraint from every property
                    // because for now required properties not supported
                    propertyInfo.IsRequired = false;
                }
            }
        }
    }
};

var memCacheStatistics = new dotnet7_new_features.Cache.MemoryCacheStatistics();

// endpoint for endpoints filter

app.MapPost("/TestEndpointFilter", ([FromBody] RegisterCustomerRequest customer) =>
{
    return Results.Ok();
}).AddEndpointFilter<ValidationFilter<RegisterCustomerRequest>>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QueryEnhancementsContext>();
    var queryEnc = new QueryEnhancements(dbContext);
    queryEnc.Test();
}


app.UseHttpLogging();

app.Run();
