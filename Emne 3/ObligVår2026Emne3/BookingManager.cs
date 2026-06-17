namespace BookingApp;

public class BookingManager
{
	private List<Booking> _bookings = [];

	public BookingManager()
	{
		_bookings = LoadFromFile();
	}

	public List<Booking> GetAll() => _bookings;

	public List<int> GetAllIds()
	{
		return [.. GetAll().Select(b => b.Id)];
	}

	public int GetHighestId()
	{
		return GetAllIds().Max();
	}

	public string AddBooking(
		string customerName,
		string room,
		string date,
		string startTime,
		string endTime,
		string description
	)
	{
		Booking newBooking = new(
			GetHighestId() + 1,
			customerName,
			room,
			DateOnly.Parse(date),
			TimeOnly.Parse(startTime),
			TimeOnly.Parse(endTime),
			description
		);

		if (ValidateBookingTime(newBooking))
			return "Booking overlaps with another booking in the same room";

		_bookings.Add(newBooking);
		SaveToFile();
		return "Successfully added booking";
	}

	public bool DeleteBooking(int id)
	{
		var index = _bookings.FindIndex(b => b.Id == id);
		if (index == -1)
			return false;
		_bookings.RemoveAt(index);
		SaveToFile();
		return true;
	}

	public string EditBooking(
		int id,
		string customerName,
		string room,
		string date,
		string startTime,
		string endTime,
		string description
	)
	{
		var booking = GetAll().Find(b => b.Id == id);
		if (booking == null)
			return $"Error: Found no booking with ID {id}.";

		Booking updatedBooking = new(
			id,
			customerName,
			room,
			DateOnly.Parse(date),
			TimeOnly.Parse(startTime),
			TimeOnly.Parse(endTime),
			description
		);

		if (ValidateBookingTime(updatedBooking))
			return "Error: Booking time overlaps with another booking.";

		var index = _bookings.FindIndex(b => b.Id == updatedBooking.Id); // Should never return -1 since we're validating existence of Id in List

		_bookings[index] = updatedBooking;
		SaveToFile();

		return "Successfully updated booking.";
	}

	private static List<Booking> LoadFromFile()
	{
		var bookings = BookingData.LoadFromFile();

		return bookings == null
			? []
			:
			[
				.. bookings.Select(b => new Booking(
					b.Id,
					b.CustomerName,
					b.Room,
					b.Date,
					b.StartTime,
					b.EndTime,
					b.Description
				)),
			];
	}

	private void SaveToFile()
	{
		BookingData.SaveToFile(
			[
				.. GetAll()
					.Select(b => new BookingData
					{
						Id = b.Id,
						CustomerName = b.CustomerName,
						Room = b.Room,
						Date = b.Date,
						StartTime = b.Start,
						EndTime = b.End,
						Description = b.Description,
					}),
			]
		);
	}

	/// <summary>
	/// Checks if booking time overlaps with another booking time in the same room.
	/// Makes sure to not compare with self, IE, does not compare to booking with same Id
	/// </summary>
	/// <param name="booking">The booking to compare to other bookings</param>
	/// <returns>True if booking time is valid, False if invalid</returns>
	private bool ValidateBookingTime(Booking booking)
	{
		TimeRange bookingTimeRange = new(booking);
		var otherBookingTimes = GetAll()
			.Where(b => booking.Room == b.Room && b.Id != booking.Id)
			.Select(b => new TimeRange(b));
		return !otherBookingTimes.All(b => !b.OverLapsWith(bookingTimeRange));
	}
}
