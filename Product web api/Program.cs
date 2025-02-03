        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // appsettings.json
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json"); // appsettings.Development.json
// Load local.settings.json **only in development mode**
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
}
// Load environment variables (these will override the other settings in production)
builder.Configuration.AddEnvironmentVariables(); // This will read the environment variables and override values

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

    app.Run();
