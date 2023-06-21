namespace api.DTOs
{
    public class RegisterDto : LoginDto
    {
        public string Email { get; set; } = string.Empty; 
    }
}
