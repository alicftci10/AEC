
using AEC_Business.Interfaces;
using AEC_Business.Managers;
using AEC_DataAccess.DBContext;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.EFOperations;
using AEC_DataAccess.GenericRepository.Interfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AEC_WebApi
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

            #region Ajax request

            builder.Services.AddCors(options => options.AddPolicy("_guvenliSiteler", builder =>
            {
                builder.WithOrigins("*").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));

            #endregion

            #region JWT Token - Microsoft.AspNetCore.Authentication.JwtBearer

            var key = Encoding.UTF8.GetBytes("UCi9U2H{53(1RePt{Cwc8H9B>5q%rHkS");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "AECJWTIssuer",
                    ValidAudience = "AECJWTAudience",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddAuthorization();

            #endregion

            #region Session

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(i =>
            {
                i.IdleTimeout = TimeSpan.FromHours(6);
            });

            #endregion

            #region MemoryCache

            builder.Services.AddMemoryCache();

            #endregion

            #region IOS

            builder.Services.AddDbContext<AecommerceDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IKullaniciService, KullaniciManager>();
            builder.Services.AddTransient <IKullaniciTuruService, KullaniciTuruManager>();
            builder.Services.AddTransient<IKullaniciKartService, KullaniciKartManager>();
            builder.Services.AddTransient<ICPUService, CPUManager>();
            //builder.Services.AddTransient<IWeb3Service, Web3Manager>();
            //builder.Services.AddTransient<ISpecialsService, SpecialsManager>();

            builder.Services.AddTransient<IEFKullaniciRepository, EFKullanici>();
            builder.Services.AddTransient<IEFKullaniciTuruRepository, EFKullaniciTuru>();
            builder.Services.AddTransient<IEFKullaniciKartRepository, EFKullaniciKart>();
            builder.Services.AddTransient<IEFCPURepository, EFCPU>();
            //builder.Services.AddTransient<IEFWeb3Repository, EFWeb3>();
            //builder.Services.AddTransient<IEFSpecialsRepository, EFSpecials>();

            builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            #endregion

            #region ConnectionString ve Config bilgileri

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            ConfigurationInfo.ConnectionString = connectionString;

            ConfigurationInfo.ImageFolderUrl = builder.Configuration["ImageUploadURL"];

            #endregion

            var app = builder.Build();

            #region Ajax Cors

            app.UseCors("_guvenliSiteler");

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            #region Session kullanılması

            app.UseSession();

            #endregion

            #region JWT Konfigürasyonu

            app.UseAuthentication();
            app.UseAuthorization();

            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}
