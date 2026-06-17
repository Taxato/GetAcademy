namespace ClassesLesson;

public class Person(string nameParam, int ageParam, string addressParam)
{
	public const string Race = "Human";

	public string Address = addressParam;
	public int Age = ageParam;
	public string Name = nameParam;

	public void Introduce()
	{
		Console.WriteLine($"Hi, my name is {Name} and I am {Age} years old. I am a {Race}");
	}
}