using RPGCombat.Application.Classes;
using System.Reflection.PortableExecutable;

internal class Program
{
    private static void Main(string[] args)
    {
        Character character = new RangeCharacter() {XYLocation = [1, 1] };
        Character enemy = new MeleeCharacter() { XYLocation = [2, 2] };
        Console.WriteLine(enemy.IsAlive);
        character.DealDamage(enemy);
        Console.WriteLine(enemy.IsAlive);
    }
}