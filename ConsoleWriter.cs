using System;

namespace RPOrganizer;

public static class ConsoleWriter
{
    private static readonly object _messageLock = new();

    public static void WriteLine(string message, ConsoleColor color)
    {
        lock (_messageLock)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
