namespace Bookify_Library_mgnt.Dtos.Users.Token
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
