using miniAPI.Services;
using miniAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Legger til støtte for UserSecrets i utviklingsmiljøet.
// Brukes til å lagre sensitive konfigurasjonsverdier uten å inkludere dem i kildekoden eller appsettings.json.
builder.Configuration.AddUserSecrets<Program>();

// Registrerer grunnleggende tjenester som gjør API-et tilgjengelig og dokumentert.
// AddControllers aktiverer API-ruter.
// EndpointsApiExplorer gjør dem synlige for Swagger.
// SwaggerGen genererer interaktiv dokumentasjon.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrerer applikasjonstjenester for dependency injection.
builder.Services.AddScoped<ToDoItemRepository>();
builder.Services.AddScoped<ToDoItemService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!)
            )
        };
    });
        

//Registrerer EF Core DbContext.
builder.Services.AddDbContext<ToDoItemContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// Gjør at React-appen får lov til å kommunisere med API-et.
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

// Konfigurerer HTTP-request pipeline for utviklingsmiljø.
// Aktiverer Swagger og SwaggerUI for interaktiv API-dokumentasjon og testing.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); Deaktivert for lokal utvikling
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
