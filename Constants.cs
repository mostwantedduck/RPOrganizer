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
            { "DEREF", @"^mov e.., dword \[e..\] ; ret" },
            { "XCHG", @"^xchg e.., e.. ; ret" },
            { "MOV", @"^mov e.., e.. ; ret" },
            { "ADD", @"^add e.., e.. ; ret" },
            { "SUB", @"^sub e.., e.. ; ret" },
            { "POP", @"^pop e.. ; ret" },
            { "NEG", @"^neg e.. ; ret" },
            { "INC", @"^inc e.. ; ret" },
            { "DEC", @"^dec e.. ; ret" },
            { "RET", @"^ret  ;" },
            { "WRITE", @"^mov dword \[e..\], e.. ; ret" },
            { "GET ESP", @"push esp.*pop e.*ret" }
        };
    }
}
