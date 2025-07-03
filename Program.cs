using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MicroservicioDemo2.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexión a la base de datos
builder.Services.AddDbContext<ProductoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CalificacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservicio Producto", Version = "v1" });
});

var app = builder.Build();

// Crear base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var prodContext = scope.ServiceProvider.GetRequiredService<ProductoContext>();
    prodContext.Database.EnsureCreated();

    var califContext = scope.ServiceProvider.GetRequiredService<CalificacionContext>();
    califContext.Database.EnsureCreated(); // ⬅️ ESTE FALTABA
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // Para mostrar errores internos
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
