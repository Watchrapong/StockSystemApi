using System.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace StockSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : Controller
{
    protected IActionResult Success(object? data = null)
    {
        dynamic dataResponse = new ExpandoObject();

        dataResponse.status = "success";
        dataResponse.data = data;

        return Ok(dataResponse);
    }

    protected IActionResult Error(string? msg = null)
    {
        var _msg = string.IsNullOrEmpty(msg) ? "เกิดข้อผิดพลาดบางอย่าง" : msg;
    
        dynamic dataResponse = new ExpandoObject();
    
        dataResponse.status = "error";
        dataResponse.message = _msg;
    
        return Ok(dataResponse);
    }
}