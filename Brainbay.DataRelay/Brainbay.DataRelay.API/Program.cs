
using Brainbay.DataRelay.API.Middlewares;
using Brainbay.DataRelay.DataAccess.SQL;

namespace Brainbay.DataRelay.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddDataAccessSql(builder.Configuration.GetSection("DataBase:SqlServer"))
                .AddDomainDataAccessSql()
                .AddApplication()
                .AddApplicationDataAccessSql();

            builder.Services
                .AddTransient<CachedHeaderMiddleware>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<CachedHeaderMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
