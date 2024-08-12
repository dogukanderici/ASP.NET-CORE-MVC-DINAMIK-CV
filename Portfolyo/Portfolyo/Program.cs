using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.AuthorizationOperations.RoleHandler;
using Core.Utilities.AuthorizationOperations.RoleRequirement;
using Core.Utilities.FileOperations;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Jwt.Encryption;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Portfolyo.AuthorizationOperations.RoleProvider;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddFluentValidation(option =>
    {
        option.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        option.DisableDataAnnotationsValidation = true;
    });

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<MyTokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = tokenOptions.Audience,
            ValidIssuer = tokenOptions.Issuer,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecretKey)
        };
    });

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterType<EfAboutDal>().As<IAboutDal>();
        builder.RegisterType<EfFeatureDal>().As<IFeatureDal>();
        builder.RegisterType<EfServiceDal>().As<IServiceDal>();
        builder.RegisterType<EfSkillDal>().As<ISkillDal>();
        builder.RegisterType<EfPortfolioDal>().As<IPortfolioDal>();
        builder.RegisterType<EfExperienceDal>().As<IExperienceDal>();
        builder.RegisterType<EfTestimonialDal>().As<ITestimonialDal>();
        builder.RegisterType<EfContactDal>().As<IContactDal>();
        builder.RegisterType<EfMessageDal>().As<IMessageDal>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();
        builder.RegisterType<EfAnnouncementDal>().As<IAnnouncementDal>();
        builder.RegisterType<EfWriterMessageDal>().As<IWriterMessageDal>();
        builder.RegisterType<EfSocialMediaDal>().As<ISocialMediaDal>();
        builder.RegisterType<EfTodoListDal>().As<ITodoDal>();
        builder.RegisterType<EfWriterUserDal>().As<IWriterUserDal>();
        builder.RegisterType<EfWriterRoleDal>().As<IWriterRoleDal>();
        builder.RegisterType<EfPanelRoleDal>().As<IPanelRoleDal>();

        builder.RegisterType<AboutManager>().As<IAboutService>();
        builder.RegisterType<FeatureManager>().As<IFeatureService>();
        builder.RegisterType<ServiceManager>().As<IServiceService>();
        builder.RegisterType<SkillManager>().As<ISkillService>();
        builder.RegisterType<PortfolioManager>().As<IPortfolioService>();
        builder.RegisterType<ExperienceManager>().As<IExperienceService>();
        builder.RegisterType<TestimonialManager>().As<ITestimonialService>();
        builder.RegisterType<MessageManager>().As<IMessageService>();
        builder.RegisterType<ContactManager>().As<IContactService>();
        builder.RegisterType<AuthManager>().As<IAuthService>();
        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        builder.RegisterType<AnnouncementManager>().As<IAnnouncementService>();
        builder.RegisterType<WriterMessageManager>().As<IWriterMessageService>();
        builder.RegisterType<SocialMediaManager>().As<ISocialMediaService>();
        builder.RegisterType<TodoListManager>().As<ITodoListService>();
        builder.RegisterType<WriterUserManager>().As<IWriterUserService>();
        builder.RegisterType<FileOperationHelper>().As<IFileOperationHelper>();
        builder.RegisterType<WriterRoleManager>().As<IWriterRoleService>();
        builder.RegisterType<PanelRoleManager>().As<IPanelRoleService>();

        builder.RegisterType<CustomAuthorizationPolicyProvider>().As<IAuthorizationPolicyProvider>().SingleInstance();
        builder.RegisterType<GenericDynamicRoleHandler>().As<IAuthorizationHandler>().InstancePerLifetimeScope();
    });

builder.Services.AddHttpClient();

builder.Services.AddDbContext<PortfolyoContext>()
    .AddIdentity<WriterUser, WriterRole>()
    .AddRoles<WriterRole>()
    .AddEntityFrameworkStores<PortfolyoContext>()
    .AddDefaultTokenProviders();

// Sisteme giriþ yapýlmýþ olmasý zorunluluðunu ekler.
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(100); // Kullanýcýnýn ne kadar süre sistemde kalacaðýný belirler.
    options.AccessDeniedPath = "/ErrorPage";
    options.LoginPath = "/Auth/Login"; // Eðer sisteme giriþ yapýlmadýysa belirtilen adrese yönlendirir.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/ErrorPage/Error404");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Default}/{action=Index}/{id?}");
});

app.Run();
