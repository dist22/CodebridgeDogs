namespace CodebridgeDogs.Models;

public class Dog(string name, string color, int tailLenght, int weight)
{
    public string Name { get; set; } = name;
    public string Color { get; set; } = color;
    public int TailLenght { get; set; } = tailLenght;
    public int Weight { get; set; } = weight;
}