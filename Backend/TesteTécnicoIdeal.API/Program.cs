using Microsoft.EntityFrameworkCore;
using TesteT�cnicoIdeal.API.Database;
using TesteT�cnicoIdeal.API.DTO_s;
using TesteT�cnicoIdeal.API.Repositories;
using TesteT�cnicoIdeal.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TesteT�cnicoIdeal_DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));        // Conex�o com DB provis�rio
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));







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
