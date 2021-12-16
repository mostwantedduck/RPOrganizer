using RPOrganizer.Services;
using System;
using CommandLine;

namespace RPOrganizer;

internal partial class Program
{
    static void Main(string[] args)
    {
        var inputRopFile = string.Empty;
        var outputGadgetsFile = string.Empty;

        Parser.Default.ParseArguments<CommandLineOptions>(args)
               .WithParsed(o =>
               {
                   if (o.Verbose)
                   {
                       Console.WriteLine($"Verbose output enabled");
                   }

                   if (string.IsNullOrEmpty(o.InputFile))
                   {
                       Console.WriteLine($"Usage: RPOrganizer.exe -i rop.txt -o output.txt");
                       return;
                   }
                   else
                   {
                       inputRopFile = o.InputFile;
                   }

                   if (string.IsNullOrEmpty(o.OutputFile))
                   {
                       Console.WriteLine($"Usage: RPOrganizer.exe -i C:\\rop.txt -o C:\\output.txt");
                       return;
                   }
                   else
                   {
                       outputGadgetsFile = o.OutputFile;
                   }
               });

        try
        {
            var ropFile = new RopFile(inputRopFile, outputGadgetsFile);
            var ropFileContents = ropFile.ReadFile();

            var ropFileProcessor = new RopFileProcessor(ropFileContents);
            var allGadgets = ropFileProcessor.GetAllGadgets();

            ConsoleWriter.WriteLine($"[+] Loaded {allGadgets.Count} gadgets...{Environment.NewLine}", ConsoleColor.DarkGreen);

            ropFile.WriteGadgetsToFile(ropFileProcessor.FilterValidGadgets(allGadgets));

            ConsoleWriter.WriteLine($"{Environment.NewLine}[+] Output saved to {outputGadgetsFile}", ConsoleColor.DarkGreen);
        }
        catch (Exception ex)
        {
            ConsoleWriter.WriteLine($"Fatal Error: {ex.Message}", ConsoleColor.Red);
        }
    }
}
