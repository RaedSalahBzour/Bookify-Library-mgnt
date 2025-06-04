namespace Application.Authorization.Dtos.Claims
{
    public class AddClaimToUserDto
    {
        public string userId { get; set; }
        public string claimType { get; set; }
        public string claimValue { get; set; }
    }
}
