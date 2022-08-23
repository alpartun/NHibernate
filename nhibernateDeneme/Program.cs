using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using nhibernateDeneme.Context;
using nhibernateDeneme.Mapping;
using nhibernateDeneme.Middleware;
using nhibernateDeneme.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mapper = new ModelMapper();
mapper.AddMappings(typeof(BookMap).Assembly.ExportedTypes);
HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

var configuration = new Configuration();
configuration.DataBaseIntegration(c =>
{
    c.Dialect<PostgreSQLDialect>(); 
    c.ConnectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
    c.SchemaAction = SchemaAutoAction.Validate;//Create first
    c.LogFormattedSql = true;
    c.LogSqlInConsole = true;
});
configuration.AddMapping(domainMapping);

var sessionFactory = configuration.BuildSessionFactory();

builder.Services.AddSingleton(sessionFactory);
builder.Services.AddScoped(factory => sessionFactory.OpenSession());


// inject
builder.Services.AddScoped<IMapperSession, MapperSession>();


    var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Middleware

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

