using Microsoft.EntityFrameworkCore;
using Proyecto2API.Models;
using Proyecto2API.Data;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseNLog();

var connectionString = builder.Configuration.GetConnectionString("SQLServerConnection");
builder.Services.AddDbContext<ContextDb>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("Cliente/", async (ContextDb context) =>
{
    app.Logger.LogInformation("Clientes encontrados");


    var cliente = await context.Clientes.ToListAsync();

    if (cliente == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(cliente);
});

app.MapGet("Cliente/{ClienteId:long}", async (long clienteId, ContextDb context) =>
{
    app.Logger.LogInformation("Cliente encontrado");

    var cliente = await context.Clientes.FindAsync(clienteId);

    if (cliente == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(cliente);
});

app.MapPost("Cliente/", async (Cliente producto, ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("Cliente creado");

        context.Clientes.Add(producto);
        await context.SaveChangesAsync();
        return Results.Created($"/Cliente/{producto.ClienteId}", producto);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapPut("Cliente/{ClienteId:long}", async (long ClienteId, Cliente cliente, ContextDb context) =>
{
    try {
        app.Logger.LogInformation("Cliente modificado");

        if (ClienteId != cliente.ClienteId)
    {
        return Results.BadRequest();
    }
    var clienteResult = await context.Clientes.FindAsync(ClienteId);

    if (clienteResult is null) return Results.NotFound();

    clienteResult.Nombre = cliente.Nombre;
    clienteResult.ClienteCedula = cliente.ClienteCedula;
    clienteResult.FechaNacimiento = cliente.FechaNacimiento;

    await context.SaveChangesAsync();

    return Results.Ok(clienteResult);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }

});

app.MapGet("Libro/", async (ContextDb context) =>
{
    app.Logger.LogInformation("Libros encontrados");
    try {
        List<Libro> libro = await context.Libros.ToListAsync();
        return libro == null ? Results.NotFound() : Results.Ok(libro);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
        
});

app.MapGet("Libro/{LibroId:long}", async (long LibroId, ContextDb context) =>
{
    try {
        app.Logger.LogInformation("Libro encontrado");

        Cliente? libro = await context.Clientes.FindAsync(LibroId);

        return libro == null ? Results.NotFound() : Results.Ok(libro);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapPost("Libro/", async (Libro libro, ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("Libro creado");

        context.Libros.Add(libro);
        await context.SaveChangesAsync();
        return Results.Created($"/Libro/{libro.LibroId}", libro);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});

app.MapPut("Libro/{LibroId:long}", async (long LibroId, Libro libro, ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("Libro modificado");

    if (LibroId != libro.LibroId)
    {
        return Results.BadRequest();
    }
    var libroResult = await context.Libros.FindAsync(LibroId);

    if (libroResult is null) return Results.NotFound();

        libroResult.Nombre = libro.Nombre;
        libroResult.Empresa = libro.Empresa;
    await context.SaveChangesAsync();

    return Results.Ok(libroResult);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapGet("LibroStock/Cliente/{ClienteId:long}", async (long ClienteId, ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("LibroStock consultado");
   
    var libroStock = await context.LibroStocks.Where(x=>x.ClienteId == ClienteId && x.LibroRetirado == false).Include(x=> x.Libro).ToListAsync();
    if (libroStock == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(libroStock);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapGet("LibroStock/", async (ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("LibroStock consultado");
    
    var libroStock = await context.LibroStocks.Include(x=>x.Libro).ToListAsync();
    if (libroStock == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(libroStock);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapGet("LibroStockSinReservar/", async (ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("LibroStock consultado Sin reservar");

        var libroStock = await context.LibroStocks.Where(x => x.ClienteId == null).Include(x => x.Libro).ToListAsync();
        if (libroStock == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(libroStock);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapGet("LibroStock/{LibroStockId:long}", async (long LibroStockId, ContextDb context) =>
{
    try {
        app.Logger.LogInformation("LibroStock consultado");
        var libroStock = await context.LibroStocks.Where(x=>x.LibroStockId == LibroStockId).Include(x=>x.Libro).FirstOrDefaultAsync();
    if (libroStock == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(libroStock);
    }catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapPost("LibroStock/", async (LibroStock libroStock, ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("LibroStock creado");
        context.LibroStocks.Add(libroStock);
        await context.SaveChangesAsync();
        return Results.Created($"/LibroStock/{libroStock.LibroStockId}", libroStock);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapPut("LibroStock/{LibroStockId:long}", async (long LibroStockId, LibroStock libroStock, ContextDb context) =>
{
    try {
    app.Logger.LogInformation("LibroStock modificado");
    if (LibroStockId != libroStock.LibroStockId)
    {
        return Results.BadRequest();
    }
    var libroStockResult = await context.LibroStocks.FindAsync(LibroStockId);
    if (libroStockResult is null) return Results.NotFound();
    libroStockResult.ClienteId = libroStock.ClienteId;

    await context.SaveChangesAsync();
    return Results.Ok(libroStockResult);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});

app.MapGet("LibroRetirado/{FechaInicio:datetime}/{FechaFinal:datetime}", async (DateTime FechaInicio, DateTime FechaFinal, ContextDb context) =>
    {
        try
        {
            app.Logger.LogInformation("LibroRetirado consultado por Fechas");
        var librosRetirados = await context.LibrosRetirados.Where(x=> x.FechaRetiro > FechaInicio && x.FechaRetiro < FechaFinal).Include(x=>x.Libro).Include(x=>x.Cliente).ToListAsync();
        if (librosRetirados == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(librosRetirados);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex.ToString());
            return Results.BadRequest(ex);
        }
    });

app.MapPost("LibroRetirado/", async (LibroRetirado libroRetirado, ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("LibroRetirado ingresado");


        LibroStock libro = new LibroStock();
        libro = await context.LibroStocks.FindAsync(libroRetirado.LibroStock);
        if (libro is null) return Results.NotFound();
        libro.LibroRetirado = true;

        context.LibrosRetirados.Add(libroRetirado);
        await context.SaveChangesAsync();
        return Results.Created($"/LibroRetirado/{libroRetirado.LibroRetiradoId}", libroRetirado);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);

    }
});

app.MapPut("LibroRetirado/{LibroRetiradoId:long}", async (long LibroRetiradoId, LibroRetirado libroRetirado, ContextDb context) =>
{
    try
    {
        app.Logger.LogInformation("LibroRetirado modificado");
        if (LibroRetiradoId != libroRetirado.LibroRetiradoId)
    {
        return Results.BadRequest();
    }
    var libroRetiradoResult = await context.LibrosRetirados.FindAsync(LibroRetiradoId);
    if (libroRetiradoResult is null) return Results.NotFound();
    libroRetiradoResult.FechaRetiro = libroRetirado.FechaRetiro;
    libroRetiradoResult.LibroId = libroRetirado.LibroId;
    libroRetiradoResult.ClienteId = libroRetirado.ClienteId;
    await context.SaveChangesAsync();
    return Results.Ok(libroRetiradoResult);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex.ToString());
        return Results.BadRequest(ex);
    }
});


app.Run();
