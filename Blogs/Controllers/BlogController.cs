using Blogs.Database;
using Blogs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController(MyDbContext context) : ControllerBase
{
    [HttpGet]
    public Task<List<Blog>> GetAll()
    {
        return context.Blogs.ToListAsync();
    }

    [HttpPost]
    public Task<int> SaveBlog(string name)
    {
        context.Blogs.Add(new Blog()
        {
            Name = name,
            CreatedTimestamp = DateTime.UtcNow
        });
        return context.SaveChangesAsync();
    }
}