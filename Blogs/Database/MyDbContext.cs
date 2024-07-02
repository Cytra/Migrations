using Blogs.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Database;

public class MyDbContext(DbContextOptions<MyDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }
}