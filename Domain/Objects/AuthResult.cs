namespace Domain.Objects;

public class AuthResult
{
    public bool Success { get; set; }
    public UserToken UserToken { get; set; }
    public string ErrorMessage { get; set; }
}
