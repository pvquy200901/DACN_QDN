using BackEnd_Football.APIs;
using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Football;

public class Program
{
    //khai bao
    public static MyUser api_user = new MyUser();
    public static MyRole api_role = new MyRole();
    public static MyFile api_myFile = new MyFile();
    public static MyUserSystem api_userSystem = new MyUserSystem();
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel((context, option) =>
        {
            option.ListenAnyIP(50000, listenOptions =>
            {

            });
            option.Limits.MaxConcurrentConnections = 1000;
            option.Limits.MaxRequestBodySize = null;
            option.Limits.MaxRequestBufferSize = null;
        });

        // Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("HTTPSystem", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).WithExposedHeaders("Grpc-Status", "Grpc-Encoding", "Grpc-Accept-Encoding");
            });
        });

        builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(DataContext.configSql));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();

        using (var scope = app.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            DataContext datacontext = services.GetRequiredService<DataContext>();
            datacontext.Database.EnsureCreated();
            await datacontext.Database.MigrateAsync();
        }

        app.UseCors("HTTPSystem");
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.MapGet("/", () => string.Format("", DateTime.Now));
        //khoi tao
        await api_role.initAsync();
        await api_user.initAsync();
        await api_userSystem.initAsync();

        app.Run();
        
       
    }
}