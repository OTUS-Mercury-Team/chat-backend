using Authorization.ApplicationServices.User.Ports;
using Authorization.ApplicationServices.User;
using UserDbSevice;
using System.Diagnostics;


string logDirectory = "logs"; // Имя директории для хранения логов
string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); // Текущая дата и время в формате yyyyMMdd_HHmmss
var appPath = System.Reflection.Assembly.GetEntryAssembly()?.Location;
var appDirectory = Path.GetDirectoryName(appPath);
string logFile = Path.Combine(appDirectory?? "", logDirectory, "log_" + timestamp + ".txt");

// Создание директории, если она не существует
if (!Directory.Exists(appDirectory+ Path.DirectorySeparatorChar + logDirectory))
{
    Directory.CreateDirectory(appDirectory + Path.DirectorySeparatorChar + logDirectory);
}

// Создание прослушивателя файла
TextWriterTraceListener fileListener = new TextWriterTraceListener(logFile);

Trace.Listeners.Add(fileListener);
Trace.AutoFlush = true;


var usersDbWorker = new UsersDbWorker();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();

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
