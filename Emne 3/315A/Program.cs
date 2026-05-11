var random = new Random();
var secretNumber = random.Next(1, 101);


var totalGuesses = 0;

Console.WriteLine("Guess my number (1,100)");
while (true)
{
	var guess = Console.ReadLine();
	if (int.TryParse(guess, out var guessNum))
	{
		totalGuesses++;

		if (guessNum == secretNumber)
		{
			Console.WriteLine($"Correct!! You used {totalGuesses} guesses.");
			break;
		}

		Console.WriteLine(guessNum < secretNumber
			? $"Too low! You've used {totalGuesses} guesses."
			: $"Too high! You've used {totalGuesses} guesses.");
	}
}