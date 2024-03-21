using RPGCombat.Application.Classes;
using System.Reflection.PortableExecutable;

internal class Program
{
    private static void Main(string[] args)
    {
        Character player = new Character();
        Character enemy = new Character();
        Console.WriteLine($"Character {player.Id} Health: {player.Health} ");
        enemy.DealDamage(player);
        Console.WriteLine($"Character {player.Id} Health: {player.Health} "); 
        player.HealDamage(player);
        Console.WriteLine($"Character {player.Id} Health: {player.Health} ");
    }
}