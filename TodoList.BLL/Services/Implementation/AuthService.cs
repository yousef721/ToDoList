namespace TodoList.BLL.Services.Implementation;

public class AuthService : IAuthServices
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResult> RegisterAsync(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            FirstName = dto.FirstName,
            LastName  = dto.LastName,
            UserName  = dto.Email,
            Email     = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return new AuthResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return new AuthResult
        {
            Succeeded = true
        };
    }

    public async Task<AuthResult> LoginAsync(LoginDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(
            dto.Email,
            dto.Password,
            dto.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return new AuthResult { Succeeded = true };
        }

        if (result.IsLockedOut)
        {
            return new AuthResult
            {
                Succeeded = false,
                Errors = ["Account is locked. Please try again later."]
            };
        }

        if (result.IsNotAllowed)
        {
            return new AuthResult
            {
                Succeeded = false,
                Errors = ["Login is not allowed. Please verify your account."]
            };
        }

        return new AuthResult
        {
            Succeeded = false,
            Errors = ["Invalid email or password."]
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}