using Unisis.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Unisis.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(8);
});

builder.Services.Configure<FirebaseOptions>(
    builder.Configuration.GetSection(FirebaseOptions.SectionName));
builder.Services.Configure<FirebaseClientOptions>(
    builder.Configuration.GetSection(FirebaseClientOptions.SectionName));
builder.Services.Configure<OpenAiOptions>(
    builder.Configuration.GetSection(OpenAiOptions.SectionName));
builder.Services.Configure<OllamaOptions>(
    builder.Configuration.GetSection(OllamaOptions.SectionName));
builder.Services.Configure<GeminiOptions>(
    builder.Configuration.GetSection(GeminiOptions.SectionName));

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();
builder.Services.AddSingleton<IFirebaseInitializer, FirebaseInitializer>();
builder.Services.AddSingleton<IUserRepository, HybridUserRepository>();
builder.Services.AddScoped<IOllamaService, OllamaService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddScoped<IIssueReportService, IssueReportService>();
builder.Services.AddScoped<IExcuseRequestService, ExcuseRequestService>();
builder.Services.AddScoped<IAcademicianAvailabilityService, AcademicianAvailabilityService>();
builder.Services.AddScoped<IQrLoginService, QrLoginService>();
builder.Services.AddScoped<IAiAssistantService, AiAssistantService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
