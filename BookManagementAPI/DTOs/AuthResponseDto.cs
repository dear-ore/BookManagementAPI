namespace BookManagementAPI.DTOs
{
    public class AuthResponseDto
    {
        public string JwtToken { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
