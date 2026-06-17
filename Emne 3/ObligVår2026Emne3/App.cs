namespace BookingApp;

public class App
{
	private readonly BookingManager _manager = new();

	public void Run()
	{
		while (true)
		{
			var choice = BookingConsole.AskForMenuChoice();

			switch (choice)
			{
				case "1": // Show bookings
					BookingConsole.ShowBookings(_manager.GetAll());
					break;
				case "2": // Add booking
					var customerName = BookingConsole.AskForString("Kundenavn");
					var room = BookingConsole.AskForString("Rom");
					var date = BookingConsole.AskForString("Dato (yyyy-mm-dd)");
					var startTime = BookingConsole.AskForString("Start tid (hh:mm)");
					var endTime = BookingConsole.AskForString("Slutt tid (hh:mm)");
					var description = BookingConsole.AskForString("Beskrivelse");
					var result = _manager.AddBooking(
						customerName,
						room,
						date,
						startTime,
						endTime,
						description
					);
					BookingConsole.ShowMessage(result);
					break;
				case "3": // Edit booking
					break;
				case "4": // Delete booking
					break;
				case "0":
					return;
				default:
					BookingConsole.ShowMessage("Ugyldig valg. Prøv igjen.");
					break;
			}
		}
	}
}
