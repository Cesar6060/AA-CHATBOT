using AAU.Components;
using AAU.Components.Classes;
using AAU.Services;

var builder = WebApplication.CreateBuilder(args);

if (DevMode.DEV)
{
    //add the specifc local port of your machine, we will add env variables for this later 
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5052") });
}
else
{
    //prod url would go here 
}


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<AdminService>();

// builder.Services.AddMvc().AddRazorPagesOptions((options) => { options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()); });
builder.Services.AddScoped<NavigationService>(); // Register custom services
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();

