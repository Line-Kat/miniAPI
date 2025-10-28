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
// Scoped livstid brukes fordi tjenestene jobber med DbContext, som ogs� er scoped.
// Dette sikrer at hver HTTP-foresp�rsel f�r sin egen instans og unng�r tr�dproblemer.
builder.Services.AddScoped<OppgaveRepository>();
builder.Services.AddScoped<OppgaveService>();
builder.Services.AddDbContext<OppgaveContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Gjør at React-appen får lov til å kommunisere med API-et
builder.Services.AddCors(options => {
    options.AddPolicy("TillatFrontend", policy => {
        policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseRouting();
app.UseCors("TillatFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); Deaktivert for lokal utvikling

app.UseAuthorization();

app.MapControllers();

app.Run();
