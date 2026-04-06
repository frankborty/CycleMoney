var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CycleMoneyRemoteDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SportScheduleDb")));

builder.Services.AddScoped<IRecurringExpenseService, RecurringExpenseService>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseAuthorization();
app.MapControllers();

app.Run();
