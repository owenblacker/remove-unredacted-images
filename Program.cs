// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

bool dryRun = (args.Length > 0 && args[0] == "--dry-run");

if (!dryRun)
{
	PromptForDryRun();
}

int deleted = 0;
int notFound = 0;

var files = Directory.EnumerateFiles(Environment.CurrentDirectory, "*(redacted).jpg");
foreach (var file in files)
{
	var unredacted = Regex.Replace(file, @"\s*\(redacted\)\s*", "");

	if (File.Exists(unredacted))
	{
		if (!dryRun) { File.Delete(unredacted); }
		deleted++;
		Console.WriteLine($"Deleted {unredacted[(unredacted.LastIndexOf('\\')+1)..]}");
	}
	else
	{
		notFound++;
		Console.WriteLine($"No unredacted file found to match {file[(file.LastIndexOf('\\')+1)..]}");
	}
}

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"{(dryRun ? "Dry run complete" : "Finished")}. {deleted} files deleted; {notFound} files had no unredacted file to remove.");
Console.ResetColor();

void PromptForDryRun()
{
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.WriteLine("Beginning deletion without dry run. Do you wish to proceed?");
	Console.WriteLine("[Y]es, [D]ry run, [N]o (exit).");

	bool continueLoop = true;

	while (continueLoop)
	{
		var key = Console.ReadKey(true).Key;

#pragma warning disable IDE0059 // Unnecessary assignment of a value
		switch (key)
		{
			case ConsoleKey.Y:
				Console.WriteLine("Continuing deletion...");
				Console.ResetColor();
				continueLoop = false;
				return;

			case ConsoleKey.D:
				Console.WriteLine("Changing to dry run...");
				Console.ResetColor();
				continueLoop = false;
				dryRun = true;
				return;

			case ConsoleKey.N:
			case ConsoleKey.X:
				Console.WriteLine("Exiting.");
				Console.ResetColor();
				continueLoop = false;
				Environment.Exit(1);
				break;

			default:
				break;
		}
#pragma warning restore IDE0059 // Unnecessary assignment of a value
	}
}