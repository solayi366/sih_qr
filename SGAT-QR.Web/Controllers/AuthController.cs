using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SGAT_QR.Core.Entities;

namespace SGAT_QR.Web.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly SignInManager<Usuario> _signInManager;

    public AuthController(SignInManager<Usuario> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken] // Valida el token del formulario
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        await _signInManager.SignOutAsync(); // Limpiar rastro previo

        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: true, lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            return LocalRedirect("/");
        }

        return Redirect("/login?error=true");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("/login");
    }
}