using Microsoft.EntityFrameworkCore;
using TesteT�cnicoIdeal.API.Database;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TesteT�cnicoIdeal_DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));








var app = builder.Build();









if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
