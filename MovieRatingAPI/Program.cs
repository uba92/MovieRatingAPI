using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Interfaces.Security;
using MovieRatingAPI.Middlewares;
using MovieRatingAPI.Models;
using MovieRatingAPI.Models.Securtity;
using MovieRatingAPI.Repositories;
using MovieRatingAPI.Services;
using MovieRatingAPI.Services.Security;
using Serilog;
using Microsoft.OpenApi.Models;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/app.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        fileSizeLimitBytes: 10_000_000,
        rollOnFileSizeLimit: true,
        shared: true)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MovieRatingAPI",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.\n\n" +
                      "Insert your token like this: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddDbContext<MovieContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasherV1>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(
                "{\"error\": \"Accesso non autorizzato. Token JWT mancante o non valido.\"}");
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(
                "{\"error\": \"Accesso proibito. Non hai i permessi necessari per accedere a questa risorsa.\"}");
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();