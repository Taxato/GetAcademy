using System.ComponentModel.DataAnnotations;

namespace ClaimTheSquareAPI.DTOs;

public class SquareDto
{
	[Range(0, 63)]
	public required int Index { get; set; }

	public required string Text { get; set; }
	public required ConsoleColor ForeGround { get; set; }
	public required ConsoleColor BackGround { get; set; }
}