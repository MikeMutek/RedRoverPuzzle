// See https://aka.ms/new-console-template for more information

using RedRoverCodePuzzle;

string puzzleString = @"(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";


if (!StringHelper.IsValidFormat(puzzleString))
{
    Console.WriteLine("Input not properly formatted");
    Console.WriteLine($"\"{puzzleString}\" one or more parenthesis is missing");
    Console.ReadKey();
    Environment.Exit(Environment.ExitCode);
}


Console.WriteLine("Validation succeeded, moving on...");
Console.WriteLine("First, we'll read and print it the string as first in, first out");
Console.WriteLine("Any key to continue");
Console.ReadKey();


StringHelper.PrintAsItReads(puzzleString);

Console.WriteLine("Then, we'll parse the string into a SortedDictionary and print it alphabetically");
Console.WriteLine("Any key to continue");
Console.ReadKey();

StringHelper.PrintAlphabetical(puzzleString);


Console.WriteLine("Thanks, this was fun (took me longer than an hour though!).  Ran into a bit of analysis paralysis...ugh - lol");
Console.WriteLine();
Console.WriteLine("Here's to hoping my solution wasn't too mundane!");

Console.WriteLine("Any key to close");
Console.ReadKey();
Environment.Exit(Environment.ExitCode);