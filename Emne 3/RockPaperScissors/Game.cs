namespace RockPaperScissors;

internal class Game
{
	private readonly Random _random = new();
	private int _losses;
	private int _ties;
	private int _wins;

	public void Run()
	{
		while (true)
		{
			Console.WriteLine("""
			                  Rock paper scissors shoot!
			                  1 - Stein
			                  2 - Saks
			                  3 - Papir
			                  4 - Avslutt spill
			                  """);
			Console.Write("Mitt svar: ");
			var userInput = Console.ReadLine();


			// 1. Check if userInput is valid integer
			// 2. If valid integer, store it as new variable "userInt"
			// 3. Check if userInt is between 1 and 4 (inclusive)
			if (int.TryParse(userInput, out var userInt) && userInt is <= 4 and >= 1)
			{
				Console.Clear();
				if (userInt == 4) break;

				var computerChoice = _random.Next(1, 4);

				var draw = userInt == computerChoice;
				var userWon = userInt == computerChoice - 1 || userInt == computerChoice + 2;

				if (draw) _ties++;
				else if (userWon) _wins++;
				else _losses++;

				Console.WriteLine($"""
				                   Du valgte {NumToString(userInt - 1)}!
				                   Jeg valgte {NumToString(computerChoice - 1)}!
				                   {(draw ? "Det ble uavgjort!" : (userWon ? "Du" : "Jeg") + " vant!")}
				                   Stillingen er {_wins} seiere, {_losses} tap, og {_ties} uavgjort. 
				                   """);
			}
			else
			{
				Console.WriteLine("Ugyldig svar! Vennligst skriv 1, 2, 3, eller 4");
			}


			Console.WriteLine("Trykk hva som helst for å fortsette");
			Console.ReadKey();
			Console.Clear();
		}


		Console.WriteLine("Takk for spillet!");
	}

	private static string NumToString(int num)
	{
		string[] choices = ["stein", "saks", "papir"];
		return choices[num];
	}
}