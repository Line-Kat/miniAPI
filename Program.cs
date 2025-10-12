using miniAPI.Services;
using miniAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrerer OppgaveRepository og OppgaveService for dependency injection.
// Scoped livstid brukes fordi tjenestene jobber med DbContext, som også er scoped.
// Dette sikrer at hver HTTP-forespørsel får sin egen instans og unngår trådproblemer.
builder.Services.AddScoped<OppgaveRepository>();
builder.Services.AddScoped<OppgaveService>();
builder.Services.AddDbContext<OppgaveContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


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
