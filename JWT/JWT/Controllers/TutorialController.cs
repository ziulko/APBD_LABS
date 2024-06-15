using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

/*
 * TODO w pracy domowej
 * Koncowki do logowania, rejestracji oraz refreshowania sesji umiescic w kontrolerze AuthController
 * 
 * 1. Logowanie api/auth/login
 * Input: username (email), password
 * - Sprawdzenie poprawnosci danych uzytkownika
 * - if(true) => generujemy token z krotkim czasem zycia + refresh token z dlugim czasem zycia => 200
 * - if(false) => 401 niepoprny login lub haslo
 * Output: tokeny
 *
 * 2. Refreshowanie sesji api/auth/refresh
 * Input: refresh token
 * - Sprawdzenie czy refresh token czy jest poprawny
 * - if(true) -> generujemy token z krotkim czasem zycia + refresh token z dlugim czasem zycia => 200
 * - if(false) => 401 Invalid token
 * Output: tokeny
 *
 * 3. Rejestacja uzytkownika api/auth/register
 * - Input: username, password
 * - Sprawdzamy czy nazwa uzytkownika jest unikalna
 * - Walidujemy zapytanie
 * - Hashujemy haslo
 * - Tworzymy nowy rekord dla uzytkownika w bazie ktory bedzie zawieral jego username oraz hash ktory wygenerowalismy w ramach hasla
 *
 * 4. Zabezpiecznie jednej koncowki
 */

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialController : ControllerBase
    {
        private readonly IConfiguration _config;
        
        public TutorialController(IConfiguration config)
        {
            _config = config;
        }
        
        [HttpGet("hash-password/{password}")]
        public IActionResult HashPassword(string password)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                new byte[] { 0 },
                10,
                HashAlgorithmName.SHA512,
                1
            );

            return Ok(Convert.ToHexString(hash));
        }

        [HttpGet("hash-password-with-salt/{password}")]
        public IActionResult HashPasswordWithSalt(string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return Ok(passwordHasher.HashPassword(new User(), password));
        }

        [HttpPost("verify-password")]
        public IActionResult VerifyPassword(VerifyPasswordRequestModel requestModel)
        {
            var passwordHasher = new PasswordHasher<User>();
            return Ok(passwordHasher.VerifyHashedPassword(new User(), requestModel.Hash, requestModel.Password) == PasswordVerificationResult.Success);
        }
    }

    public class VerifyPasswordRequestModel
    {
        public string Password { get; set; } = null!;
        public string Hash { get; set; } = null!;
    }

    public partial class User
    {
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}