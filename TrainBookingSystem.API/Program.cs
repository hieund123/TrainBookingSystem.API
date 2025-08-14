using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Services.Authentication;
using TrainBookingSystem.API.Services.Booking;
using TrainBookingSystem.API.Services.CarriageClass;
using TrainBookingSystem.API.Services.CarriagePrice;
using TrainBookingSystem.API.Services.JourneyCarriage;
using TrainBookingSystem.API.Services.JourneyStation;
using TrainBookingSystem.API.Services.Schedules;
using TrainBookingSystem.API.Services.TrainJourney;
using TrainBookingSystem.API.Services.TrainStation;
using TrainBookingSystem.Service.Models;
using TrainBookingSystem.Service.Services;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod(); // Cho phép GET, POST, PUT, DELETE, OPTIONS
        });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


// Add services to the container.
builder.Services.AddControllers();

// Disable auto-validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Aith API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[] {}
        }
    });
});



// Add services to the containner

// For Identity Framework
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Tokens.ProviderMap.Add("email", new TokenProviderDescriptor(typeof(EmailTokenProvider<IdentityUser>)));
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

// Add config for required email
builder.Services.Configure<IdentityOptions>(opts => opts.SignIn.RequireConfirmedEmail = true);

builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));

// Add Email Configs
var emailConfig = configuration.GetSection("EmailConfiguration")
                                .Get<EmailConfiguration>()!;

builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailService, EmailService>();



// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"],
        ValidIssuer = configuration["Jwt:Issuer"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
    };
});
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ITrainJourneyService, TrainJourneyService>();

builder.Services.AddScoped<ITrainStationService, TrainStationService>();

builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddScoped<IJourneyStationService, JourneyStationService>();

builder.Services.AddScoped<ICarriageClassService, CarriageClassService>();

builder.Services.AddScoped<IJourneyCarriageService, JourneyCarriageService>();

builder.Services.AddScoped<ICarriagePriceService, CarriagePriceService>();

builder.Services.AddScoped<IBookingService, BookingService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularClient");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
