
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using NadinsoftTask.Models.DataBase;
using NadinsoftTask.Models.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

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

            builder.Services.AddAntiforgery(options =>
            {
                options.DefaultAuthentacateScheme=
            });

            #region Sowagger Version control and ui hinter

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "NadinSoftTask", Version = "v1" });
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "NadinSoftTask", Version = "v2" });
                c.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "NadinSoftTask", Version = "v3" });
                c.DocInclusionPredicate((doc, apiDescription) =>
                {
                    if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var version = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return version.Any(v => $"v{v.ToString()}" == doc);
                });
                //نمایش ورژن های api و جداسازی اونها توی رابط کاربری sowagger

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "NadinSoftTask.xml"), true);
                //نتونستم فایل xml ایجاد کنم تا کامنت هایی که کردم توی swagger ui نمایش داده بشه
            });

            #endregion

            #region Api Versioning

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true; // وقتی هیچ ورژنی تعریف نشده بود بیاد دیفالت ورژن رو روش اعمال بکنه
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
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
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "WebApi v2");
                    c.SwaggerEndpoint("/swagger/v3/swagger.json", "WebApi v3");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}