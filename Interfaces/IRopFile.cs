using RPOrganizer.Model;
using System.Collections.Generic;

namespace RPOrganizer.Interfaces;

public interface IRopFile
{
    IEnumerable<string> ReadFile();

    void WriteGadgetsToFile(Dictionary<string, List<Instruction>> gadgets);
}
