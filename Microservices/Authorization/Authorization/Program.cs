using Authorization.ApplicationServices.User.Ports;
using Authorization.ApplicationServices.User;
using UserDbSevice;
using System.Diagnostics;


string logDirectory = "logs"; // ��� ���������� ��� �������� �����
string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); // ������� ���� � ����� � ������� yyyyMMdd_HHmmss
var appPath = System.Reflection.Assembly.GetEntryAssembly()?.Location;
var appDirectory = Path.GetDirectoryName(appPath);
string logFile = Path.Combine(appDirectory?? "", logDirectory, "log_" + timestamp + ".txt");

// �������� ����������, ���� ��� �� ����������
if (!Directory.Exists(appDirectory+ Path.DirectorySeparatorChar + logDirectory))
{
    Directory.CreateDirectory(appDirectory + Path.DirectorySeparatorChar + logDirectory);
}

// �������� �������������� �����
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
