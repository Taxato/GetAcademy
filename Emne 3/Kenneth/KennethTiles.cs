namespace Kenneth;

public abstract class KennethTiles
{
	private static readonly int[] NumbersArray = GenerateArray();

	public static void Run()
	{
		Console.WriteLine("Lets play a game, Y/N");
		var input = Console.ReadLine();
		input = input?.ToLower();
		if (input == "y")
		{
			DrawBoard();
			MovePiece();
		}
		else
		{
			Console.WriteLine("Game Over");
		}
	}

	private static void DrawBoard()
	{
		for (var i = 0; i < NumbersArray.Length; i++)
		{
			Console.Write(NumbersArray[i] == 9 ? $"{' ',-3}" : $"{NumbersArray[i],-3}");
			if ((i + 1) % 3 == 0) Console.WriteLine();
		}
	}

	private static void MovePiece()
	{
		Console.WriteLine("Choose a number to move to the empty space.");
		var userInput = Console.ReadLine();
		var userMove = Convert.ToInt32(userInput);
		int[] possibleMoves = [userMove - 3, userMove - 1, userMove + 1, userMove - 3];
		possibleMoves = RemoveInvalidIndexes(possibleMoves);
		Console.WriteLine(Array.Exists(possibleMoves, move => move == userMove)
			? $"Move {userMove} to the empty space."
			: $"Move {userMove} is invalid. Try again.");
	}

	private static int[] RemoveInvalidIndexes(int[] array)
	{
		var validList = new List<int>(array);
		validList.RemoveAll(item => item is < 0 or > 8);
		return validList.ToArray();
	}


	private static int[] GenerateArray()
	{
		var rand = new Random();
		var array = new int[9];

		for (var i = 0; i < array.Length; i++) array[i] = i + 1;

		for (var i = array.Length - 1; i >= 0; i--)
		{
			var j = rand.Next(0, i + 1);
			(array[i], array[j]) = (array[j], array[i]);
		}

		return array;
	}
}