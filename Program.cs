using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using authapinet8.Data;
using authapinet8.Data.entities;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("SupabaseAuthConnection");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityApiEndpoints<IdentityUser>(options => options.User.RequireUniqueEmail=false)
    .AddEntityFrameworkStores<AuthContext>();
/*
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = false;
});
*/
builder.Services.AddDbContext<AuthContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseAuthConnection")));

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

app.MapGroup("/api")
.MapIdentityApi<UserEntity>();

app.MapPost("/api/signup", async (UserManager<IdentityUser> userManager, [FromBody] UserRegDto userRegistrationDto) =>
{/*
    UserEntity user = new UserEntity()
    {
        Email = userRegistrationDto.Email,
        UserName=userRegistrationDto.Username,
        FirstName = userRegistrationDto.FirstName,
        MiddleName = userRegistrationDto.MiddleName,
        LastName = userRegistrationDto.LastName
    };*/

    var identityUser = new IdentityUser
    {
      Email = userRegistrationDto.Email,
      UserName=userRegistrationDto.Username
    };
   // var result = await userManager.CreateAsync(identityUser, userRegistrationDto.Password);
    var result = await userManager.CreateAsync(identityUser, userRegistrationDto.Password);

    if (result.Succeeded)
        return Results.Ok(result);
    else
        return Results.BadRequest(result);
});

app.Run();

public class UserRegistrationDto
{
    public string Email { get; set; }
       public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
  
    public string MiddleName { get; set; }
 
    public string LastName { get; set; }
}

public class UserRegDto
{
    public string Email { get; set; }
       public string Username { get; set; }
    public string Password { get; set; }

}