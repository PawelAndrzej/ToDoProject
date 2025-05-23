using NHibernate;
using ToDoWebApplication.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add NHibarnate
var configuration = builder.Services
    .BuildHNibernateConfiguration
    ("Server=localhost;Port=3306;Database=todo;Uid=root;Pwd=Test123##;");
builder.Services.AddSingleton(configuration);
// Add SessionFactory
builder.Services.AddSingleton<ISessionFactory>(s => s.GetRequiredService<NHibernate.Cfg.Configuration>().BuildSessionFactory());
// Add Session
builder.Services.AddScoped<NHibernate.ISession>(s => s.GetRequiredService<ISessionFactory>().WithOptions().OpenSession());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/ToDo/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDo}/{action=Index}/{id?}");




app.Run();
