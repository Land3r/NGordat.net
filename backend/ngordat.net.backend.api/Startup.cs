namespace ngordat.net.backend.api
{
  using Microsoft.AspNetCore.Authentication.JwtBearer;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.IdentityModel.Tokens;
  using ngordat.net.backend.api.Services.Users;
  using ngordat.net.backend.api.Settings;
  using ngordat.net.backend.services.Bookmarks;
  using ngordat.net.backend.services.Users;
  using ngordat.net.backend.transversal.Settings;
  using System;
  using System.Text;

  /// <summary>
  /// Application startup class.
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// The <see cref="IConfiguration"/> used by the program.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> to use to create instance.</param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Enable CORS.
      services.AddCors();

      // Asp.Net Core 2.2 MVC Application.
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      // Configure cache mechanism (for sessions).
      services.AddDistributedMemoryCache();

      // Enable sessions
      services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromHours(2);
        options.Cookie.HttpOnly = false;
        options.Cookie.IsEssential = true;
      });

      // Configure strongly typed settings objects.
      IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // Configure interface settings object.
      var appSettings = appSettingsSection.Get<AppSettings>();
      services.AddSingleton<IAppSettings>(appSettings);

      // Configure jwt authentication.
      var key = Encoding.ASCII.GetBytes(appSettings.JWTSecret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });

      // Configure DI for application services.
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IUserGroupService, UserGroupService>();
      services.AddScoped<IAuthorizationService, AuthorizationService>();
      services.AddScoped<IBookmarkService, BookmarkService>();
      services.AddScoped<IBookmarkTagService, BookmarkTagService>();
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
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      // Global cors policy.
      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials()
      );

      // Add sessions
      app.UseSession();
      
      // Application require Authentication.
      app.UseAuthentication();

      // Redirect to HTTPS if HTTP.
      app.UseHttpsRedirection();

      // App is MVC
      app.UseMvc();
    }
  }
}
