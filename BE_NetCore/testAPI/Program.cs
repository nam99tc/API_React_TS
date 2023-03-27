using Microsoft.OpenApi.Models;
using testAPI.Business.EmailTemplate;
using testAPI.Business.Navigation;
using testAPI.Business.SMSTemplate;
using testAPI.Context;
using testAPI.Extention.Middleware;
using testAPI.Extention.Utils;
using testAPI.User;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        //    builder.Services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v2", new OpenApiInfo { Title = "CatalogueAPI", Version = "v2" });
        //    });
        //    builder.Services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
        //        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            In = ParameterLocation.Header,
        //            Description = "Please enter a valid token",
        //            Name = "Authorization",
        //            Type = SecuritySchemeType.Http,
        //            BearerFormat = "JWT",
        //            Scheme = "Bearer"
        //        });
        //        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Type=ReferenceType.SecurityScheme,
        //                Id="Bearer"
        //            }
        //        },
        //        Array.Empty<string>()
        //    }
        //});
        //        c.OperationFilter<AddRequiredHeaderParameter>();
        //    });
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                              });
        });

        builder.Services
            .AddScoped<IUserHandler, UserHandler>()
            .AddScoped<IEmailTemplatehandler, EmailTemplatehandler>()
            .AddScoped<ISMSTemplateHandler, SMSTemplateHandler>()
            .AddScoped<INavigationHandler, NavigationHandler>();
        builder.Services.AddDbContext<DemoContext>();

        //builder.Services.AddAuthentication(option =>
        //{
        //    option.DefaultAuthenticateScheme = "Custom Scheme";
        //    option.DefaultChallengeScheme = "Custom Scheme";
        //}).AddScheme<CustomAuthenticationOptions, CustomAuthenticationHandler>("Custom Scheme", "Custom Auth", options => { });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(MyAllowSpecificOrigins);
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DemoContext>();
        }
        app.Run();
    }
}