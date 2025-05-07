using System.Runtime.InteropServices;

namespace Homework10_11;

public class Formula
{
    private string name { get; }
    private string anwser { get; }

    public Formula(string name, string anwser)
    {
        this.name = name;
        this.anwser = anwser;
    }

    public void PrintName()
    {
        Console.WriteLine(name);
    }

    public void PrintAnwser()
    {
        Console.WriteLine(anwser);
    }
}