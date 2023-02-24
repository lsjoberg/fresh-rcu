using FreshRcu.Rcu;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<RcuOptions>()
    .Bind(builder.Configuration.GetSection("Rcu"));
if (builder.Configuration.GetValue<bool>("Rcu:FetchFromFile"))
{
    builder.Services.AddSingleton<IRcuLogFetcher, RcuLogFileFetcher>();
}
else
{
    builder.Services.AddSingleton<IRcuLogFetcher, RcuLogHttpFetcher>();    
}

builder.Services.AddSingleton<RcuLogService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();