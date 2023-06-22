using FnolApiVersion2.O.Repositories;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FnolApiVersion2.O.Repositories.Services;
using FnolApiVersion2.O.Security.Requirements;
using FnolApiVersion2.O.Security.Handlers;
using FnolApiVersion2.O.Repositories.Services.Fnol;
using FnolApiVersion2.O.Repositories.Services.Tokens;
using FnolApiVersion2.O.Repositories.Services.Users;
using FnolApiVersion2.O.Repositories.Services.Register;
using FnolApiVersion2.O.Repositories.Services.Case;
using FnolApiVersion2.O.Repositories.Services.Roles;
using FnolApiVersion2.O.Repositories.Services.Validate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
  var securityScheme = new OpenApiSecurityScheme
  {
    Name="JWT Authentication",
    Description="Enter a valid JWT bearer token",
    In=ParameterLocation.Header,
    Type=SecuritySchemeType.Http,
    Scheme="bearer",
    BearerFormat="JWT",
    Reference = new OpenApiReference
    {
      Id=JwtBearerDefaults.AuthenticationScheme,
      Type = ReferenceType.SecurityScheme
    }
  };
  options.AddSecurityDefinition(securityScheme.Reference.Id,securityScheme);
  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {securityScheme, new string[] {}}
  });
});
builder.Services.AddDbContext<FnolDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("FnolDetailsDB")));
builder.Services.AddHttpContextAccessor();
//builder.Services.AddDbContext<FnolDbContext>(options =>
//options.UseNpgsql(builder.Configuration.GetConnectionString("FnolDetailsDB")));
builder.Services.AddTransient<IFnolRepository,FnolRepository>();
builder.Services.AddScoped<ITokenHandler, FnolApiVersion2.O.Repositories.Services.Tokens.TokenHandler>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IRegistrationRepository,RegistrationRepository>();
builder.Services.AddScoped<ICaseRepository,CaseRepository>();
builder.Services.AddScoped<IValidateRepository,ValidateRepository>();
builder.Services.AddScoped<IRoleAssign,RoleAssign>();
builder.Services.AddTransient<IAuthorizationHandler,CanOnlyAccessToHisResourceHandler>();
builder.Services.AddTransient<IAuthorizationHandler,UserCanOnlyHaveNonActiveCaseHandler>();
builder.Services.AddTransient<IAuthorizationHandler,AccessToOrgHandler>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
options.TokenValidationParameters = new TokenValidationParameters {
  ValidateIssuer=true,
  ValidateAudience=true,
  ValidateLifetime=true,
  ValidateIssuerSigningKey=true,
  ValidIssuer=builder.Configuration["Jwt:Issuer"],
  ValidAudience=builder.Configuration["Jwt:Audience"],
  IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
  )});
  builder.Services.AddAuthorization(options =>
  {
    options.AddPolicy("newCaseAccessOnly", policy => policy.Requirements.Add(new ActiveCaseForUserRequirement()));
    options.AddPolicy("AccessToResource",policy => policy.Requirements.Add(new UserAccessToHisResourceOnlyRequirement()));
    options.AddPolicy("AccessToOrg",policy => policy.Requirements.Add(new UserIncidentAccessToOrgRequirement()));
  });

var app = builder.Build();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "";
  });
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
