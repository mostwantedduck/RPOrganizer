using RPOrganizer.Interfaces;
using RPOrganizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RPOrganizer.Services;

public class RopFile : IRopFile
{
    private readonly string _filePath;
    private readonly string _outputFilePath;

    public RopFile(string inputFilePath, string outputFilePath)
    {
        _filePath = inputFilePath;
        _outputFilePath = outputFilePath;
    }

    public IEnumerable<string> ReadFile()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException();
        }

        List<string> lines = new();

        using (StreamReader reader = new(_filePath))
        {
            while (!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine());
            }
        }

        return lines;
    }

    public void WriteGadgetsToFile(Dictionary<string, List<Instruction>> gadgets)
    {
        using StreamWriter outputFile = new(_outputFilePath);

        foreach (var category in gadgets)
        {
            var categoryCount = category.Value.Count;

            var title = $"{category.Key} gadgets [{categoryCount}]";

            var color = categoryCount == 0 ? ConsoleColor.Red : ConsoleColor.DarkBlue;

            ConsoleWriter.WriteLine($"[!] Found {categoryCount} \"{category.Key}\" gadgets...", color);

            outputFile.WriteLine($"{title}{Environment.NewLine}{new string('-', title.Length)}");

            foreach (var gadget in category.Value.OrderBy(x => x.OpCodes))
            {
                outputFile.WriteLine(gadget);
            }

            outputFile.WriteLine();
        }
    }
}
