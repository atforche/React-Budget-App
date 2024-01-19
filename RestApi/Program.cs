using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using RestApi;
using RestApi.WindowsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure the JSON serializer to be able to handle interface types
        options.JsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        {
            Modifiers = { JsonInterfaceHelper.ResolveTypeInfo }
        };
        // Configure the JSON serializer to be able to decode enums from their string values
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow the API to be hosted as a Windows service
builder.Services.AddWindowsService();
builder.Services.AddHostedService<WindowsService>();

var app = builder.Build();

// Configure the HTTP request pipeline. Always leave the Swagger page active since this won't be deployed publicly
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();