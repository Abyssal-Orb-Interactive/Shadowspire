using AtomicFramework.AtomicStructures;
using IntExtensions;

namespace UseCases
{
    public class LevelCases
    {
        public static void LevelUp(in IAtomicVariable<int> level, in int levelsAmount)
        {
            level.IncreaseValueBy(levelsAmount);
        }
    }
}