using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace PeliculasAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration) //Se inserta como una propiedad y eso genera una clas ede IConfigyration de get
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)//Proovedor de configuracion para acceder a la data de los proovedor
        {

            services.AddAutoMapper(typeof(Startup));//En este mismo proyecto se va a tener una clase que va a encapsular las configuraciones del automapper


            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers(opcions =>
            {    
            });//configuracion del json para desabilitar los cycles que inpiden la muestra de data

            
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) //env es el ambiente
        {

            

            app.Map("/ruta1", app => //Map hace una difurcancion de la tuberia de procesos. 
            {
                app.Run(async contexto =>
                {
                    await contexto.Response.WriteAsync("Estoy interseptando la tuberia");
                });
            });



            if (env.IsDevelopment())
            {

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();//milddlware para usar cache de datos


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

    }
}
