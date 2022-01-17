using System;
using System.Collections.Generic;

namespace RPOrganizer;

public static class Constants
{
    public static readonly int MaxReturnValueAcceptable = Convert.ToInt32("0x10", 16);

    public const bool IgnoreEBPOperations = true;      

    public static class RegExRules
    {
        public static readonly Dictionary<string, string> Rules = new()
        {
            { "DE-REF", @"^mov e.., dword \[e..\] ; ret" },
            { "WRITE", @"^mov dword \[e..\], e.. ; ret" },
            { "XCHG", @"^xchg e.., e.. ; ret" },
            { "MOV", @"^mov e.., e.. ; ret" },
            { "ADD", @"^add e.., e.. ; ret" },
            { "SUB", @"^sub e.., e.. ; ret" },
            { "POP", @"^pop e.. ; ret" },
            { "PUSH", @"^push e.. ; ret" },
            { "PUSH-POP-RET", @"^push e.*pop e.*ret" },
            { "POP-PUSH-RET", @"^pop e.*push e.*ret" },
            { "PUSH-PUSH-POP-POP-RET", @"^push e.*push e.*pop e.*pop e.*ret" },
            { "NEG", @"^neg e.. ; ret" },
            { "INC", @"^inc e.. ; ret" },
            { "DEC", @"^dec e.. ; ret" },
            { "RET", @"^ret  ;" },
            { "XOR", @"^xor e.., e.. ; ret" },
            { "(SPECIAL) GET ESP TO SOME REG", @"^push esp.*pop e.*ret" },

        };
    }
}
