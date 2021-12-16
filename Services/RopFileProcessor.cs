using RPOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace RPOrganizer.Services;

public class RopFileProcessor : Interfaces.IRopFileProcessor
{
    private const string TryingToOpenText = "Trying to open";

    private const string OpCodeReturnWithValue = "retn 0x";
    private const string OpCodeEBP = "ebp";

    private readonly IEnumerable<string> _contents;

    public RopFileProcessor(IEnumerable<string> contents)
    {
        _contents = contents;
    }

    public Dictionary<string, List<Instruction>> FilterValidGadgets(List<Instruction> allGadgets)
    {
        if (allGadgets == null || !allGadgets.Any())
        {
            throw new Exception($"Incorrect use: Initialize the {nameof(allGadgets)} parameter calling {nameof(GetAllGadgets)} method first.");
        }

        var initializedFilteredGadgetsDict = InitializeFilteredGadgets();

        foreach (var gadget in allGadgets)
        {
            foreach (var rule in Constants.RegExRules.Rules)
            {
                if (!Regex.IsMatch(gadget.OpCodes, rule.Value, RegexOptions.IgnoreCase))
                {
                    continue;
                }

                if (gadget.OpCodes.Contains(OpCodeReturnWithValue))
                {
                    var arr = gadget.OpCodes.Split(" ");
                    var returnValue = arr[^2];

                    var currentReturnValueAsBytes = Convert.ToInt32(returnValue, 16);

                    if (currentReturnValueAsBytes > Constants.MaxReturnValueAcceptable)
                    {
                        continue;
                    }
                }

                if (Constants.IgnoreEBPOperations && gadget.OpCodes.Contains(OpCodeEBP))
                {
                    continue;
                }

                initializedFilteredGadgetsDict[rule.Key].Add(new Instruction(gadget.Address, gadget.OpCodes, gadget.File));
            }
        }

        return initializedFilteredGadgetsDict;
    }

    public List<Instruction> GetAllGadgets()
    {
        if (!_contents.ToList().Any())
        {
            throw new Exception($"Fatal: No data has been found. Check your ROP input file.");
        }

        var output = new List<Instruction>();

        var originDllName = string.Empty;

        foreach (var line in _contents)
        {
            if (line.Contains(TryingToOpenText))
            {
                originDllName = line.Split("'")[1];
            }

            var validLineToBeParsed = line.IndexOf(';') != -1;

            if (validLineToBeParsed)
            {
                var currentLine = line.Trim();

                var address = currentLine[..10];

                var items = currentLine.Split(" ");
                var itemsRange = items[1..(items.Length - 3)];

                output.Add(new Instruction(address, string.Join(" ", itemsRange), originDllName));
            }
        }

        return output;
    }

    private static Dictionary<string, List<Instruction>> InitializeFilteredGadgets()
    {
        var filteredGadgets = new Dictionary<string, List<Instruction>>();

        foreach (var keyValue in Constants.RegExRules.Rules)
        {
            filteredGadgets.Add(keyValue.Key, new List<Instruction>());
        }

        return filteredGadgets;
    }
}
