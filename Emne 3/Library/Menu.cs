namespace Library;

public class Menu(List<MenuItem> items)
{
	private int _selected;

	public void Run()
	{
		while (true)
		{
			Draw();
			Console.CursorVisible = false;

			var key = Console.ReadKey();

			switch (key.Key)
			{
				case ConsoleKey.DownArrow:
					_selected = (_selected + 1) % items.Count;
					break;
				case ConsoleKey.UpArrow:
					_selected = (_selected - 1 + items.Count) % items.Count;
					break;

				case ConsoleKey.Enter:
					items[_selected].Action();
					break;

				case ConsoleKey.Escape:
				case ConsoleKey.Backspace:
				case ConsoleKey.Delete:
					return;
			}
		}
	}

	private void Draw()
	{
		Console.Clear();
		for (var i = 0; i < items.Count; i++)
		{
			if (i == _selected)
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
			}

			Console.WriteLine(items[i].Text);
			Console.ResetColor();
		}
	}

	public static string Prompt(string header, string label)
	{
		Console.Clear();
		Console.WriteLine(header);
		Console.Write($"{label}: ");
		return Console.ReadLine() ?? "";
	}
}