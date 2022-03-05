using CryptoWorkbooks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOptions<DataContextOptions>()
    .BindConfiguration(DataContextOptions.SectionName);

builder.Services.AddDbContextPool<DataContext>((provider, options) =>
{
    var contextOptions = provider.GetRequiredService<IOptions<DataContextOptions>>();
    options.UseFirebird(contextOptions.Value.FirebirdConnectionString, builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

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
