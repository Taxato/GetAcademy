using System.Text.Json;

namespace BookingApp;

public class BookingData
{
	public int Id { get; set; }
	public string CustomerName { get; set; } = "";
	public string Room { get; set; } = "";
	public string Description { get; set; } = "";
	public DateOnly Date { get; set; }
	public TimeOnly StartTime { get; set; }
	public TimeOnly EndTime { get; set; }

	private static readonly string filePath = Path.GetFullPath(
		Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "bookings.json")
	);

	private static readonly JsonSerializerOptions jsonSerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true,
		WriteIndented = true,
		IndentCharacter = '\t',
	};

	public static List<BookingData>? LoadFromFile()
	{
		string json = File.ReadAllText(filePath);
		return JsonSerializer.Deserialize<List<BookingData>>(json, jsonSerializerOptions);
	}

	public static void SaveToFile(List<BookingData> bookings)
	{
		string json = JsonSerializer.Serialize(bookings, jsonSerializerOptions);
		File.WriteAllText(filePath, json);
	}
}
