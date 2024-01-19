using Microsoft.AspNetCore.Mvc;
using Models;
using Models.CreateRequests;

namespace RestApi.Controllers;

/// <summary>
/// Controller class that allows callers to interact with Accounts.
/// </summary>
[ApiController]
[Route("/accounts")]
public class AccountController : ControllerBase
{
    /// <summary>
    /// Creates an account using the provided create request
    /// </summary>
    /// <param name="createRequest">Create request for an account</param>
    /// <returns>An IAccount model representing the created account</returns>
    [HttpPost("create")]
    public IAccount Create(ICreateAccountRequest createRequest)
    {
        return new Account()
        {
            Id = 0L,
            Name = createRequest.Name,
            Type = createRequest.Type
        };
    }
}
