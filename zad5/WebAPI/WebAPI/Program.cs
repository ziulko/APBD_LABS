using Microsoft.EntityFrameworkCore;
using WebAPI.Classes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v3" });
});

builder.Services.AddDbContext<AnimalContext>(options =>
    options.UseInMemoryDatabase("AnimalDatabase"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AnimalContext>();
        context.Database.EnsureCreated();

        if (!context.Animals.Any())
        {
            context.Animals.Add(new Animal
            {
                Name = "Burek",
                Category = "Pies",
                Breed = "Golden Retriever",
                Color = "Złoty"
            });

            context.SaveChanges();
        }

        if (!context.Visits.Any())
        {
            context.Visits.Add(new Visit
            {
                Date = DateTime.Now,
                AnimalId = 1, // Id zwierzęcia, do którego należy wizyta
                Description = "Wizyta kontrolna",
                Price = 50.00m
            });

            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v3"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); // Mapowanie kontrolerów

app.Run();
