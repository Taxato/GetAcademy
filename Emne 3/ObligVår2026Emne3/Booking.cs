namespace BookingApp;

public class Booking(
	int id,
	string customerName,
	string room,
	DateOnly date,
	TimeOnly start,
	TimeOnly end,
	string description
)
{
	public int Id { get; private set; } = id;
	public string CustomerName { get; private set; } = customerName;
	public string Room { get; private set; } = room;
	public DateOnly Date { get; private set; } = date;
	public TimeOnly Start { get; private set; } = start;
	public TimeOnly End { get; private set; } = end;
	public string Description { get; private set; } = description;
}
