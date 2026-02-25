using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameCharacters API v1");
    c.RoutePrefix = "swagger"; // /swagger
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/api/characters", () =>
{
    var characters = new[]
    {
        new { id = 1, name = "Mario", role = "Hero Plumber", level = 25 },
new { id = 2, name = "Luigi", role = "Support Jumper", level = 23 },
new { id = 3, name = "Bowser", role = "Boss Tank", level = 30 }
    };

    return Results.Ok(characters);
})
.WithName("GetCharacters");
app.Run();