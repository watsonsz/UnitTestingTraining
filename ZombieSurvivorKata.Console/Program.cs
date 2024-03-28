// See https://aka.ms/new-console-template for more information
using ZombieSurvivor.Application.Contracts;

Console.WriteLine("Hello, World!");
int threshold = 6;
var characterLevel = ISurvivor.Levels.Blue;
Console.WriteLine(characterLevel.ToString());
ISurvivor.Levels levelResult = (ISurvivor.Levels)Enum.ToObject(typeof(ISurvivor.Levels), threshold);
Console.WriteLine(levelResult.ToString());

var values = Enum.GetValues(typeof(ISurvivor.Levels));
foreach (var level in values)
{
    if(threshold < (int)level)
    {
        threshold = (int)level; break;
    }
}
Console.WriteLine(threshold);
