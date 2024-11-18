using Blogs.Database;
using Blogs.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;

namespace Tests;

public class BlogsAbTests : IDisposable
{
    private readonly HttpClient _client;
    private readonly MyDbContext _context;

    public BlogsAbTests()
    {
        var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(GetPath("config.json")));
        _client = new HttpClient { BaseAddress = new Uri(config!.BaseUrl) };
        _context = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>()
            .UseMySql(config.ConnectionString, new MariaDbServerVersion(new Version(10, 5, 4)))
            .Options);
    }

    [Fact]
    public async Task Should_ReturnValidBlogData_WithAcceptablePerformance()
    {
        // Arrange
        var testFile = "blogsData";

        // Act
        var response = await _client.GetAsync("blog/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseObject = await response.Content.ReadFromJsonAsync<Blog>();

        var testData = await GetOrSaveTestData(responseObject, testFile);
        var expectedResponse = JsonSerializer.Deserialize<Blog>(testData.TestResult);

        responseObject.Should().NotBeNull("Should return valid Blogs");
        responseObject.Name.Should().Be(expectedResponse.Name, "Blog name does not match");
    }

    private async Task<TestData> GetOrSaveTestData<T>(T data, string filename)
    {
        var testData = await _context.TestData.SingleOrDefaultAsync(x => x.TestName == filename);
        if (testData is not null)
        {
            return testData;
        }

        testData = new TestData()
        {
            TestName = filename,
            TestResult = JsonSerializer.Serialize(data)
        };
        _context.TestData.Add(testData);
        await _context.SaveChangesAsync();
        return testData;
    }

    private static string GetPath(string fileName)
    {
        return Path.Combine(Environment.CurrentDirectory, fileName);
    }
    public void Dispose()
    {
        _client.Dispose();
        _context.Dispose();
    }
}
