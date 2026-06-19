using System.Text;

namespace BookingApp;

class Program
{
	static void Main(string[] args)
	{
		Console.InputEncoding = Encoding.UTF8;
		Console.OutputEncoding = Encoding.UTF8;

		var app = new App();
		app.Run();
	}
}
