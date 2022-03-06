using CryptoWorkbooks.Data;
using CryptoWorkbooks.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOptions<ContextOptions>()
    .BindConfiguration(ContextOptions.SectionName);

builder.Services.AddDbContextPool<Context>((provider, options) =>
{
    var contextOptions = provider.GetRequiredService<IOptions<ContextOptions>>();
    options.UseFirebird(contextOptions.Value.FirebirdConnectionString, builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

builder.Services.AddHttpClient<PriceService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
