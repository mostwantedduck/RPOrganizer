using RPOrganizer.Model;
using System.Collections.Generic;

namespace RPOrganizer.Interfaces;

public interface IRopFileProcessor
{
    List<Instruction> GetAllGadgets();

    Dictionary<string, List<Instruction>> FilterValidGadgets(List<Instruction> allGadgets);
}
