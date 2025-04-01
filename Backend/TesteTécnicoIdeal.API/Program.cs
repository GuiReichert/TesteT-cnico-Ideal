using Microsoft.EntityFrameworkCore;
using TesteTécnicoIdeal.API.Database;
using TesteTécnicoIdeal.API.DTO_s;
using TesteTécnicoIdeal.API.Repositories;
using TesteTécnicoIdeal.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TesteTécnicoIdeal_DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));        // Conexão com DB provisório
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
