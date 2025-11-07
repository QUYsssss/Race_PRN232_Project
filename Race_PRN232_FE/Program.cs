var builder = WebApplication.CreateBuilder(args);

// ==================================================
// ? ??ng ký các d?ch v? c?n thi?t
// ==================================================
builder.Services.AddRazorPages();

// ? Cho phép inject IHttpContextAccessor trong Razor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ==================================================
// ? C?u hình pipeline HTTP
// ==================================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ? Khi ch?y l?n ??u, t? ??ng chuy?n v? trang Login
app.MapGet("/", context =>
{
    context.Response.Redirect("/Auth/Login");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
