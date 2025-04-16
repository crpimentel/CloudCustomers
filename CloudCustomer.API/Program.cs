using CloudCustomer.API.Config;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Configura el puerto a 5000
});

// Add services to the container.
ConfigureServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
// ✅ Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
void ConfigureServices(IServiceCollection services) 
{
    services.Configure<UserApiOptions>(
        builder.Configuration.GetSection("UserApiOptions")
        );
    services.AddTransient<IUSersService, UsersServices>();
    services.AddHttpClient<IUSersService, UsersServices>();
    
}