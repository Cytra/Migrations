using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ChromiumHtmlToPdfLib;
using ChromiumHtmlToPdfLib.Settings;

namespace Blogs.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController() : ControllerBase
{
    //[Authorize(Policy = "ReadAccess")]
    [HttpGet("secure-data")]
    public IActionResult GetSecureData()
    {
        return Ok("You have read access!");
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GeneratePdf([FromBody] string htmlContent)
    {
        var converter = new PdfService();
        var pdfBytes = await converter.GeneratePdfAsync(htmlContent);
        return File(pdfBytes, "application/pdf", "generated.pdf");
    }
}


public class PdfService
{

    public async Task<byte[]> GeneratePdfAsync(string htmlContent)
    {
        await using var converter = new Converter();
        converter.AddChromiumArgument("--no-sandbox");
        converter.AddChromiumArgument("--disable-dev-shm-usage");
        await using var pdfStream = new MemoryStream();
        await converter.ConvertToPdfAsync(htmlContent, pdfStream, new PageSettings());
        var result = pdfStream.ToArray();
        return result;
    }
}
