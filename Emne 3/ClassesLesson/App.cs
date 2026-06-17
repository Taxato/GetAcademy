using Library;

namespace ClassesLesson;

public class App
{
	private readonly List<Person> _people =
	[
		new("Mariann Olsen", 27, "Nyveien 32"),
		new("Ola Nordmann", 45, "Oslos Gater")
	];


	public void Run()
	{
		List<MenuItem> menuItems = new([
			new MenuItem("Vis alle personer", ListAllPeople),
			new MenuItem("Legg til person", AddPerson),
			new MenuItem("Slett en person", () => { })
		]);
		Menu menu = new(menuItems);
		menu.Run();
	}

	private void ListAllPeople()
	{
		Console.Clear();
		foreach (var p in _people)
			Console.WriteLine($"Navn: {p.Name} - Alder: {p.Age} - Addresse: {p.Address}");
		Console.WriteLine("Trykk en tast for å fortsette...");
		Console.ReadKey();
	}

	private void AddPerson()
	{
		Console.CursorVisible = true;
		var newName = Menu.Prompt("Opprett ny person", "Navn");
		var newAge = int.Parse(Menu.Prompt("Opprett ny person", "Alder"));
		var newAddress = Menu.Prompt("Opprett ny person", "Addresse");

		_people.Add(new Person(newName, newAge, newAddress));
	}
}