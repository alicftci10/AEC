using AEC_Entities.Configuration;

namespace AEC_WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region AddRazorRuntimeCompilation

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            #endregion

            #region Session

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(i =>
            {
                i.IdleTimeout = TimeSpan.FromHours(6);
                i.Cookie.Name = "WebApp";
            });

            #endregion

            #region MemoryCache

            builder.Services.AddMemoryCache();

            #endregion

            #region ApiUrl ve MemoryCacheTimeOut

            var apiUrl = builder.Configuration["WebApiUrl"];
            ConfigurationInfo.ApiUrl = apiUrl;
            ConfigurationInfo.MemoryCacheTimeOut = Convert.ToInt32(builder.Configuration["MemoryCacheTimeOut"]);

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            #region Session kullanýlmasý

            app.UseSession();

            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
