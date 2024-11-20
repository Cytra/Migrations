using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "http://localhost:8080/realms/local-dev";
//        options.Audience = "blogs";
//        options.RequireHttpsMetadata = false;  // Disable for local dev
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = true,
//            ValidateIssuer = true
//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("ReadAccess", policy => policy.RequireAssertion(context =>
//        context.User.HasClaim(c => c.Type == "scope" && c.Value.Contains("blogs:view"))
//    ));
//});

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
