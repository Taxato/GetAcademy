namespace BasicRpg;

internal class Game
{
	public void Fight(Character a, Character b)
	{
		Console.CursorVisible = false;
		Console.WriteLine($"""
		                   Fight! {a.Name} vs {b.Name}
		                   Press any key to begin...
		                   """);
		Console.ReadKey();
		while (true)
		{
			Console.Clear();

			Console.WriteLine($"""
			                   {a.Name} - HP: {Math.Max(a.Health, 0)}/{a.MaxHealth}
			                   {b.Name} - HP: {Math.Max(b.Health, 0)}/{b.MaxHealth}
			                   Press any key to continue...
			                   """);
			Console.ReadKey();

			if (a.Health <= 0 || b.Health <= 0) break;

			Console.Clear();
			Console.WriteLine($"""
			                   {a.Name} takes {b.Strength} damage!
			                   {b.Name} takes {a.Strength} damage!
			                   Press any key to continue...
			                   """);
			a.Health -= b.Strength;
			b.Health -= a.Strength;
			Console.ReadKey();
		}

		Console.Clear();
		if (a.Health <= 0 && b.Health > 0) Console.WriteLine($"{b.Name} Wins!");
		else if (a.Health > 0 && b.Health <= 0) Console.WriteLine($"{a.Name} Wins!");
		else Console.WriteLine("It's a draw");

		Console.CursorVisible = true;
	}
}