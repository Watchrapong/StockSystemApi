using Microsoft.AspNetCore.Mvc;
using StockSystem.Api.Data;
using StockSystem.Api.Domain;

namespace StockSystem.Api.Controllers;

public class LoginController : BaseController
{
    private readonly UnitOfWork _unitOfWork;

    public LoginController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpPost]
    public IActionResult Login()
    {
        var user = _unitOfWork.Repo<User>()
                              .Table.FirstOrDefault();

        return Success(user);
    }
}