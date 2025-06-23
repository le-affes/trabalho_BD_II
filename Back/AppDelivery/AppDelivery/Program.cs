using AppDelivery.Model;
using Npgsql;

namespace AppDelivery;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // CORS liberado
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}
