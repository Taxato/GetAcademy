namespace BookingApp;

public class TimeRange
{
	public readonly DateTime StartTime;
	public readonly DateTime EndTime;

	public TimeRange(DateOnly date, TimeOnly start, TimeOnly end)
	{
		StartTime = new(date, start);
		EndTime = new(date, end);
	}

	public TimeRange(Booking b)
	{
		StartTime = new(b.Date, b.Start);
		EndTime = new(b.Date, b.End);
	}

	public bool OverLapsWith(TimeRange other)
	{
		return this.StartTime < other.StartTime && other.StartTime < this.EndTime;
	}
}
