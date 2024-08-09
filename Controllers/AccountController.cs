
using Backend.Services;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IJwtService _jwtService;
        public AccountController(IAccountService accountService, IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }
        [AllowAnonymous]
        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                AccountViewModel user = await _accountService.LoginAdminAsync(loginViewModel);
                string accessToken = _jwtService.GenerateAccessToken(user.AccountId);
                user.AccessToken = accessToken;
                return Ok(new ResponseViewModel { Code = 0, Message = "Login Success!", Data = user});
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("resgiter-admin")]
        public async Task<ActionResult> RegisterAdmin([FromForm] RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _accountService.RegisterAdminAsync(registerViewModel);
                return Ok(new ResponseViewModel { Code = 0, Message = "Register Admin Success!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message });
            }
        }

        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()

        {
            var redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                if (!result.Succeeded)
                {
                    return BadRequest("Google authentication failed.");
                }
                // Lấy thông tin người dùng từ Google
                var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;
                var fullName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var userEmail = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var picture = claims?.FirstOrDefault(c => c.Type == "picture")?.Value;

                GoogleLoginViewModel googleLoginViewModel = new GoogleLoginViewModel { FullName = fullName, Email = userEmail, PictureUrl = picture, GoogleId = googleId };
                AccountViewModel user = await _accountService.LoginGoogleAsync(googleLoginViewModel);
                string accessToken = _jwtService.GenerateAccessToken(user.AccountId);
                user.AccessToken = accessToken;
                return Ok(new ResponseViewModel { Code = 0, Message = "Login Success!", Data = user});
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel { Code = 1, Message = ex.Message});
            }
        }
    }
}
