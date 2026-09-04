using EcommerceDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Windbrands.Data;

namespace EcommerceDB.Controllers;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<Cliente> _passwordHasher = new();

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.Session.GetInt32("ClienteId") != null)
        {
            return RedirectToLocal(returnUrl);
        }

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var correo = model.Correo.Trim().ToLowerInvariant();
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo.ToLower() == correo);
        if (cliente == null || string.IsNullOrWhiteSpace(cliente.PasswordHash) ||
            _passwordHasher.VerifyHashedPassword(cliente, cliente.PasswordHash, model.Password) == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "El correo o la contraseña no son correctos.");
            return View(model);
        }

        IniciarSesion(cliente);
        return RedirectToLocal(model.ReturnUrl);
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.Session.GetInt32("ClienteId") != null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new RegistroViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegistroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var correo = model.Correo.Trim().ToLowerInvariant();
        if (await _context.Clientes.AnyAsync(c => c.Correo.ToLower() == correo))
        {
            ModelState.AddModelError(nameof(model.Correo), "Ya existe una cuenta con este correo.");
            return View(model);
        }

        var cliente = new Cliente
        {
            NombreCompleto = model.NombreCompleto.Trim(),
            Correo = correo
        };
        cliente.PasswordHash = _passwordHasher.HashPassword(cliente, model.Password);

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        IniciarSesion(cliente);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View(new ForgotPasswordViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var correo = model.Correo.Trim().ToLowerInvariant();
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo.ToLower() == correo);
        if (cliente == null)
        {
            ModelState.AddModelError(nameof(model.Correo), "No encontramos una cuenta con ese correo.");
            return View(model);
        }

        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
            .Replace("+", "-").Replace("/", "_").TrimEnd('=');
        cliente.PasswordResetTokenHash = HashToken(token);
        cliente.PasswordResetExpiresAt = DateTime.UtcNow.AddMinutes(30);
        await _context.SaveChangesAsync();

        model.ResetUrl = Url.Action(nameof(ResetPassword), "Auth", new { correo, token }, Request.Scheme);
        return View(model);
    }

    [HttpGet]
    public IActionResult ResetPassword(string correo, string token)
    {
        return View(new ResetPasswordViewModel { Correo = correo, Token = token });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var correo = model.Correo.Trim().ToLowerInvariant();
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Correo.ToLower() == correo);
        if (cliente == null || cliente.PasswordResetTokenHash != HashToken(model.Token) ||
            cliente.PasswordResetExpiresAt <= DateTime.UtcNow)
        {
            ModelState.AddModelError(string.Empty, "El enlace no es válido o ya expiró.");
            return View(model);
        }

        cliente.PasswordHash = _passwordHasher.HashPassword(cliente, model.Password);
        cliente.PasswordResetTokenHash = null;
        cliente.PasswordResetExpiresAt = null;
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = "Tu contraseña fue actualizada. Ya puedes iniciar sesión.";
        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }

    private void IniciarSesion(Cliente cliente)
    {
        HttpContext.Session.SetInt32("ClienteId", cliente.Id);
        HttpContext.Session.SetString("ClienteNombre", cliente.NombreCompleto);
        HttpContext.Session.SetString("ClienteCorreo", cliente.Correo);
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        return !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? Redirect(returnUrl)
            : RedirectToAction("Index", "Home");
    }

    private static string HashToken(string token)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }
}