GetSentenceStats(
	"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");

return;

void GetSentenceStats(string input)
{
	var longestWord = "";
	var mostVowelsWord = "";

	foreach (var word in input.Split(" "))
	{
		if (word.Length > longestWord.Length) longestWord = word;
		if (CountVowelsInWord(word) > CountVowelsInWord(mostVowelsWord)) mostVowelsWord = word;
	}

	Console.WriteLine($"The longest word was {longestWord} ({longestWord.Length} characters).");
	Console.WriteLine($"The word with most vowels was {mostVowelsWord} ({CountVowelsInWord(mostVowelsWord)} vowels).");
}

int CountVowelsInWord(string word)
{
	// Original version
	// char[] vowels = ['a', 'e', 'i', 'o', 'u'];
	// return word.ToLower().ToCharArray().Sum(c => vowels.Contains(c) ? 1 : 0);

	// Improved version from GPT
	return word.ToLower().Count(c => "aeiou".Contains(c));
}