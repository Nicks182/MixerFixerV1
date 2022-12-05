using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Web;

namespace Services
{
    public class Startup
    {
        // This is so the File Provider knows where to get the Embedded files.
        // It's the namespace of your app with the folder path.
        // You can see this folder is specified with some wild cards when you Edit the Project File.
        const string G_FP_Namespace = "MixerFixerV1.wwwroot";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<Srv_DB>();
            services.AddSingleton<Srv_TimerManager>();
            services.AddSingleton<HTML_Templates>();
            services.AddSingleton<Srv_UI>();

            services.AddCors();
            services.AddLogging();
            services.AddResponseCompression();
            services.AddSignalR()
            
            //.AddNewtonsoftJsonProtocol(options =>
            //{
            //    options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter(null, false));
            //    options.PayloadSerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //    options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
            //});
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter(null, true));
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                options.PayloadSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });

            

            //services.AddLogging();
            //services.AddResponseCompression();
            //.AddControllers();
            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.Converters.Add(new StringEnumConverter(null, true));
            //    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //});

        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                .WithOrigins("http://" + Srv_Utils._GetIp() + ":5000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseDeveloperExceptionPage();

            EmbeddedFileProvider L_FileProvider = _Get_FileProvider();

            app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = L_FileProvider });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = L_FileProvider,
            });


            app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async http => { http.Response.Redirect("/index.html"); });
            //});
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<CommandHub>("/CommandHub");
            });

        }

        private EmbeddedFileProvider _Get_FileProvider()
        {
            return new EmbeddedFileProvider(
                assembly: typeof(Startup).Assembly,
                baseNamespace: G_FP_Namespace);
        }

        //private string _Get_FP_Namespace()
        //{
        //    // This is so the File Provider knows where to get the Embedded files.
        //    // It's the namespace of your app with the folder path.
        //    // You can see this folder is specified with some wild cards when you Edit the Project File.
            
        //    return "MixerFixerV1.wwwroot";
        //}
    }
}
