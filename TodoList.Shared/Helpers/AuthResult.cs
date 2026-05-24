namespace TodoList.Shared.Helpers;

public class AuthResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}
