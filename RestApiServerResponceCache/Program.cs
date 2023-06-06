using RestApiServerResponceCache.RateLimiter;
using RestApiServerResponceCache.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLimiter(requestLimit: 10, windowSize: TimeSpan.FromSeconds(10));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHttpResponseCache, HttpResponseCache>();

builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseLimiter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCaching();


app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
    {
        Public = true,
        MaxAge = TimeSpan.FromSeconds(10)
    };

    await next();

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
