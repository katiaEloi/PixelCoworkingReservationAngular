using Microsoft.EntityFrameworkCore;
using PixelCoworking.Api.Data;
using PixelCoworking.Api.Models;
using PixelCoworking.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddPolicy("angular", p => p
        .WithOrigins("http://localhost:4200", "http://localhost:4201")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddDbContext<PixelCoworkingDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Swagger solo en Development (lo típico)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // ✅ “bonito”: redirigir a HTTPS solo en producción
    app.UseHttpsRedirection();
}

app.UseCors("angular");

// Endpoints
// GET
app.MapGet("/api/spaces", async (PixelCoworkingDbContext db) =>
    await db.Spaces.AsNoTracking().OrderBy(x => x.Id).ToListAsync());

// GET by Id
app.MapGet("/api/spaces/{id:int}", async (PixelCoworkingDbContext db, int id) =>
{
    var space = await db.Spaces.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    return space is null ? Results.NotFound() : Results.Ok(space);
});

// POST
app.MapPost("/api/spaces", async (PixelCoworkingDbContext db, SpaceCreateDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name))
        return Results.BadRequest("Name is required.");

    var space = new Space
    {
        Name = dto.Name.Trim(),
        Type = dto.Type,
        Capacity = dto.Capacity,
        HasPrivateBathroom = dto.HasPrivateBathroom
    };

    db.Spaces.Add(space);
    await db.SaveChangesAsync();

    return Results.Created($"/api/spaces/{space.Id}", space);
});

// Put (update)
app.MapPut("/api/space/{id:int}", async (PixelCoworkingDbContext db, int id, SpaceUpdateDto dto) =>
{
    if (string.IsNullOrEmpty(dto.Name))
        return Results.BadRequest("Name is required.");

    var space = await db.Spaces.FirstOrDefaultAsync(x => x.Id == id);
    if (space is null)
        return Results.NotFound();

    space.Name = dto.Name.Trim();
    space.Type = dto.Type;
    space.Capacity = dto.Capacity;
    space.HasPrivateBathroom = dto.HasPrivateBathroom;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE
app.MapDelete("/api/spaces/{id:int}", async (PixelCoworkingDbContext db, int id) =>
{
    var space = await db.Spaces.FirstOrDefaultAsync(x => x.Id == id);
    if (space is null)
        return Results.NotFound();

    db.Spaces.Remove(space);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
