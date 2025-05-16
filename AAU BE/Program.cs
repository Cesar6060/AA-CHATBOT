// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// DotNetEnv.Env.Load();

// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


DotNetEnv.Env.Load();

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5180") // Frontend URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS middleware before authorization and mapping controllers
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//ONLY ENABLE THESE TO RUN UNIT TESTS 
//var test = new Test();
//test.Test_AuthenticateUser_WithInValidCredentials();
//test.Test_AddUser_DeleteUser_All_User_Functions();
///Console.WriteLine("Tests executed.");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

