using Microsoft.EntityFrameworkCore;
using MyFolderProject.Data;
using MyFolderProject.Interface;
using MyFolderProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FoldersAndFilesAPIDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FoldersAndFilesApiConnectionsString")));

builder.Services.AddMvc();
builder.Services.AddScoped<IMyFoldersService, MyFoldersService>();
builder.Services.AddScoped<IMyFilesService, MyFilesService>();


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
