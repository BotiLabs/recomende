using Eudora.Api.Models;
using Eudora.Api.Repositories;
using Eudora.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eudora.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddTransient<IRepresentanteService, RepresentanteService>();
            services.AddTransient<IMensagemService, MensagemService>();
            services.AddTransient<IRepresentanteRepository, RepresentanteRepository<Representante>>();
            services.AddTransient<ICompraRepository, CompraRepository<Compra>>();
            services.AddTransient<ICompraCategoriaRepository, CompraCategoriaRepository<CompraCategoria>>();
            services.AddTransient<ICompraMaterialRepository, CompraMaterialRepository<CompraMaterial>>();
            services.AddTransient<IMensagemRepository, MensagemRepository<Mensagem>>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod()
            );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
