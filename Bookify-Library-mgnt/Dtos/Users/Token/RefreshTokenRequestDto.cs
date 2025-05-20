namespace Bookify_Library_mgnt.Dtos.Users.Token
{
    public class RefreshTokenRequestDto
    {
        public string UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
