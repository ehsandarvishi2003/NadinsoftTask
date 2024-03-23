
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using NadinsoftTask.Models.DataBase;
using NadinsoftTask.Models.Repository;

namespace NadinsoftTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region Connection String
            string ConnectionString = "Data Source=EHSAN;Initial Catalog=NadinsoftTask;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(ConnectionString));
            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            #region Inject Services and Ripositoreis

            builder.Services.AddScoped<ProductRepository, ProductRepository>();

            #endregion

            builder.Services.AddSwaggerGen();

            #region Api Versioning

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified=true; // وقتی هیچ ورژنی تعریف نشده بود بیاد دیفالت ورژن رو روش اعمال بکنه
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.ReportApiVersions= true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });

            #endregion

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
        }
    }
}