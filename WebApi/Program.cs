using Application.Mapper;
using Infrastructure.Persistence.Configuration;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
}
);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(typeof(EspecialidadesMapper).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseMiddleware<EspecialidadMiddlewares>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
