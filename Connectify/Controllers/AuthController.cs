using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var errorMessage = await _authService.RegisterUserAsync(request);
        if (!errorMessage)
        {
            return BadRequest(new { message = "email already exists" });
        }

        return StatusCode(201, new { message = "Registered successfully! Please verify your email." });
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        var errorMessage = await _authService.ConfirmEmailAsync(token);
        if (errorMessage != null)
        {
            return BadRequest(new { message = errorMessage });
        }

        return Ok(new { message = "Xác thực email thành công" });

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {

        var (result, token) = await _authService.LoginAsync(dto);

        switch (result)
        {
            case LoginResult.InvalidCredentials:
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng!" });

            case LoginResult.EmailNotVerified:
                return StatusCode(403, new { message = "Tài khoản chưa xác thực Email. Vui lòng kiểm tra email để xác thực." });

            case LoginResult.Success:
                return Ok(new { token });

            default:
                return StatusCode(500, new { message = "Lỗi không xác định." });
        }
    }
}