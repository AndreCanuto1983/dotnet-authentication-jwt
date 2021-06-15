using Core.Interfaces;
using Core.Models.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenWeatherMap.Interface;
using OpenWeatherMap.Models;
using OpenWeatherMap.Services;
using Spotify.Interfaces;
using Spotify.Services;
using System.Text;
using WebAPI.Config;
using WebAPI.Context;
using WebAPI.Interfaces;
using WebAPI.Models.NoteModel;
using WebAPI.Models.AccessModels;
using WebAPI.Repository;
using WebAPI.Services.Token;
using WebAPI.ViewModels.UserViewModels;
using Spotify.Models;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurando o acesso ao bd
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //cria e utiliza o context na memória, qdo a requisição terminar ele limpa o context da memória.
            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IRepository<Notes>, NotesRepository>();
            services.AddTransient<IGenerateToken, GenerateToken>();
            services.AddTransient<IGetWeatherInfo<WeatherInfo>, WeatherInfoService>();
            services.AddTransient<IGetResponseApiExternal<string>, SpotifyRequest>();
            services.AddTransient<IGetExternalToken, GetSpotifyToken>();
            services.AddTransient<IGetResponseApiExternal<string>, SpotifyRequest>();      
            services.AddTransient<IGetIdPlaylist<string, double>, GetIdPlaylistSpotfy>();      
            services.AddTransient<IGetPlaylist<string, double>, GetPlaylistSpotifyForOpenInBrowser>();
            services.AddTransient<UserBackViewModel, UserBackViewModel>();
            services.AddTransient<UserLoginBackViewModel, UserLoginBackViewModel>();
            services.AddTransient<Page, Page>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();            

            // Configurando JWT
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), //chave da linha 64
                    ValidateIssuer = true, //valida o emissor. Posso passar uma collection se eu quiser
                    ValidIssuer = appSettings.Emissor,
                    ValidateAudience = true, //valida a url. Posso passar uma collection se eu quiser
                    ValidAudience = appSettings.OriginValidate
                };
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);            
            //services.AddCors();
            services.AddControllers();
            services.AddSwaggerConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseHsts();
            }
                        
            app.UseHttpsRedirection();

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseMvc();

            //usar swagger
            app.UseSwaggerConfiguration();
        }
    }
}