using AutoMapper;
using DotnetMinApi.Data;
using DotnetMinApi.Dtos;
using DotnetMinApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConBuilder = new SqlConnectionStringBuilder(); 

// Note how the builder obejct is able to pull from various configuration entities, the connString is coming from a json config file, whereas the UserId/Passwd is 
// coming from a secret store.
sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection"); // getting this from appsettings.json file
sqlConBuilder.UserID = builder.Configuration["UserId"];
sqlConBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<DotnetMinApi.Data.AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString)); // Where AppDbContext is your namespace
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //registering automapper for dependency injection for use in the application


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/v1/commands", async (ICommandRepo repo, IMapper mapper) => { // This is dependency injection
    var commands = await repo.GetAllCommands();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands)); // return the results and map the returned model to our DTO.
}); 

app.MapGet("/api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, int id) => {
    var command = await repo.GetCommandById(id);
    if (command != null)
    {
        return Results.Ok(mapper.Map<CommandReadDto>(command));
    }
    return Results.NotFound();
});

app.MapPost("/api/v1/commands", async (ICommandRepo repo, IMapper mapper, CommandCreateDto cmdCreateDto) => { // Object mapping the request body
    var commandModel = mapper.Map<Command>(cmdCreateDto);

    await repo.CreateCommand(commandModel);
    await repo.SaveChanges(); // pushes down to the persistance layer, name this SaveChangesAsync instead (best practice)

    var cmdReadDto = mapper.Map<CommandReadDto>(commandModel);

    return Results.Created($"api/v1/commands/{cmdReadDto.Id}", cmdCreateDto); // .Created just gives you a 201 instead of a 200
});

app.MapPut("/api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, CommandUpdateDto cmdUpdateDto, int id) => {
    var command = await repo.GetCommandById(id);
    if (command == null)
    {
        return Results.NotFound();
    }

    mapper.Map(cmdUpdateDto, command); // source --> target 

    await repo.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("/api/v1/commands/{id}", async (ICommandRepo repo, int id) => {
    var command = await repo.GetCommandById(id);
    if (command == null) 
    {
        return Results.NotFound();
    }

    repo.DeleteCommand(command); // not async
    await repo.SaveChanges();

    return Results.NoContent();
});

app.Run();

