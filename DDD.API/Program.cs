using DDD.Business.Interfaces.Processes.Calendar;
using DDD.Business.Interfaces.Processes.Users;
using DDD.Business.Interfaces.Services.Calendar;
using DDD.Business.Interfaces.Services.Users;
using DDD.Business.Processes.Calendar.Events;
using DDD.Business.Processes.Users;
using DDD.Business.Services.Calendar;
using DDD.Business.Services.Users;
using DDD.Core.Maps.Calendar;
using DDD.Core.Maps.Users;
using DDD.DataAccess;
using DDD.DataAccess.Entities.Calendar;
using DDD.DataAccess.Entities.Users;
using DDD.DataAccess.Interfaces.Calendar;
using DDD.DataAccess.Interfaces.Users;
using DDD.DataAccess.Repositories.Calendar;
using DDD.DataAccess.Repositories.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(
    typeof(EventEntityMapper),
    typeof(ClientEntityMapper),
    typeof(EventMapper),
    typeof(ClientMapper)
);

//Processes
builder.Services.AddScoped<ICreateEventProcess, CreateEventProcess>();
builder.Services.AddScoped<IGetEventProcess, GetEventProcess>();
builder.Services.AddScoped<IUpdateEventProcess, UpdateEventProcess>();
builder.Services.AddScoped<IDeleteEventProcess, DeleteEventProcess>();
builder.Services.AddScoped<IGetEventsProcess, GetEventsProcess>();
builder.Services.AddScoped<ICreateClientProcess, CreateClientProcess>();
builder.Services.AddScoped<IGetClientProcess, GetClientProcess>();
builder.Services.AddScoped<IGetClientsProcess, GetClientsProcess>();
builder.Services.AddScoped<IUpdateClientProcess, UpdateClientProcess>();
builder.Services.AddScoped<IDeleteClientProcess, DeleteClientProcess>();

//Services
builder.Services.AddScoped<IEventsService, EventsService>();
builder.Services.AddScoped<IClientsService, ClientsService>();

//Repository
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IEventsClientsRepository, EventsClientsRepository>();
builder.Services.AddScoped<IClientsRepository, ClientsRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
