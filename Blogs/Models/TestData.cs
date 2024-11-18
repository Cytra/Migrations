using Microsoft.EntityFrameworkCore;

namespace Blogs.Models;

[Index(nameof(TestName), IsUnique = true)]
public class TestData
{
    public int Id { get; set; }
    required public string TestName { get; set; }
    required public string TestResult { get; set; }
}
