using authapinet8.Data.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace authapinet8.Data;

public class AuthContext : IdentityDbContext
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
    public DbSet<UserEntity> Users { get; set; }
}