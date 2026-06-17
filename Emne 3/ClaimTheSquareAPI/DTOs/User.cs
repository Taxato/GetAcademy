namespace ClaimTheSquareAPI.DTOs;

// What you save in database
public class User
{
	public string Username { get; set; }
	public string EncryptedPassword { get; set; }
	public int Id { get; set; }
	public string Email { get; set; }
}

// What user gets
public class UserDto
{
	public string Username { get; set; }
	public string Email { get; set; }
}