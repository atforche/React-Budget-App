using RestApi.WindowsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow the API to be hosted as a Windows service
builder.Services.AddWindowsService();
builder.Services.AddHostedService<WindowsService>();

var app = builder.Build();

// Configure the HTTP request pipeline. Always leave the Swagger pages active since this won't be deployed publicly
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
