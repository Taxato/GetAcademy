namespace BasicRpg;

internal class Character
{
	public Character(string type, string name, int maxHealth, int strength)
	{
		Type = type;
		Name = name;
		Health = MaxHealth = maxHealth;
		Strength = strength;
	}

	public int Health { get; set; }
	public int MaxHealth { get; }
	public string Name { get; }
	public int Strength { get; }
	public string Type { get; }
}

internal class Goblin() : Character("Enemy", "Goblin", 10, 2);

public class MyClass(int num1, int num2)
{
	private int _num1 = num1;
	private int _num2 = num2;
}