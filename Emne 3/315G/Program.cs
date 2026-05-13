namespace _315G;

internal static class Program
{
	private static void Main(string[] args)
	{
		const int range = 250;
		var counts = new int[range];
		var text = Console.ReadLine();
		while (!string.IsNullOrWhiteSpace(text))
		{
			CountChars(counts, text);
			PrintCounts(counts);
			text = Console.ReadLine();
		}
	}

	private static void CountChars(int[] counts, string text)
	{
		foreach (var character in text) counts[char.ToUpper(character)]++;
	}

	private static void PrintCounts(int[] counts)
	{
		for (var i = 0; i < counts.Length; i++)
			if (counts[i] > 0)
			{
				var character = (char)i;
				Console.WriteLine(character + " - " + counts[i]);
			}
	}
}