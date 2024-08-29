namespace FloykLibrary.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public string Jwt {  get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}
