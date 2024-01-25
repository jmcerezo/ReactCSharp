using RegistrationApp.Server.Data;
using RegistrationApp.Server.Dto;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
SqlHelper sql = new(connectionString);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("https://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.MapPost("/api/Users", (CreateUserDto createUserDto) => sql.RegisterUser(createUserDto));

app.MapGet("/api/Users", () => sql.GetAllUsers());

app.MapGet("/api/Users/{id}", (int id) => sql.GetUserById(id));

app.MapPut("/api/Users/{id}", (int id, UpdateUserDto updateUserDto) => sql.UpdateUser(id, updateUserDto));

app.MapDelete("/api/Users/{id}", (int id) => sql.DeleteUser(id));

app.MapFallbackToFile("/index.html");

app.Run();
