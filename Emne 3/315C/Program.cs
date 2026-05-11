Console.WriteLine(ReverseText("Terje"));
Console.WriteLine(ReverseText("Racecar"));


return;

string ReverseText(string input)
{
	var charArr = input.ToCharArray();
	Array.Reverse(charArr);
	return new string(charArr);
}