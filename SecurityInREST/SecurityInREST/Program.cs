using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecurityInREST.Security;
using SecurityInREST.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//REST: SOA yapısı. REpresentational State Transfer -> Temsili Durum Transferi

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option=> option.AddPolicy("allow", policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();

    /*
     *  http://www.tgs.com.tr/operations
     *  https://www.tgs.com.tr
     *  http://service.tgs.com.tr
     *  http://www.tgs.com.tr:3030
     *  
     *  
     *  
     */
}));

builder.Services.AddScoped<UserService>();

//builder.Services.AddAuthentication("Basic")
//                .AddScheme<BasicOption, BasicHandler>("Basic", null);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "server.tgs",

                    ValidateAudience = true,
                    ValidAudience = "client.tgs",

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("burası-token-onayi-icin-kullanilacak")),
                    ValidateIssuerSigningKey = true

                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("allow");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
