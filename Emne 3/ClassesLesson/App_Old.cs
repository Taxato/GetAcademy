namespace ClassesLesson;

internal enum MenuOptions
{
	ShowPeople,
	AddPerson,
	DeletePerson
}

public class App_Old
{
	private static readonly string[] MenuOptionStrings = ["Vis personer", "Legg til person", "Slett person"];
	private int _currentMenuOption;
	public List<Person> People;

	public App_Old()
	{
		People =
		[
			new Person("Mariann Olsen", 27, "Nyveien 32"),
			new Person("Ola Nordmann", 45, "Oslos Gater")
		];
	}


	public void Run()
	{
		while (true)
		{
			Console.CursorVisible = false;
			Console.Clear();
			Console.WriteLine("Naviger med piltaster, velg med Enter, avslutt med Backspace");
			for (var i = 0; i < MenuOptionStrings.Length; i++)
			{
				var menuOption = MenuOptionStrings[i];
				if (i == _currentMenuOption)
				{
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.White;
				}

				Console.WriteLine(menuOption);
				Console.ResetColor();
			}

			var keyStroke = Console.ReadKey();

			switch (keyStroke.Key)
			{
				case ConsoleKey.Backspace:
				case ConsoleKey.Escape:
				case ConsoleKey.Delete:
					return;
				case ConsoleKey.DownArrow:
					_currentMenuOption = (_currentMenuOption + 1) % MenuOptionStrings.Length;
					break;
				case ConsoleKey.UpArrow:
					_currentMenuOption =
						(_currentMenuOption - 1 + MenuOptionStrings.Length) % MenuOptionStrings.Length;
					break;
				case ConsoleKey.Enter:
					SelectMenuOption();
					break;
			}
		}
	}

	private void SelectMenuOption()
	{
		switch ((MenuOptions)_currentMenuOption)
		{
			case MenuOptions.ShowPeople:
				ListPeople();
				break;
			case MenuOptions.AddPerson:
				AddPerson();
				break;
			case MenuOptions.DeletePerson:
				DeletePerson();
				break;
		}
	}

	private void ListPeople()
	{
		Console.Clear();
		foreach (var person in People)
			Console.WriteLine($"Navn: {person.Name} - Alder: {person.Age} - Addresse: {person.Address}");
		Console.WriteLine("Trykk en tast for å fortsette...");
		Console.ReadKey();
	}

	private void AddPerson()
	{
		Console.CursorVisible = true;

		Console.Clear();
		Console.WriteLine("Opprett ny person");
		Console.Write("Navn: ");
		var newName = Console.ReadLine();

		Console.Clear();
		Console.WriteLine("Opprett ny person");
		Console.Write("Alder: ");
		var newAge = int.Parse(Console.ReadLine());

		Console.Clear();
		Console.WriteLine("Opprett ny person");
		Console.Write("Addresse: ");
		var newAddress = Console.ReadLine();

		People.Add(new Person(newName, newAge, newAddress));
	}

	private void DeletePerson()
	{
	}
}