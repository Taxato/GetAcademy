using System.Diagnostics.CodeAnalysis;

namespace PasswordGenerator315I;

internal static class Program
{
	private const string ValidRequirementChars = "lLds";
	private const string LowercaseAlphabet = "abcdefghijklmnopqrstuvwxyz";
	private const string UppercaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private const string Digits = "0123456789";
	private const string SpecialSymbols = "!\"#¤%&/(){}[]";

	private static readonly Random Random = new();

	private static void Main(string[] args)
	{
		if (!ValidateArgumentsAndCreatePattern(args, out var pattern))
		{
			ShowHelpText();
			return;
		}

		Console.WriteLine(GeneratePassword(pattern));
	}

	private static bool ValidateArgumentsAndCreatePattern(string[] args, [NotNullWhen(true)] out string? pattern)
	{
		// Make sure args is either length 1 or 2, first argument is integer, and second argument (if present) is only valid symbols
		if (args.Length is < 1 or > 2 || !int.TryParse(args[0], out var passwordLength) ||
		    (args.Length == 2 && !ValidatePatternSymbols(args[1])))
		{
			pattern = null;
			return false;
		}

		pattern = new string((args.Length == 2 ? args[1] : "").PadRight(passwordLength, 'l').Shuffle().ToArray());
		return true;
	}

	private static bool ValidatePatternSymbols(string pattern)
	{
		return pattern.ToCharArray().All(c => ValidRequirementChars.Contains(c));
	}

	private static string GeneratePassword(string pattern)
	{
		var password = "";

		var patternArr = pattern.ToCharArray();
		foreach (var character in patternArr)
			password += character switch
			{
				'l' => GenerateChar(LowercaseAlphabet),
				'L' => GenerateChar(UppercaseAlphabet),
				'd' => GenerateChar(Digits),
				's' => GenerateChar(SpecialSymbols),
				_ => throw new ArgumentException("Invalid password pattern")
			};
		/*switch (character)
		{
			case 'l':
				password += GenerateChar(LowercaseAlphabet);
				break;
			case 'L':
				password += GenerateChar(UppercaseAlphabet);
				break;
			case 'd':
				password += GenerateChar(Digits);
				break;
			case 's':
				password += GenerateChar(SpecialSymbols);
				break;
			default:
				throw new ArgumentException("Invalid password pattern");
		}*/

		return password;
	}

	private static char GenerateChar(string charSet)
	{
		return charSet[Random.Next(charSet.Length)];
	}

	private static void ShowHelpText()
	{
		Console.WriteLine(
			"""
			PasswordGenerator
			Options:
			- l = liten bokstav
			- L = stor bokstav
			- d = siffer
			- s = spesialtegn (!"#¤%&/(){}[]
			Eksempel: PasswordGenerator 14 lLssdd
			    betyr
			        Min. 1 liten bokstav
			        Min. 1 stor bokstav
			        Min. 2 spesialtegn
			        Min. 2 sifre
			        Lengde på passordet skal være 14
			""");
	}
}