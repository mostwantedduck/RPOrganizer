using RPOrganizer.Services;
using System;

namespace RPOrganizer;

internal partial class Program
{
    static void Main(string[] args)
    {
        if (args == null || args.Length < 2)
        {
            ConsoleWriter.WriteLine("Usage: RPOrganizer.exe C:\\rop.txt C:\\output.txt", ConsoleColor.Red);
            return;
        }

        try
        {
            var inputRopFile = args[0].ToString();
            var outputGadgetsFile = args[1].ToString();

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
