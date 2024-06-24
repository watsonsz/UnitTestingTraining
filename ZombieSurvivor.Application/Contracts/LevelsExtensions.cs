using static ZombieSurvivor.Application.Contracts.ISurvivor;

namespace ZombieSurvivor.Application.Contracts
{
    public static class LevelsExtensions
    {
        public static Levels GetNext(this Levels current)
        {
            Levels[] values = (Levels[])Enum.GetValues(typeof(Levels));
            int currentIndex = Array.IndexOf(values, current);
            int nextIndex = (currentIndex + 1) % values.Length;
            return values[nextIndex];
        }
    }

}
