namespace TodoList.BLL.Services.Interface;

public interface IAuthServices
{
    Task<AuthResult> RegisterAsync(RegisterDto dto);
    Task<AuthResult> LoginAsync(LoginDto dto);
    Task LogoutAsync();
}
