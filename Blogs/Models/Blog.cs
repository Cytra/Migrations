namespace Blogs.Models;

public class Blog
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedTimestamp { get; set; }
}