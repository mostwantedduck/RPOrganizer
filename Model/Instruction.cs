namespace RPOrganizer.Model;

public class Instruction
{
    public Instruction(string address, string opCodes, string file)
    {
        Address = address;
        OpCodes = opCodes;
        File = file;
    }

    public string Address { get; private set; }

    public string OpCodes { get; private set; }

    public string File { get; private set; }

    public override string ToString()
    {
        return $"{Address}, # {File} :: {OpCodes}";
    }
}
