using Carsales.Services;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
string apiBaseUrl = Env.GetString("API_BASE_URL");
string urlFront = Env.GetString("URL_FRONT");

builder.Services.Configure<AppSettings>(options =>
{
    options.ApiBaseUrl = apiBaseUrl;
});
builder.Services.AddControllers();
builder.Services.AddHttpClient<RickAndMortyService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins(urlFront)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
