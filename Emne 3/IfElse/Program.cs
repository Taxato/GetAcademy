Console.WriteLine(Oppgave2(7, 4));
Console.WriteLine(Oppgave2(10, 10));

Console.WriteLine(Oppgave3(10, 20));
Console.WriteLine(Oppgave3(10, 30));
Console.WriteLine(Oppgave3(10, 10));


return;


int Oppgave2(int a, int b)
{
	if (a == b) return a * b;
	return a + b;
}

bool Oppgave3(int a, int b)
{
	if (a == 30 || b == 30 || a + b == 30) return true;
	return false;
}