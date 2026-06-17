namespace Library;

public class MenuItem(string text, Action action)
{
	public readonly Action Action = action;
	public readonly string Text = text;
}