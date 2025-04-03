using Meepl.API;
using Meepl.Controllers;
using Meepl.Managers;
using Meepl.Social.Interfaces;
using Meepl.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Spectre.Console;

var consoleRule = new Rule();
consoleRule.RuleStyle("lime");
AnsiConsole.Write(consoleRule);
AnsiConsole.Markup("[#FED85D]███╗   ███╗███████╗███████╗██████╗ ██╗     [/]\n");
AnsiConsole.Markup("[#FED85D]████╗ ████║██╔════╝██╔════╝██╔══██╗██║     [/]\n");
AnsiConsole.Markup("[#FF9933]██╔████╔██║█████╗  █████╗  ██████╔╝██║     [/]\n");
AnsiConsole.Markup("[#FF9933]██║╚██╔╝██║██╔══╝  ██╔══╝  ██╔═══╝ ██║     [/]\n");
AnsiConsole.Markup("[#ff5555]██║ ╚═╝ ██║███████╗███████╗██║     ███████╗[/]\n");
AnsiConsole.Markup("[#FD0E35]═╚╝     ╚═╝╚══════╝╚══════╝╚═╝     ╚══════╝[/]\n");
AnsiConsole.Write(consoleRule);
AnsiConsole.Markup("[#AAF0D1]Copyright (c) Tablebound LLC. 2025 and affiliates.[/]\n");
AnsiConsole.Markup("[#AAF0D1]All rights reserved.[/]\n");
AnsiConsole.Write(consoleRule);
AnsiConsole.Markup("[#FFFFFF]Version: " + Globals.VERSION_STRING + "[/]\n");
AnsiConsole.Markup("[#FFFFFF]Meepl API Version: " + Globals.MEEPL_API_VERSION+"[/]\n");
AnsiConsole.Write(consoleRule);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin() // Allows all origins (you can change this to specify certain origins)
            .AllowAnyMethod() // Allows all HTTP methods
            .AllowAnyHeader()); //Allows all HTTP Headers
});
string value = builder.Configuration["meeplconf:serverBind"]; //Leave this commented if you are testing local, this is here for when we deploy
builder.WebHost.UseUrls(value);
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Meepl API", Version = "v1"});
    var securityScheme = new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT" // Optional
    };
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
        }
    };
    options.AddSecurityDefinition("bearerAuth", securityScheme);
    options.AddSecurityRequirement(securityRequirement);
});


SqlManager.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JWT:Issuer"],
    ValidAudience = builder.Configuration["JWT:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["JWT:Key"]))
};


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(bearer =>
    {
        bearer.TokenValidationParameters = tokenValidationParameters;
    });

SqlManager manager = new SqlManager();
manager.Init();

ProfileManager profileManager = new ProfileManager();
await profileManager.Init(manager);

FriendManager friendManager = new FriendManager();
await friendManager.Init(manager);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();