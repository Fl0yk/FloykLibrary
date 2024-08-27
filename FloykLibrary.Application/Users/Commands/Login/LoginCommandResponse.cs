namespace FloykLibrary.Application.Users.Commands.Login
{
    public class LoginCommandResponse
    {
        public bool IsLogedIn { get; init; } = false;

        public string JwtToken { get; init; } = string.Empty;

        public string RefreshToken {  get; init; } = string.Empty;
    }
}
