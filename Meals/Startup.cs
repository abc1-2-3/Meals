using Meals.BLL.ImplementBLL;
using Meals.BLL.InterfaceBLL;
using Meals.Models.Context;
using Meals.Repository.ImplementRepository;
using Meals.Repository.InterfaceRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meals
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Jwt>();
            services.AddSingleton<MD5>();
            services.AddScoped<IModifyCustomerBLL, ModifyCustomerBLL>();
            services.AddScoped<Mail>();
            services.AddScoped<ILogInBLL, LogInBLL>();
            services.AddScoped<IOrderBLL, OrderBLL>();
            services.AddScoped<ISerchBLL, SerchBLL>();
            services.AddScoped<IProductBLL, ProductBLL>();
            services.AddScoped<IBomBLL, BomBLL>();
            services.AddScoped<IItemBLL, ItemBLL>();
            services.AddScoped<ISetUpBLL, SetUpBLL>();
            services.AddScoped<IBomRepository, BomRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddLogging(m => m.AddNLog());
            services.AddDbContext<DBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DBConStr")));
            services.AddControllers();


            // Add the required Quartz.NET services
            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
            });

            // Add the Quartz.NET hosted service

            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);





            ///sweager���n�J���A
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meals", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);
            });

            ///����
            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // �����ҥ��ѮɡA�^�����Y�|�]�t WWW-Authenticate ���Y�A�o�̷|��ܥ��Ѫ��Բӿ��~��]
                options.IncludeErrorDetails = true; // �w�]�Ȭ� true�A���ɷ|�S�O����

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    // �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                    // �@��ڭ̳��|���� Issuer
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

                    // �q�`���ӻݭn���� Audience
                    ValidateAudience = false,
                    //ValidAudience = "JwtAuthDemo", // �����ҴN���ݭn��g

                    // �@��ڭ̳��|���� Token �����Ĵ���
                    ValidateLifetime = true,


                    // �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
                    ValidateIssuerSigningKey = false,

                    // "1234567890123456" ���ӱq IConfiguration ���o
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Meals v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
